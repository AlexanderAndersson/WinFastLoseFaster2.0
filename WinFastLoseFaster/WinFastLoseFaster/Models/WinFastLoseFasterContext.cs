using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WinFastLoseFaster.Models
{
    public class WinFastLoseFasterContext : DbContext
    {
        public WinFastLoseFasterContext() : base ("WinFastLoseFasterDb")
        {}

        public DbSet<User> Users { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Winner> Winners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasMany(g => g.user).WithMany(g => g.Games);
            modelBuilder.Entity<Game>().HasMany(g => g.WinnerId);
            modelBuilder.Entity<Game>().HasMany(u => u.Userbet);
        }
    }
}