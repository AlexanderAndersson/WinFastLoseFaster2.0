using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinFastLoseFaster.Models
{
    public class Winner
    {
        public int Id { get; set; }

        public User WinnerId { get; set; }

        public int TotalAmount { get; set; }
    }
}