using CustomShirtStore.Models;
using CustomShirtStore.ViewModels;
using System;
using Microsoft.AspNetCore.Mvc;
using CustomShirtStore.Models;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

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
            _context.CustomerDesigns.Add(model.Design);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }

}
