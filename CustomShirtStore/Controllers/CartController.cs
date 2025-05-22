using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
