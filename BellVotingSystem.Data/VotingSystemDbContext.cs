using BellVotingSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BellVotingSystem.Data
{
    public class VotingSystemDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public VotingSystemDbContext(DbContextOptions<VotingSystemDbContext> options)
            : base(options)
        { }

        public DbSet<BlacklistedSong> BlacklistedSongs { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public DbSet<UsedSong> UsedSongs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.CONNECTION_STRING);
        }
    }
}
