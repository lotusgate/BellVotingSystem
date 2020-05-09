using BellVotingSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using BellVotingSystem.Data;
using Microsoft.EntityFrameworkCore;
using BellVotingSystem.WEB.Models;
using System.Threading.Tasks;
using BellVotingSystem.WEB.Models.Entry;
using System.Linq;
using System;
using BellVotingSystem.WEB.Models.Entries;

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
        [ActionName("Add")]
        public IActionResult Add()
        {
            EntryCreateViewModel model = new EntryCreateViewModel();

            return View("AddEntry", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(EntryCreateViewModel model)
        {
            bool hasError = false;

            await foreach (Entry entry in context.Entries)
            {
                if (entry.Song == model.Song)
                {
                    ModelState.AddModelError(nameof(model.Song), "This song is already nominated.");
                    hasError = true;
                    break;
                }
            }

            if (!hasError)
            {
                await foreach (BlacklistedSong blacklistedSong in context.BlacklistedSongs)
                {
                    if (blacklistedSong.Song == model.Song)
                    {
                        ModelState.AddModelError(nameof(model.Song), "This song has been blacklisted. Choose another one.");
                        hasError = true;
                        break;
                    }
                }
            }

            if (!hasError)
            {
                foreach (UsedSong usedSong in context.UsedSongs.OrderByDescending(s => s.ChosenOn).Take(24))
                {
                    if (usedSong.Song == model.Song)
                    {
                        ModelState.AddModelError(nameof(model.Song), "This song has been used in the last 24 weeks. Choose another one.");
                        break;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                Entry entry = new Entry
                {
                    Id = Guid.NewGuid().ToString(),
                    Song = model.Song,
                    VoteCount = 1,
                };

                context.Entries.Add(entry);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View("AddEntry", model);
        }

        public async Task<IActionResult> AddToBlacklist(EntriesViewModel model)
        {
            foreach (EntryViewModel entry in model.Entries)
            {
                if (entry.IsBlacklisted)
                {
                    BlacklistedSong bs = new BlacklistedSong
                    {
                        Id = Guid.NewGuid().ToString(),
                        Song = entry.Song,
                    };

                    await context.BlacklistedSongs.AddAsync(bs);

                    Entry e = await context.Entries.FirstOrDefaultAsync(e => e.Id == entry.Id);
                    if (e != null)
                    {
                        context.Entries.Remove(e);
                    }

                    await context.SaveChangesAsync();
                }
            }

            return View("Index", "Home");
        }

        [HttpGet]
        [ActionName("Read")]
        public async Task<IActionResult> Read(string id)
        {
            Entry entry = await context.Entries.SingleOrDefaultAsync(e => e.Id == id);

            EntryViewModel entryViewModel = new EntryViewModel()
            {
                Song = entry.Song,
                VoteCount = entry.VoteCount
            };

            return View("", entryViewModel);
        }
    }
}