using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WinFastLoseFaster.Models
{
    public class Game
    {
        public int Id { get; set; }

        public virtual IList<User> user {get; set;}

        public IList<Bet> Userbet { get; set; }

        public IList<Winner> WinnerId { get; set; }

        public DateTime Timestamp { get; set; }
        public GameEnum Gametype { get; set; }
        public enum GameEnum { Coinflip, Jackpot, Roulette }

        
    }
}