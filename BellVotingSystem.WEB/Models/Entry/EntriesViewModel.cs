using System.Collections.Generic;

namespace BellVotingSystem.WEB.Models.Entry
{
    public class EntriesViewModel
    {
        public EntryViewModel Entry { get; set; }

        public ICollection<EntryViewModel> Entries { get; set; }
    }
}