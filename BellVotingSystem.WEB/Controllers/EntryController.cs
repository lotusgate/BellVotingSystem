using BellVotingSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using BellVotingSystem.Data;
using Microsoft.EntityFrameworkCore;
using BellVotingSystem.WEB.Models;
using System.Threading.Tasks;
using BellVotingSystem.WEB.Models.Entry;
using System.Linq;
using System;

namespace BellVotingSystem.WEB.Controllers
{
    public class EntryController : Controller
    {
        private readonly VotingSystemDbContext context;

        public EntryController(VotingSystemDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> AllEntries()
        {
            EntriesViewModel model = new EntriesViewModel
            {
                Entry = (EntryViewModel)context.UsedSongs.OrderByDescending(s => s.ChosenOn).Take(1)
                .Select(s => new EntryViewModel
                {
                    Id = s.Id,
                    Song = s.Song,
                    ChosenOn = s.ChosenOn,
                    IsBlacklisted = false,
                }),

                Entries = await context.Entries.OrderByDescending(e => e.VoteCount).Select(e => new EntryViewModel
                {
                    Id = e.Id,
                    Song = e.Song,
                    VoteCount = e.VoteCount,
                    IsBlacklisted = false,
                }).ToListAsync()
            };

            return View("Index", model);
        }

        public async Task<IActionResult> AllSongs()
        {
            EntriesViewModel model = new EntriesViewModel();

            model.Entries = await context.Entries.Select(e => new EntryViewModel()
            {
                Id = e.Id,
                Song = e.Song,
                VoteCount = e.VoteCount,
                ChosenOn = new DateTime(),
                IsBlacklisted = false,
            }).ToListAsync();

            return View(model);
        }

        public async Task<IActionResult> AllBlacklistedSongs()
        {
            EntriesViewModel model = new EntriesViewModel();

            model.Entries = await context.BlacklistedSongs.Select(e => new EntryViewModel()
            {
                Id = e.Id,
                Song = e.Song,
                VoteCount = 0,
                ChosenOn = new DateTime(),
                IsBlacklisted = true,
            }).ToListAsync();

            return View("Blacklist");
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

                return RedirectToAction(nameof(AllSongs));
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

            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Vote(string id)
        {
            Entry entry = await context.Entries.SingleOrDefaultAsync(e => e.Id == id);
            entry.VoteCount++;
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(AllSongs));
        }
    }
}