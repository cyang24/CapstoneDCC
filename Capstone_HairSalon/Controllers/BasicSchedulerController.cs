using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone_HairSalon.Models;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using System.Data;
using Microsoft.AspNet.Identity;

namespace Capstone_HairSalon.Controllers
{
    
    public class BasicSchedulerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? id)
        {

            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Flat;

            
            

            scheduler.Config.hour_date = "%H:%i %A";
            scheduler.Config.first_hour = 8;
            scheduler.Config.last_hour = 20;

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;


            return View(scheduler);
        }


        //[Authorize(Roles = "Stylist, Admin")]
        public ContentResult Data(int? id)
        {
            var thisEvent = from s in db.Events
                                  select s;

            var stylists = db.Stylists.ToList();
            string currentUser = User.Identity.GetUserId();
            var stylistInfo = db.Stylists.Where(c => c.UserId == currentUser).FirstOrDefault();
            var myEvent = thisEvent.Where(e => e.Stylist.UserId == stylistInfo.UserId);

            return new SchedulerAjaxData(myEvent.ToList());
        }

        [Authorize(Roles = "Stylist, Admin")]
        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var stylists = db.Stylists.ToList();
            string currentUser = User.Identity.GetUserId();
            var stylistInfo = db.Stylists.Where(c => c.UserId.Equals(currentUser)).FirstOrDefault();

            try
            {
                var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        changedEvent.StylistId = stylistInfo.Id;
                        db.Events.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        db.Entry(changedEvent).State = EntityState.Deleted;
                        break;
                    default:// "update"  
                        db.Entry(changedEvent).State = EntityState.Modified;
                        break;
                }
                db.SaveChanges();
                action.TargetId = changedEvent.Id;
            }
            catch (Exception)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }

        [Authorize(Roles = "Stylist, Admin")]
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
