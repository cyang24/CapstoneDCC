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

        public void SendText(Appointment appointment)
        {

            //var user = db.Customers.Where(c => c.UserId == userId).Select(c => c).FirstOrDefault();
            //var customerName = user.FirstName;

            //var stylist = db.Stylists.Where(c => c.UserId == userId).Select(c => c).FirstOrDefault();
            //var stylistname = stylist.FirstName;

            //string toPhoneNumber = "+1" + appointment.PhoneNumber;
            string toPhoneNumber = "+1" + "9202424833";
            //string toStylistNumber = "+1" + stylist.Phone;



            if (appointment.ConfirmAppointment == true)
            {
                // Find your Account Sid and Token at twilio.com/console
                const string accountSid = "AC8f17440345184e555b817781af4d94cb";
                const string authToken = "66843904e1d2f4f6fb98a8c6b7173080";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: appointment.FirstName + " " + appointment.LastName +" Your appointment has been confirmed on " + appointment.Date +" at " + appointment.TimeRequest+ ".",
                    from: new Twilio.Types.PhoneNumber("+19203755309"),
                    to: new Twilio.Types.PhoneNumber(toPhoneNumber)
                );

                Console.WriteLine(message.Sid);
                //Console.ReadLine();

            }
            else if (appointment.DenyAppointment == true)
            {
                const string accountSid = "AC8f17440345184e555b817781af4d94cb";
                const string authToken = "66843904e1d2f4f6fb98a8c6b7173080";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Your requested appointment time is unavailable. Please select another choice. We apologize for any inconvience.",
                    from: new Twilio.Types.PhoneNumber("+19203755309"),
                    to: new Twilio.Types.PhoneNumber(toPhoneNumber)
                );

                Console.WriteLine(message.Sid);

            }
            else if (appointment.Accept_Terms == true)
            {
                const string accountSid = "AC8f17440345184e555b817781af4d94cb";
                const string authToken = "66843904e1d2f4f6fb98a8c6b7173080";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "An Appointment has been requested by " + appointment.FirstName + " " + appointment.LastName + " on " + appointment.Date + " at " + appointment.TimeRequest ,
                    from: new Twilio.Types.PhoneNumber("+19203755309"),
                    to: new Twilio.Types.PhoneNumber("+19202424833")
                );

                Console.WriteLine(message.Sid);

            }
            else if (appointment.DenyAppointment == true)
            {
                const string accountSid = "AC8f17440345184e555b817781af4d94cb";
                const string authToken = "66843904e1d2f4f6fb98a8c6b7173080";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Your requested appointment time is unavailable. Please select another choice. We apologize for any inconvience.",
                    from: new Twilio.Types.PhoneNumber("+19203755309"),
                    to: new Twilio.Types.PhoneNumber("+19202424833")
                );

                Console.WriteLine(message.Sid);

            }
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
