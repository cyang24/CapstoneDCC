﻿using DHTMLX.Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone_HairSalon.Models
{
    public class Event
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DHXJson(Alias = "Id")]
        public int Id { get; set; }

        [DHXJson(Alias = "text")]
        public string EventText { get; set; }

        [DHXJson(Alias = "start_date")]
        public DateTime Start_date { get; set; }

        [DHXJson(Alias = "end_date")]
        public DateTime End_date { get; set; }

        [ForeignKey("Stylist")]
        [Display(Name = "Stylist Name")]
        public int? StylistId { get; set; }
        public Stylist Stylist { get; set; }
        public IEnumerable<Stylist> Stylists { get; set; }
    }
}