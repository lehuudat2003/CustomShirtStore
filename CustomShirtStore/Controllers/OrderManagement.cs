using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class OrderManagement : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
