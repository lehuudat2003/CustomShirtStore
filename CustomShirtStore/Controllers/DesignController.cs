using CustomShirtStore.Models;
using CustomShirtStore.ViewModels;
using System;
using Microsoft.AspNetCore.Mvc;
using CustomShirtStore.Models;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using CustomShirtStore.Extensions;

namespace CustomShirtStore.Controllers
{
    public class DesignController : Controller
    {
        private readonly Exe201Context _context;
        private readonly IWebHostEnvironment _env;

        public DesignController(Exe201Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(long productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId && p.IsActive);
            if (product == null) return NotFound();

            var model = new ShirtDesignViewModel
            {
                Product = product,
                AvailableColors = product.Color?.Split(',').Select(c => c.Trim().ToLower()).ToList() ?? new List<string>()
            };

            return View(model);
        }
        [HttpGet("/images/icons-list")]
        public IActionResult GetIconImages()
        {
            var iconFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Icon");
            var files = Directory.GetFiles(iconFolder)
                                 .Select(Path.GetFileName)
                                 .Where(f => f.EndsWith(".png") || f.EndsWith(".jpg") || f.EndsWith(".jpeg"))
                                 .ToList();

            return Json(files);
        }
        [HttpPost]
        public async Task<IActionResult> SaveDesign(ShirtDesignViewModel model, IFormFile? uploadedImage)
        {
            if (uploadedImage != null)
            {
                string folder = Path.Combine("images", "customerdesign");
                string fileName = Guid.NewGuid() + Path.GetExtension(uploadedImage.FileName);
                string path = Path.Combine(_env.WebRootPath, folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await uploadedImage.CopyToAsync(stream);

                model.Design.UploadedImagePath = "/" + Path.Combine(folder, fileName).Replace("\\", "/");
            }

            model.Design.CreatedAt = DateTime.Now;

            // ----------------- Add to session cart ------------------
            var cartItem = new CartItem
            {
                ProductId = (int)model.Design.ProductId,
                ProductName = _context.Products.FirstOrDefault(p => p.ProductId == model.Design.ProductId)?.ProductName,
                Size = model.Design.Size,
                Color = Request.Form["colorOption"],
                DesignImageUrl = model.Design.UploadedImagePath,
                Price = _context.Products.FirstOrDefault(p => p.ProductId == model.Design.ProductId)?.BasePrice ?? 0,
                Quantity = 1
            };

            List<CartItem> cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            cart.Add(cartItem);
            HttpContext.Session.SetObject("Cart", cart);
            // --------------------------------------------------------

            return RedirectToAction("Index", "Cart");
        }

    }

}
