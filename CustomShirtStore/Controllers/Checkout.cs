using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class Checkout : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
