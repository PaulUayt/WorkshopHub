using Microsoft.AspNetCore.Mvc;

namespace WorkshopHub.Web.Controllers
{
    public class TrainersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
