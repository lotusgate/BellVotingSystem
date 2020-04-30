using System;

namespace BellVotingSystem.WEB.Models
{
    public class EntryViewModel
    {
        public string Song { get; set; }

        public string Artist { get; set; }

        public int VoteCount { get; set; }

        public DateTime ChosenOn { get; set; }
    }
}
