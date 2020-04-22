using Microsoft.AspNetCore.Mvc;

namespace BellVotingSystem.WEB.Controllers
{
    public class EntryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}