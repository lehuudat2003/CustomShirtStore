using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class Message : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
