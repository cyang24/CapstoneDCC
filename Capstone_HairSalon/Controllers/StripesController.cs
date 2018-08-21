using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stripe;
using Capstone_HairSalon.Models;

namespace Capstone_HairSalon.Controllers
{
    public class StripesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stripes
        public ActionResult Index()
        {
            StripeConfiguration.SetApiKey("sk_test_IGQjMw0yYI0CtHTQQcjyq0yk");
            var stripePublishKey = "pk_test_88RuAbuVAFajNdiid6qwOfdQ";
            ViewBag.StripePublishKey = stripePublishKey;

            return View();
        }
        public ActionResult Charge(string stripeEmail, string stripeToken, int chargeTotal)
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = chargeTotal,
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}