using Capstone_HairSalon.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Capstone_HairSalon.Controllers
{
    public class CheckoutsController : Controller
    {
            private ApplicationDbContext db = new ApplicationDbContext();

            // GET: ServiceTotals
            public ActionResult Index()
            {
                //var serviceTotals = db.ServiceTotals.Include(s => s.Service);
                return View(db.Checkouts.ToList());
            }

            // GET: ServiceTotals/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Checkout checkout = db.Checkouts.Find(id);
                if (checkout == null)
                {
                    return HttpNotFound();
                }
                return View(checkout);
            }

            public ActionResult StylistCustomPayment()
            {
                var stylistId = User.Identity.GetUserId();
                var stylist = db.Stylists.Where(e => e.UserId == stylistId).FirstOrDefault();
                var checkout = db.Checkouts.Where(o => o.Id == stylist.CheckoutId);
                return View(checkout);
            }

        // GET: ServiceTotals/Create
        public ActionResult Create()
            {
                ViewBag.CheckoutId = new SelectList(db.Checkouts, "Id", "Total");
                return View();
            }

            // POST: ServiceTotals/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "Id,Total,UserId")] Checkout checkout)
            {
                if (ModelState.IsValid)
                {
                    db.Checkouts.Add(checkout);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.CheckoutId = new SelectList(db.Checkouts, "Id", "Total", checkout.Id);
                return View(checkout);
            }

            // GET: ServiceTotals/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Checkout checkout = db.Checkouts.Find(id);
                if (checkout == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CheckoutId = new SelectList(db.Checkouts, "Id", "Total", checkout.Id);
                return View(checkout);
            }

            // POST: ServiceTotals/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "Id,ServiceId,AdditionalFees")] Checkout checkout)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(checkout).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.CheckoutId = new SelectList(db.Checkouts, "Id", "Total", checkout.Id);
                return View(checkout);
            }

            // GET: ServiceTotals/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Checkout checkout = db.Checkouts.Find(id);
                if (checkout == null)
                {
                    return HttpNotFound();
                }
                return View(checkout);
            }

            // POST: ServiceTotals/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Checkout checkout = db.Checkouts.Find(id);
                db.Checkouts.Remove(checkout);
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