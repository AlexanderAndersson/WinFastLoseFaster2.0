using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WinFastLoseFaster.Models
{
    public class Winner
    {
        public int Id { get; set; }

        [ScriptIgnore]
        public virtual User WinningUser { get; set; }

        public int TotalAmount { get; set; }

        [ScriptIgnore]
        public virtual Game game { get; set; }
    }
}