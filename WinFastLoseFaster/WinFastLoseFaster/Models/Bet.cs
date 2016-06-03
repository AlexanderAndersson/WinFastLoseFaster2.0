using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WinFastLoseFaster.Models
{
    public class Bet
    {
        public int Id { get; set; }

        //[ForeignKey("user")]
        //public int UserId { get; set; }

        
        public int Wager { get; set; }

        [ScriptIgnore]
        public virtual Game game { get; set; }

        [ScriptIgnore]
        public virtual User user { get; set; }

        public string ToJson()
        {
            string json = "";
            json += "\"Wager\": " + Wager;

            return json;
        }
    }
}