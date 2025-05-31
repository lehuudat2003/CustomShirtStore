using CustomShirtStore.Models;
using CustomShirtStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomShirtStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly Exe201Context _context;
        public AdminController(Exe201Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DoanhThu() { 
            return View(); 
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DonHang()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .ToListAsync();

            // Map Order to OrderViewModel
            var orderViewModels = orders.Select(order => new OrderViewModel
            {
                Id = order.Id,
                GuestName = order.GuestName,
                GuestPhoneNumber = order.GuestPhoneNumber,
                GuestAddress = order.GuestAddress,
                TotalAmountFormatted = string.Format("{0:N0} VND", order.TotalAmount),
                OrderStatus = order.OrderStatus
            }).ToList();

            return View(orderViewModels);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChiTietDonHang(int orderId)
        {
            var order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.CustomerDesigns)
            .ThenInclude(cd => cd.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);


            if (order == null)
            {
                return NotFound();
            }

            var viewModel = new OrderDetailViewModel
            {
                OrderId = order.Id,
                GuestName = order.GuestName,
                GuestPhoneNumber = order.GuestPhoneNumber,
                GuestAddress = order.GuestAddress,
                TotalAmountFormatted = string.Format("{0:N0} VND", order.TotalAmount),
                OrderStatus = order.OrderStatus,
                GuestEmail = order.GuestEmail,
                OrderItems = order.OrderItems.Select(oi => new OrderDetailViewModel.OrderItemViewModel
                {
                    OrderItemId = oi.Id,
                    ProductName = oi.CustomerDesigns.FirstOrDefault()?.Product?.ProductName ?? "No Product",
                    DesignImageUrl = oi.CustomerDesigns.FirstOrDefault()?.DesignImageUrl ?? string.Empty,
                    LinkMessage = $"/Message/{oi.Id}",
                    Quantity = (int)oi.Quantity,
                    ItemPriceFormatted = string.Format("{0:N0} VND", oi.ItemPrice),
                    Size = oi.Size,
                    QRImageUrl = oi.CustomerDesigns.FirstOrDefault()?.QrCodeImagePath ?? string.Empty
                }
                ).ToList()
            };
            return View(viewModel);
            {
            }
            
        }


        [Authorize(Roles = "Admin")]
        public IActionResult TaiKhoan()
        {
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatusUpdateRequest request)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId); // Replace 1 with the actual order ID(request.OrderId);
            if (order != null)
            {
                order.OrderStatus = request.Status;
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
        public class OrderStatusUpdateRequest
        {
            public int OrderId { get; set; }
            public string Status { get; set; }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> XoaDonHang(int orderId)
        {
            try
            {
                // Find the order with related OrderItems and CustomerDesigns, including Product
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.CustomerDesigns)
                            .ThenInclude(cd => cd.Product)
                    .FirstOrDefaultAsync(o => o.Id == orderId);

                if (order == null)
                {
                    TempData["ErrorMessage"] = "Đơn hàng không tồn tại.";
                    return RedirectToAction("DonHang");
                }

                // Delete all CustomerDesigns for each OrderItem
                foreach (var orderItem in order.OrderItems)
                {
                    if (orderItem.CustomerDesigns.Any())
                    {
                        _context.CustomerDesigns.RemoveRange(orderItem.CustomerDesigns);
                    }
                }

                // Delete all OrderItems
                if (order.OrderItems.Any())
                {
                    _context.OrderItems.RemoveRange(order.OrderItems);
                }

                // Delete the Order
                _context.Orders.Remove(order);

                // Save changes
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Xóa đơn hàng và các mục liên quan thành công.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi xóa đơn hàng: {ex.Message}";
            }

            return RedirectToAction("DonHang");
        }


    }
}
