using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinFastLoseFaster.Models
{
    public class GhettoListCoinflipGames
    {

        public GhettoListCoinflipGames()
        {

        }

        public int GameId { get; set; }

        public string Creater { get; set; }

        public int Wager { get; set; }

        //public DateTime Timestamp { get; set; }

        public string ShortDate { get; set; }

        public string ShortTime { get; set; }

        public string PictureURL { get; set; }


    }
}