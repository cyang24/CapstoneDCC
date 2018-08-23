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
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }

            [Required, Phone, Display(Name = "Phone number")]
            public string Phone { get; set; }

            [Required]
            [Display(Name = "Appointment Date")]
            public string Date { get; set; }

            
            [ForeignKey("Stylist")]
            [Display(Name = "Preferred Stylist")]
            public int? StylistId { get; set; }
            public Stylist Stylist { get; set; }
            public IEnumerable<Stylist> Stylists { get; set; }

            [ForeignKey("Service")]
            [Display(Name = "Type of Service")]
            public int? ServiceId { get; set; }
            public Services Service{ get; set; }
            public IEnumerable<Services> Services { get; set; }


            [Required]
            [Display(Name = "Preferred Time")]
            public string TimeRequest { get; set; }
            [Display(Name = "Confirm Appointment")]
            public bool ConfirmAppointment { get; set; }
            [Display(Name = "Deny Appointment")]
            public bool DenyAppointment { get; set; }
            [Required]
            [Display(Name = "Accept Terms")]
            public bool Accept_Terms { get; set; }
            [Display(Name = "Reminder Text")]
            public bool ReminderText { get; set; }   
    }
}