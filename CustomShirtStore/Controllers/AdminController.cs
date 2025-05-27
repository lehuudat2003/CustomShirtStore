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
                OrderItems = order.OrderItems.Select(oi => new OrderDetailViewModel.OrderItemViewModel
                {
                    OrderItemId = oi.Id,
                    ProductName = oi.CustomerDesigns.FirstOrDefault()?.Product?.ProductName ?? "No Product",
                    DesignImageUrl = oi.CustomerDesigns.FirstOrDefault()?.DesignImageUrl ?? string.Empty,
                    LinkMessage = $"/Message/{oi.Id}",
                    Quantity = (int)oi.Quantity,
                    ItemPriceFormatted = string.Format("{0:N0} VND", oi.ItemPrice)
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
    }
}
