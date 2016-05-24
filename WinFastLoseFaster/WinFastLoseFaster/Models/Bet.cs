using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WinFastLoseFaster.Models
{
    public class Bet
    {
        public int Id { get; set; }

        [ForeignKey("user")]
        public int UserId { get; set; }

        [Required]
        public int Wager { get; set; }

        public virtual Game game { get; set; }

        public virtual User user { get; set; }
    }
}