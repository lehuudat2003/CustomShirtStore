using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class ProductManagement : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
