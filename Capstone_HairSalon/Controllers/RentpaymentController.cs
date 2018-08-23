using Capstone_HairSalon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Capstone_HairSalon.Controllers
{
    public class RentPaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiceTotals
        public ActionResult Index()
        {
            //var serviceTotals = db.ServiceTotals.Include(s => s.Service);
            return View(db.RentPayments.ToList());
        }

        // GET: ServiceTotals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentPayment rentPayment = db.RentPayments.Find(id);
            if (rentPayment == null)
            {
                return HttpNotFound();
            }
            return View(rentPayment);
        }

        //public ActionResult StylistCustomPayment()
        //{
        //    var stylistId = User.Identity.GetUserId();
        //    var stylist = db.Stylists.Where(e => e.UserId == stylistId).FirstOrDefault();
        //    var checkout = db.Checkouts.Where(o => o.Id == stylist.CheckoutId);
        //    return View(checkout);
        //}

        // GET: ServiceTotals/Create
        public ActionResult Create()
        {
            ViewBag.RentPaymentId = new SelectList(db.RentPayments, "Id", "Total");
            return View();
        }

        // POST: ServiceTotals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Total,UserId")] RentPayment rentPayment)
        {
            if (ModelState.IsValid)
            {
                db.RentPayments.Add(rentPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RentPaymentId = new SelectList(db.RentPayments, "Id", "Total", rentPayment.Id);
            return View(rentPayment);
        }

        // GET: ServiceTotals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentPayment rentPayment = db.RentPayments.Find(id);
            if (rentPayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.RentPaymentId = new SelectList(db.RentPayments, "Id", "Total", rentPayment.Id);
            return View(rentPayment);
        }

        // POST: ServiceTotals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Total")] RentPayment rentPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RentPaymentId = new SelectList(db.RentPayments, "Id", "Total", rentPayment.Id);
            return View(rentPayment);
        }

        // GET: ServiceTotals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentPayment rentPayment = db.RentPayments.Find(id);
            if (rentPayment == null)
            {
                return HttpNotFound();
            }
            return View(rentPayment);
        }

        // POST: ServiceTotals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentPayment rentPayment = db.RentPayments.Find(id);
            db.RentPayments.Remove(rentPayment);
            db.SaveChanges();
            return RedirectToAction("Index");
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