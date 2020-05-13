using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BellVotingSystem.WEB.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;
using System.Threading.Tasks;
using BellVotingSystem.Data;

namespace BellVotingSystem.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly VotingSystemDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(VotingSystemDbContext context, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!context.Roles.Any())
            {
                IdentityRole masterAdmin = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "MasterAdmin" };
                IdentityRole subAdmin = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "SubAdmin" };
                IdentityRole voter = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Voter" };

                await roleManager.CreateAsync(masterAdmin);
                await roleManager.CreateAsync(subAdmin);
                await roleManager.CreateAsync(voter);
            }

            if (!User.Identity.IsAuthenticated)
            {
                if (!context.Users.Any())
                {
                    return RedirectToPage("/Account/Register", new { area = "Identity" });
                }

                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return RedirectToAction("AllEntries", "Entry");
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        public IActionResult Help()
        {
            return View("Help");
        }

        public IActionResult About()
        {
            return View("About");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
