﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone_HairSalon.Models
{
    public class ServiceTotal
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Service")]
        public int? ServiceId { get; set; }
        public Services Service { get; set; }
        public IEnumerable<Services> Services { get; set; }


        [ForeignKey("PaymentId")]
        public int? Payment_Id { get; set; }
        public Payment PaymentId { get; set; }

        [Display(Name = "Additional Fees")]
        public int? AdditionalFees { get; set; }






    }
}