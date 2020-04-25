using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BellVotingSystem.WEB.Models.User
{
    public class UserViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public bool HasVoted { get; set; }
    }
}
