using Microsoft.AspNetCore.Mvc;

namespace CustomShirtStore.Controllers
{
    public class ErrorController : Controller
    {
        
        [Route("Error/404")]
        public IActionResult Error404()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

        [Route("Error/500")]
        public IActionResult Error500()
        {
            Response.StatusCode = 500;
            return View("ServerError");
        }

        [Route("Error/{code}")]
        public IActionResult General(int code)
        {
            Response.StatusCode = code;
            return View("Generic");
        }
    }
}
