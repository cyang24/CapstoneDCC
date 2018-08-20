using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Capstone_HairSalon.Models;

namespace Capstone_HairSalon.Controllers
{
    public class TextAPIsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void SendText(string userId, bool ConfirmAppointment)
        {
            
            var user = db.Customers.Where(c => c.UserId == userId).Select(c => c).FirstOrDefault();
            var customerName = user.FirstName;

            var stylist = db.Stylists.Where(c => c.UserId == userId).Select(c => c).FirstOrDefault();
            var stylistname = stylist.FirstName;

            string toPhoneNumber = "+1" + user.Phone;
            string toStylistNumber = "+1" + stylist.Phone;

            const string accountSid = "AC8f17440345184e555b817781af4d94cb";
            const string authToken = "66843904e1d2f4f6fb98a8c6b7173080";

            if (ConfirmAppointment != true)
                
            TwilioClient.Init(accountSid, authToken);
            
            var to = new PhoneNumber(toPhoneNumber);
            var message = MessageResource.Create(
              to,
              from: new PhoneNumber("+9203755309"),
              body: "Your appointment has been confirmed. Thank You!");
            Console.WriteLine(message.Sid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
