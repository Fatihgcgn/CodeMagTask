using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
