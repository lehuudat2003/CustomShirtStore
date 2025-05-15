using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class AdminDashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
