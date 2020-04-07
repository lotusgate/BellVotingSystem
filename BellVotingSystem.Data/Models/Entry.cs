using System;
using System.Collections.Generic;
using System.Text;

namespace BellVotingSystem.Data.Models
{
    public class Entry
    {
        public string Id { get; set; }

        public string Song { get; set; }

        public string Artist { get; set; }

        public int VoteCount { get; set; }
    }
}
