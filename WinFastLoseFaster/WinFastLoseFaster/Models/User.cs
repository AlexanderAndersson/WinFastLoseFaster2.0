using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WinFastLoseFaster.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [ScriptIgnore]
        public virtual IList<Game> Games { get; set; }

        public int Deposit { get; set; }

        public int Withdrawal { get; set; }

        public int Credits { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Must be a correct e-mail")]
        public string Mail { get; set; }

        public string Picture { get; set; }

        [ScriptIgnore]
        public virtual IList<Bet> bets { get; set; }
    }
}