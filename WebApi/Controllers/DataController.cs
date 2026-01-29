using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
