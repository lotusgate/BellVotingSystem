using System;

namespace BellVotingSystem.Data.Models
{
    public class Entry
    {
        public string Id { get; set; }

        public string Song { get; set; }

        public string Artist { get; set; }

        public int VoteCount { get; set; }

        public bool IsBlacklisted { get; set; }

        public DateTime ChosenOn { get; set; }
    }
}
