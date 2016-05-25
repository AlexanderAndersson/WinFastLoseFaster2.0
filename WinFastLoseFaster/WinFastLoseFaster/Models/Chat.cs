using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinFastLoseFaster.Models
{
    public class Chat
    {
        [Key]
        public int MessageId { get; set; }

        //[ForeignKey("user")]
        //public int UserId { get; set; }

        [StringLength(150)]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual User user { get; set; }
    }
}