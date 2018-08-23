using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone_HairSalon.Models
{
    public class RentTransaction
    { 

    [Key]
    public int Id { get; set; }


    [ForeignKey("RentPayment")]
    public int? RentPaymentId { get; set; }
    public RentPayment RentPayment { get; set; }

    [Display(Name = "Rent Fee")]
    public int RentalFee { get; set; }
    }
}