using CustomShirtStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;

namespace CustomShirtStore.Controllers
{
    public class LoiChucController : Controller
    {
        private readonly Exe201Context _context;
        private readonly IWebHostEnvironment _env;

        public LoiChucController(Exe201Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var card = await _context.BirthdayCards
                .OrderByDescending(c => c.Id)
                .FirstOrDefaultAsync();

            if (card == null)
                return RedirectToAction(nameof(Create));

            return View(card);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BirthdayCard card)
        {
            // In lỗi để debug nếu có
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"[ModelState Error] {modelState.Key}: {error.ErrorMessage}");
                    }
                }

                ModelState.AddModelError(string.Empty, "Vui lòng kiểm tra lại thông tin nhập.");
                return View(card);
            }

            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            async Task<string> SaveFileAsync(IFormFile file)
            {
                if (file == null || file.Length == 0)
                    return null;

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return "/uploads/" + fileName;
            }

            try
            {
                card.ImagePath = await SaveFileAsync(card.ImageUpload);
                card.BackgroundImagePath = await SaveFileAsync(card.BackgroundImageUpload);
                card.NoteImagePath = await SaveFileAsync(card.NoteImageUpload);
                card.AudioPath = await SaveFileAsync(card.AudioUpload);

                _context.BirthdayCards.Add(card);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu dữ liệu: " + ex.Message);
                ModelState.AddModelError(string.Empty, "Lỗi khi lưu dữ liệu: " + ex.Message);
                return View(card);
            }
        }
        public async Task<IActionResult> List()
        {
            var cards = await _context.BirthdayCards
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return View(cards);
        }

        public async Task<IActionResult> Details(int id)
        {
            var card = await _context.BirthdayCards.FirstOrDefaultAsync(c => c.Id == id);
            if (card == null)
                return NotFound();

            return View(card);
        }


    }
}
