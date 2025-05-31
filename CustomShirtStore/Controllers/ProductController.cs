using CustomShirtStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly Exe201Context _context;

        public ProductController(Exe201Context context)
        {
            _context = context;
        }

        public IActionResult ProductList()
        {
            var products = _context.Products
                .Where(p => p.IsActive)
                .ToList();

            return View(products);
        }
    }
}
