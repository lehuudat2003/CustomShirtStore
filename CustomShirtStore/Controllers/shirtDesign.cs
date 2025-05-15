using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class shirtDesign : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
