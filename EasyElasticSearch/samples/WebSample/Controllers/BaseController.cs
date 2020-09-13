using Microsoft.AspNetCore.Mvc;

namespace WebSample.Controllers
{
    public class BaseController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}