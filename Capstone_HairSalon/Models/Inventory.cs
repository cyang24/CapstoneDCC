using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone_HairSalon.Models
{
    public class Inventory
    {

        [Key]

        public int Id { get; set; }

        [Display(Name = "Item")]
        public string Name { get; set; }

        [Display(Name = "Count")]
        public double ItemCount { get; set; }
    }
}