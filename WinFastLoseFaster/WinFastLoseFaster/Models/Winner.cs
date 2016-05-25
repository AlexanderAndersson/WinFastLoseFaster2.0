using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinFastLoseFaster.Models
{
    public class Winner
    {
        public int Id { get; set; }

        public virtual User WinningUser { get; set; }

        public int TotalAmount { get; set; }

        public virtual Game game { get; set; }
    }
}