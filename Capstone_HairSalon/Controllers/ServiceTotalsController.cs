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

namespace Capstone_HairSalon.Controllers
{
    public class ServiceTotalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiceTotals
        public ActionResult Index()
        {
            var serviceTotals = db.ServiceTotals.Include(s => s.Service);
            return View(serviceTotals.ToList());
        }

        // GET: ServiceTotals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTotal serviceTotal = db.ServiceTotals.Find(id);
            if (serviceTotal == null)
            {
                return HttpNotFound();
            }
            return View(serviceTotal);
        }

        // GET: ServiceTotals/Create
        public ActionResult Create()
        {
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name");
            return View();
        }

        // POST: ServiceTotals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,AdditionalFees")] ServiceTotal serviceTotal)
        {
            var checkoutId = db.Checkouts.OrderByDescending(c => c.Id).FirstOrDefault();
            checkoutId.UserId = User.Identity.GetUserId();
            var result = db.Checkouts.OrderByDescending(c => c.Id).Select(c => c.Id).First();
            serviceTotal.CheckoutId = result;


            if (ModelState.IsValid)
            {
                db.ServiceTotals.Add(serviceTotal);
                db.SaveChanges();
                return RedirectToAction("SingleIndex");
            }

            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", serviceTotal.ServiceId);
            return View(serviceTotal);
        }

        public ActionResult MakeNewCharge()
        {
            Checkout checkout = new Checkout();
            db.Checkouts.Add(checkout);
            db.SaveChanges();
            return RedirectToAction("Create");
        }
        public ActionResult SingleIndex(string searchString)
        {
            var latestPayment = db.Checkouts.OrderByDescending(c => c.Id).FirstOrDefault();
            var userId = User.Identity.GetUserId();
            var checkout = db.ServiceTotals.Include(o => o.Service).Include(o => o.Checkout).Where(o => o.Checkout.UserId == userId && o.CheckoutId == latestPayment.Id).ToList();
            var currentServicefees = db.ServiceTotals.OrderByDescending(c => c.Id).FirstOrDefault();

            var total = 0;
            foreach (var item in checkout)
            {
                total += item.Service.Price;
            }
            checkout.First().Checkout.Total = total;

            ViewBag.TotalAmount = total + currentServicefees.AdditionalFees;

            return View(checkout);
        }

        // GET: ServiceTotals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTotal serviceTotal = db.ServiceTotals.Find(id);
            if (serviceTotal == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", serviceTotal.ServiceId);
            return View(serviceTotal);
        }

        // POST: ServiceTotals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,AdditionalFees")] ServiceTotal serviceTotal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceTotal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", serviceTotal.ServiceId);
            return View(serviceTotal);
        }

        // GET: ServiceTotals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTotal serviceTotal = db.ServiceTotals.Find(id);
            if (serviceTotal == null)
            {
                return HttpNotFound();
            }
            return View(serviceTotal);
        }

        // POST: ServiceTotals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceTotal serviceTotal = db.ServiceTotals.Find(id);
            db.ServiceTotals.Remove(serviceTotal);
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
