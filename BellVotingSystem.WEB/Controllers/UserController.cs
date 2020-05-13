using BellVotingSystem.Data;
using BellVotingSystem.Data.Models;
using BellVotingSystem.WEB.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BellVotingSystem.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly VotingSystemDbContext context;
        private readonly UserManager<User> userManager;

        public UserController(VotingSystemDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AllUsers()
        {
            UsersViewModel model = new UsersViewModel();
            string id = userManager.GetUserId(HttpContext.User);

            model.User = await context.Users.Where(u => u.Id == id).Select(u => new UserViewModel 
            {
                Username = u.UserName,
                HasVoted = u.HasVoted,
            }).FirstOrDefaultAsync();

            model.Users = await context.Users.Where(u => u.Id != id).Select(u => new UserViewModel
            {
                Username = u.UserName,
                HasVoted = u.HasVoted,
            }).ToListAsync();

            return View(model);
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