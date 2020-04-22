using Microsoft.AspNetCore.Mvc;

namespace BellVotingSystem.WEB.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}