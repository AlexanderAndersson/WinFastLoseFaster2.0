using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinFastLoseFaster.Models
{
    public class GhettoCoinflipGameStatus
    {

        public int gameId { get; set; }

        public bool gameActive { get; set; }

        public string CreaterUsername { get; set; }

        public string JoinerUsername { get; set; }

        public string WinnerUsername { get; set; }

        public string CreaterPicture { get; set; }

        public string JoinerPicture { get; set; }

        public string WinnerPicture { get; set; }


    }
}