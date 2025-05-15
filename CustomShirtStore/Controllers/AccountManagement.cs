using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class AccountManagement : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
