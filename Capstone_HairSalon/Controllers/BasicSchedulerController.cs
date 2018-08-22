﻿using System;
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


namespace Capstone_HairSalon.Controllers
{
    
    public class BasicSchedulerController : Controller
    {
        private SchedulerContext db = new SchedulerContext();
        public ActionResult Index()
        {


            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Flat;

            scheduler.Config.hour_date = "%H:%i:%s";
            scheduler.Config.readonly_form = false;
            scheduler.Config.first_hour = 8;
            scheduler.Config.last_hour = 20;

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;



            return View(scheduler);
        }


        [Authorize(Roles = "Stylist, Admin")]
        public ContentResult Data()
        {
            var apps = db.Events.ToList();
            return new SchedulerAjaxData(apps);
        }

        [Authorize(Roles = "Stylist, Admin")]
        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
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
