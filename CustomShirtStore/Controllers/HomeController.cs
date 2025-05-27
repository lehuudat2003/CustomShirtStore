using System.Diagnostics;
using CustomShirtStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

       

        [Authorize(Roles = "Customer")]
        public IActionResult User()
        {
            return View();
        }

        public IActionResult BangMauAo()
        {
            return View();
        }

        public IActionResult CachChonSizeAo()
        {
            return View();
        }

        public IActionResult CachThucThanhToan()
        {
            return View();
        }

        public IActionResult CongNgheIn()
        {
            return View();
        }

        public IActionResult CauHoi()
        {
            return View();
        }

        public IActionResult LienHe()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
