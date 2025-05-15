using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class Cart : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
