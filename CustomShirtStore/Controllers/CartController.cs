using CustomShirtStore.Extensions;
using CustomShirtStore.Models;
using CustomShirtStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class CartController : Controller
    {
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
    }
}
