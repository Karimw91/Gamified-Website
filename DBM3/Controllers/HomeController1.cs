using Microsoft.AspNetCore.Mvc;

namespace DBM3.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
