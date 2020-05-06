using System;

namespace BellVotingSystem.WEB.Models
{
    public class EntryViewModel
    {
        public int Id { get; set; }

        public string Song { get; set; }

        public int VoteCount { get; set; }

        public DateTime ChosenOn { get; set; }

        public bool IsBlacklisted { get; set; }
    }
}