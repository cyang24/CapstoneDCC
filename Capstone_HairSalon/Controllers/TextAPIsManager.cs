using Capstone_HairSalon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Capstone_HairSalon.Controllers
{
    public class TextAPIsManager : Controller
    {
        // GET: TextAPIsManager
        private ApplicationDbContext db = new ApplicationDbContext();

        public void SendText(Stylist stylist)
        {
            //string toPhoneNumber = "+1" + "9202424833";
            string toStylistNumber = "+1" + stylist.Phone;

            if (stylist.RentDue == true)
            {
                const string accountSid = "AC8f17440345184e555b817781af4d94cb";
                const string authToken = "66843904e1d2f4f6fb98a8c6b7173080";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Just a friendly reminder that your rent is due soon " + stylist.FirstName + ".",
                    from: new Twilio.Types.PhoneNumber("+19203755309"),
                    to: new Twilio.Types.PhoneNumber(toStylistNumber)
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