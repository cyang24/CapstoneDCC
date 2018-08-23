using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone_HairSalon.Models;
using Microsoft.AspNet.Identity;
using Stripe;

namespace Capstone_HairSalon.Controllers
{
    public class RentTransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RentTransactions
        public ActionResult Index()
        {
            var rentTransactions = db.RentTransactions.Include(r => r.RentPayment);
            return View(rentTransactions.ToList());
        }

        // GET: RentTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentTransaction rentTransaction = db.RentTransactions.Find(id);
            if (rentTransaction == null)
            {
                return HttpNotFound();
            }
            return View(rentTransaction);
        }

        // GET: RentTransactions/Create
        public ActionResult Create()
        {
            ViewBag.RentPaymentId = new SelectList(db.RentPayments, "Id", "UserId");
            return View();
        }

        // POST: RentTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RentPaymentId,RentalFee")] RentTransaction rentTransaction)
        {
            var rentPaymentId = db.RentPayments.OrderByDescending(c => c.Id).FirstOrDefault();
            rentPaymentId.UserId = User.Identity.GetUserId();
            var result = db.RentPayments.OrderByDescending(c => c.Id).Select(c => c.Id).First();
            rentTransaction.RentPaymentId = result;


            if (ModelState.IsValid)
            {
                db.RentTransactions.Add(rentTransaction);
                db.SaveChanges();
                return RedirectToAction("SingleIndex");
            }

            ViewBag.RentPaymentId = new SelectList(db.RentPayments, "Id", "UserId", rentTransaction.RentPaymentId);
            return View(rentTransaction);
        }

        public ActionResult PayRentCharge()
        {
            RentPayment rentPayment = new RentPayment();
            db.RentPayments.Add(rentPayment);
            db.SaveChanges();
            return RedirectToAction("Create");
        }
        public ActionResult SingleIndex(string searchString)
        {
            var latestRentPayment = db.RentPayments.OrderByDescending(c => c.Id).FirstOrDefault();
            var userId = User.Identity.GetUserId();
            var rentpaid = db.RentTransactions.Include(o => o.RentPayment).Where(o => o.RentPayment.UserId == userId && o.RentPaymentId == latestRentPayment.Id).ToList();
            var currentServicefees = db.RentTransactions.OrderByDescending(c => c.Id).FirstOrDefault();

            var total = 0;

            rentpaid.First().RentPayment.Total = total;

            ViewBag.TotalAmount = total + currentServicefees.RentalFee;


            StripeConfiguration.SetApiKey("sk_test_IGQjMw0yYI0CtHTQQcjyq0yk");
            var stripePublishKey = "pk_test_88RuAbuVAFajNdiid6qwOfdQ";
            ViewBag.StripePublishKey = stripePublishKey;

            //RedirectToAction("StripeIndex");
            return View(rentpaid);

        }
            // GET: RentTransactions/Edit/5
            public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentTransaction rentTransaction = db.RentTransactions.Find(id);
            if (rentTransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.RentPaymentId = new SelectList(db.RentPayments, "Id", "UserId", rentTransaction.RentPaymentId);
            return View(rentTransaction);
        }

        // POST: RentTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RentPaymentId,RentalFee")] RentTransaction rentTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RentPaymentId = new SelectList(db.RentPayments, "Id", "UserId", rentTransaction.RentPaymentId);
            return View(rentTransaction);
        }

        // GET: RentTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentTransaction rentTransaction = db.RentTransactions.Find(id);
            if (rentTransaction == null)
            {
                return HttpNotFound();
            }
            return View(rentTransaction);
        }

        // POST: RentTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentTransaction rentTransaction = db.RentTransactions.Find(id);
            db.RentTransactions.Remove(rentTransaction);
            db.SaveChanges();
            return RedirectToAction("SingleIndex", "ServiceTotals");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Charge(string stripeEmail, string stripeToken)
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
                Amount = 500,//charge in cents
                Description = "Your Total",
                Currency = "usd",
                CustomerId = customer.Id
            });

            return View();
        }
    }
}
