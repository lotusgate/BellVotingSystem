using System.Collections.Generic;

namespace BellVotingSystem.WEB.Models.Users
{
    public class EntriesViewModel
    {
        public UserViewModel User { get; set; }

        public ICollection<UserViewModel> Users { get; set; }
    }
}