using BellVotingSystem.Data;
using BellVotingSystem.Data.Models;
using BellVotingSystem.WEB.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BellVotingSystem.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly VotingSystemDbContext context;

        public UserController(VotingSystemDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Read")]
        public async Task<IActionResult> Read(string id)
        {
            User user = await context.Users.SingleOrDefaultAsync(u => u.Id == id);

            UserViewModel userViewModel = new UserViewModel()
            {
                Username = user.UserName,
                HasVoted = user.HasVoted
            };

            return View("", userViewModel);
        }
    }
}