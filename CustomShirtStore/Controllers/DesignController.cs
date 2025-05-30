using CustomShirtStore.Models;
using CustomShirtStore.ViewModels;
using System;
using Microsoft.AspNetCore.Mvc;
using CustomShirtStore.Models;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using CustomShirtStore.Extensions;
using System.Text.RegularExpressions;

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
        public async Task<IActionResult> SaveDesign(ShirtDesignViewModel model, IFormFile? uploadedImage, string? DesignImageBase64)
        {
            string? designImageUrl = null;

            // Nếu có ảnh base64 từ canvas
            if (!string.IsNullOrEmpty(DesignImageBase64))
            {
                var base64Data = Regex.Match(DesignImageBase64, @"data:image/(?<type>.+?);base64,(?<data>.+)").Groups["data"].Value;
                var imageBytes = Convert.FromBase64String(base64Data);
                var fileName = Guid.NewGuid().ToString() + ".png";
                var folder = Path.Combine("images", "customerdesign");
                var folderPath = Path.Combine(_env.WebRootPath, folder);
                var filePath = Path.Combine(folderPath, fileName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                designImageUrl = "/" + Path.Combine(folder, fileName).Replace("\\", "/");
            }

            // Nếu có ảnh upload từ máy (ưu tiên ảnh này)
            if (uploadedImage != null)
            {
                string folder = Path.Combine("images", "customerdesign");
                string fileName = Guid.NewGuid() + Path.GetExtension(uploadedImage.FileName);
                string path = Path.Combine(_env.WebRootPath, folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await uploadedImage.CopyToAsync(stream);

                model.Design.UploadedImagePath = "/" + Path.Combine(folder, fileName).Replace("\\", "/");
                designImageUrl = model.Design.UploadedImagePath;
            }

            model.Design.Color = Request.Form["colorOption"];
            model.Design.CreatedAt = DateTime.Now;
            model.Design.DesignImageUrl = designImageUrl;

            // ✅ Lưu CustomerDesign vào database
            _context.CustomerDesigns.Add(model.Design);
            await _context.SaveChangesAsync();

            // Thêm vào session cart
            var product = _context.Products.FirstOrDefault(p => p.ProductId == model.Design.ProductId);

            var cartItem = new CartItem
            {
                ProductId = (int)model.Design.ProductId,
                ProductName = product?.ProductName,
                Size = model.Design.Size,
                Color = model.Design.Color,
                DesignImageUrl = designImageUrl,
                Price = product?.BasePrice ?? 0,
                Quantity = 1
            };

            List<CartItem> cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            cart.Add(cartItem);
            HttpContext.Session.SetObject("Cart", cart);

            return RedirectToAction("Index", "Cart");
        }


    }

}
