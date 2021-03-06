﻿using System;
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
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index(int? id)
        {
            var stylists = db.Stylists.ToList();
            var services = db.Services.ToList();
            Appointment appointment = new Appointment()
            {
                Stylists = stylists,
                Services = services
            };
            return View(db.Appointments.ToList());
        }

        public ActionResult SingleIndex(int? id)
        {
            var stylists = db.Stylists.ToList();
            var services = db.Services.ToList();
            Appointment appointment = new Appointment()
            {
                Stylists = stylists,
                Services = services
            };

            var appointmentList = from s in db.Appointments
                               select s;

            string currentUser = User.Identity.GetUserId();
            var stylistInfo = db.Stylists.Where(c => c.UserId.Equals(currentUser)).FirstOrDefault();
            string stylistsName = stylistInfo.FirstName;
            appointmentList = appointmentList.Where(s => s.Stylist.FirstName == stylistsName);

            return View(appointmentList.ToList());
        }

        public ActionResult SingleIndexCustomer(int? id)
        {
            var stylists = db.Stylists.ToList();
            var services = db.Services.ToList();
            Appointment appointment = new Appointment()
            {
                Stylists = stylists,
                Services = services
            };

            var appointmentList = from s in db.Appointments
                                  select s;

            string currentUser = User.Identity.GetUserId();
            var customerInfo = db.Customers.Where(c => c.UserId.Equals(currentUser)).FirstOrDefault();
            string customerPhone = customerInfo.Phone;
            appointmentList = appointmentList.Where(s => s.Phone == customerPhone);

            return View(appointmentList.ToList());
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Include(p => p.Stylist).Include(s => s.Service).Where(m => m.Id == id).FirstOrDefault();
            if (appointment == null)
            {
                return HttpNotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            var stylists = db.Stylists.ToList();
            var services = db.Services.ToList();
            Appointment appointment = new Appointment()
            {
                Stylists = stylists,
                Services = services
            };
            return View(appointment);

        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Phone,Date,StylistId,ServiceId,TimeRequest,Accept_Terms")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var stylists = db.Stylists.ToList();
                var services = db.Services.ToList();
                db.Appointments.Add(appointment);
                db.SaveChanges();
                TextAPIsController textAPIsController = new TextAPIsController();
                textAPIsController.SendText(appointment);
                return RedirectToAction("ThankYou");
            }

            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            appointment.Stylists = db.Stylists.ToList();
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,PhoneNumber,Date,StylistId,TimeRequest,ConfirmAppointment,DenyAppointment,ReminderText")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;

                db.SaveChanges();
                TextAPIsController textAPIsController = new TextAPIsController();
                textAPIsController.SendText(appointment);
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
