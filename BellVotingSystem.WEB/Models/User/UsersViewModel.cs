using System.Collections.Generic;

namespace BellVotingSystem.WEB.Models.User
{
    public class UsersViewModel
    {
        public UserViewModel User { get; set; }

        public ICollection<UserViewModel> Users { get; set; }
    }
}