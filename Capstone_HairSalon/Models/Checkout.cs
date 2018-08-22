using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone_HairSalon.Models
{
    public class Checkout
    {
        [Key]
        [Display(Name = "Checkout")]
        public int Id { get; set; }
        [Display(Name = "Total")]
        public int Total { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}