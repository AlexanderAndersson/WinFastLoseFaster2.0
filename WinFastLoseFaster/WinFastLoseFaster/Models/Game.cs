using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Script.Serialization;

namespace WinFastLoseFaster.Models
{
    public class Game
    {
        public int Id { get; set; }

        
        public virtual IList<User> users {get; set;}

        
        public virtual IList<Bet> Userbets { get; set; }

        
        public virtual IList<Winner> Winners { get; set; }

        public DateTime Timestamp { get; set; }

        public bool GameActive { get; set; }

        public GameEnum Gametype { get; set; }
        public enum GameEnum { Coinflip, Jackpot, Roulette }

        
    }
}