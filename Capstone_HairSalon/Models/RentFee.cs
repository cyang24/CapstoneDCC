using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone_HairSalon.Models
{
    public class RentFee
    {
        [Key]

        public int Id { get; set; }

        [Display(Name = "Rent Fee")]
        public int RentalFee{ get; set; }

    }
}