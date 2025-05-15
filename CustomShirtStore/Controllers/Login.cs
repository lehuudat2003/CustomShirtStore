using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
