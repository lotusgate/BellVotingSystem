﻿using BellVotingSystem.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VotingSystem.WEB.Data
{
    public class VotingSystemDbContext : IdentityDbContext
    {
        public VotingSystemDbContext(DbContextOptions<VotingSystemDbContext> options)
            : base(options)
        { }
        public DbSet<Entry> Entries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BellVotingSystem;Trusted_Connection=True;");
        }
    }
}
