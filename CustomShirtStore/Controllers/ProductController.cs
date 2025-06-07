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

        public IActionResult ProductList(string? category)
        {
            // Lấy danh sách category (có thể từ bảng Category nếu có)
            var allCategories = _context.Products
                .Where(p => p.IsActive)
                .Select(p => p.Category)
                .Distinct()
                .ToList();

            var query = _context.Products.Where(p => p.IsActive);

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }

            ViewBag.Categories = allCategories;
            ViewBag.SelectedCategory = category;

            var products = query.ToList();
            return View(products);
        }

    }
}
