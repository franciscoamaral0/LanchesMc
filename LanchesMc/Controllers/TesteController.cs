using Microsoft.AspNetCore.Mvc;

namespace LanchesMc.Controllers
{
    public class TesteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
