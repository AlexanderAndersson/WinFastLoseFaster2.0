using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WinFastLoseFaster.Models
{
    public class WinFastLoseFasterContext : DbContext
    {
        public WinFastLoseFasterContext() : base ("name=LocalDb")
        {}

        public DbSet<User> Users { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Winner> Winners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(g => g.users)
                .WithMany(u => u.Games);

            //modelBuilder.Entity<Game>().HasMany(g => g.Winners);

            modelBuilder.Entity<Bet>()
                .HasRequired(b => b.game)
                .WithMany(g => g.Userbets);

            modelBuilder.Entity<Winner>()
                .HasRequired(w => w.game)
                .WithMany(g => g.Winners);

            modelBuilder.Entity<Bet>()
                .HasRequired(b => b.user)
                .WithMany(u => u.bets);


        }
    }
}