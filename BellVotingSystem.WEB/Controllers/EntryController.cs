using BellVotingSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using BellVotingSystem.Data;
using Microsoft.EntityFrameworkCore;
using BellVotingSystem.WEB.Models;
using System.Threading.Tasks;

namespace BellVotingSystem.WEB.Controllers
{
    public class EntryController : Controller
    {
        private readonly VotingSystemDbContext context;

        public EntryController(VotingSystemDbContext context)
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
            Entry entry = await context.Entries.SingleOrDefaultAsync(e => e.Id == id);

            EntryViewModel entryViewModel = new EntryViewModel()
            {
                Song = entry.Song,
                Artist = entry.Artist,
                VoteCount = entry.VoteCount
            };

            return View("", entryViewModel);
        }
    }
}