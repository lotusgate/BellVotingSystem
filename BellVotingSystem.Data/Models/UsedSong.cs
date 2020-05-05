using System;

namespace BellVotingSystem.Data.Models
{
    public class UsedSong
    {
        public string Id { get; set; }

        public string Song { get; set; }

        public DateTime ChosenOn { get; set; }
    }
}