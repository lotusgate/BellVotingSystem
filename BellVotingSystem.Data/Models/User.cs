using Microsoft.AspNetCore.Identity;

namespace BellVotingSystem.Data.Models
{
    public class User : IdentityUser<string>
    {
        public bool HasVoted { get; set; }
    }
}
