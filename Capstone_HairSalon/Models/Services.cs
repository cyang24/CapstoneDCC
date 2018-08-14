using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone_HairSalon.Models
{
    public class Services
    {
        [Key]

        public int Id { get; set; }

        [Display(Name = "Services")]
        public string Name { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}