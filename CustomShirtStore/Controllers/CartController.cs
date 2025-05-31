using CustomShirtStore.Extensions;
using CustomShirtStore.Models;
using CustomShirtStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustomShirtStore.Controllers
{
    public class CartController : Controller
    {
        private readonly Exe201Context _context;

        public CartController(Exe201Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            var model = new OrderInformationViewModel
            {
                CartItems = cart
            };
            return View(model);
        }

        public IActionResult RemoveItem(int productId)
        {
            List<CartItem> cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            var itemToRemove = cart.FirstOrDefault(item => item.ProductId == productId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.SetObject("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity([FromBody] UpdateQuantityRequest request)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (item != null)
            {
                item.Quantity = request.Quantity;
                HttpContext.Session.SetObject("Cart", cart);
            }
            return Ok();
        }

        public class UpdateQuantityRequest
        {
            public long ProductId { get; set; }
            public int Quantity { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckoutProduct(OrderInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with validation errors
            }
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            if (!cart.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng trống. Vui lòng thêm sản phẩm trước khi thanh toán.");
                return View(model);
            }
            string diaChi = model.HouseNumber + ", "  + model.Ward + ", " + model.District + ", " + model.City;
            try
            {
                var order = new Order
            {
                UserId = User.Identity.IsAuthenticated ? long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) : null,
                GuestAddress = diaChi,
                GuestEmail = model.Email,
                GuestName = model.Name,
                GuestPhoneNumber = model.PhoneNumber,
                TotalAmount = cart.Sum(item => item.Quantity * item.Price),
                OrderStatus = "Đang xử lý",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };
            var cartItemToOrderItemMap = new Dictionary<CartItem, OrderItem>();
            foreach (var item in cart)
            {
                var orderItem = new OrderItem
                {
                    MaleName="",
                    FemaleName="",
                    MessageTitle="",
                    MsgContent="",
                    MsgImageUrl="",
                    MsgAudioUrl="",
                    ItemPrice = item.Price,
                    Quantity = item.Quantity,
                    Template = "1",
                    Size = item.Size,
                    
                };

                order.OrderItems.Add(orderItem);
                    cartItemToOrderItemMap.Add(item, orderItem);
                   
            }
                _context.Orders.Add(order);
                _context.SaveChanges();
                foreach (var cartItem in cart)
                {
                    var customerDesign = _context.CustomerDesigns.FirstOrDefault(cd => cd.Id == cartItem.GeneratedId);
                    if (customerDesign != null && cartItemToOrderItemMap.TryGetValue(cartItem, out var orderItem))
                    {
                        customerDesign.OrderItemId = orderItem.Id; // Set to the generated OrderItem.Id
                        _context.CustomerDesigns.Update(customerDesign);
                    }
                }
                _context.SaveChanges();
                HttpContext.Session.SetObject("Cart", new List<CartItem>());
                return RedirectToAction("DatHangThanhCong", "Home");
            }
            catch (Exception ex)
            {
                // Handle errors (e.g., database failure)
                ModelState.AddModelError("", $"Đã xảy ra lỗi khi xử lý đơn hàng: {ex.Message}");
                return View(model);
            }
            

            
        }
    }
}
