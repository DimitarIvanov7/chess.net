using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.WebApi.Controllers
{
    public class PlayersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
