using System.Collections.Generic;

namespace BellVotingSystem.WEB.Models.Entries
{
    public class EntriesViewModel
    {
        public EntryViewModel Entry { get; set; }

        public ICollection<EntryViewModel> Entries { get; set; }
    }
}