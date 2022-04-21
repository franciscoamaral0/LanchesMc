using Microsoft.AspNetCore.Mvc;

namespace LanchesMc.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
