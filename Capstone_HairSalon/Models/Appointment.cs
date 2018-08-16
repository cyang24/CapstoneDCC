using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone_HairSalon.Models
{
    public class Appointment
    {
            [Key]
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required, Phone, Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Appointment Date")]
            public string Time { get; set; }

            
            [ForeignKey("Stylist")]
            [Display(Name = "Preferred Stylists")]
            public int? StylistId { get; set; }
            public Stylist Stylist { get; set; }
            public IEnumerable<Stylist> Stylists { get; set; }


            [Required]
            [Display(Name = "Preferred Time")]
            public string TimeRequest { get; set; }

            //public static int ReminderTime = 1440;
    }
}