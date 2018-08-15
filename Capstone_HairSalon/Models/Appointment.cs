using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            [Display(Name = "Appointment Time")]
            public DateTime Time { get; set; }

            //[Required]
            //[Display(Name = "Appointment Day")]
            //public string Day { get; set; }

        //[Required]
        //public string Timezone { get; set; }

        //[Display(Name = "Created at")]
        //public DateTime CreatedAt { get; set; }

        public static int ReminderTime = 1440;
    }
}