using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Data.DataModels;
using Microsoft.AspNet.Identity;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TimesheetController : BaseController
    {
        // GET: Timesheet
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var TsEntries =
                this.db.TsEntries.OrderByDescending(i => i.StartDate)
                    .Where(e => e.UserId == userId)
                    .Select(e => new TsEntryViewModel()
                    {
                        TsEntryId = e.TsEntryId,
                        //ApplicationUserId = userId,
                        Name = e.Name,
                        StartDate = e.StartDate,
                        //FirstDayOfWeek = e.FirstDayOfWeek,
                        EndDate = e.EndDate,
                        TotalHours = e.TotalHours
                    }).ToList();
            return View(TsEntries);
        }

        // GET: Timesheet/Create
        public ActionResult Create()
        {
            var entry = new TsEntryViewModel();
            string userId = User.Identity.GetUserId();
            var templates = db.TsWeekTemplates.Where(t => t.ApplicationUserId == userId).
                Select(e => new TsTemplateViewModel()
                {
                    TsWeekTemplateId = e.TsWeekTemplateId,
                    TemplateName = e.TemplateName
                }).ToList();
            entry.WeekTemplatesList = templates;
            return View(entry);
        }

        // POST: Timesheet/Create
        [ValidateAntiForgeryToken]
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(TsEntryViewModel model)
        {
            //try
            //{
                string userID = User.Identity.GetUserId();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                TsEntry dbEntry = new TsEntry();
                dbEntry.StartDate = model.StartDate;
                dbEntry.EndDate = model.EndDate;
                dbEntry.Name = $"{model.StartDate.ToString("yyyy-MM-dd")} - {model.EndDate.ToString("yyyy-MM-dd")}";
                dbEntry.UserId = userID;
                db.TsEntries.Add(dbEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            //}
            //catch(Exception ex)
            //{
            //    this.AddNotification("Error: " + ex.Message, NotificationType.ERROR);
            //    return View(model);
            //}
        }

        // GET: Timesheet/Edit/5
        public ActionResult Edit(int id)
        {
            string userID = User.Identity.GetUserId();
            var dbEntry = db.TsEntries.SingleOrDefault(t => t.TsEntryId == id && t.UserId == userID);
            if (dbEntry == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var entry = new TsEntryViewModel();
            entry.TsEntryId = dbEntry.TsEntryId;
            entry.UserId = userID;
            entry.StartDate = dbEntry.StartDate;
            entry.EndDate = dbEntry.EndDate;
            entry.TotalHours = dbEntry.TotalHours;
            entry.Name = dbEntry.Name;

            //var templates = db.TsWeekTemplates.Where(t => t.ApplicationUserId == userID).
            //    Select(e => new TsTemplateViewModel()
            //    {
            //        TsWeekTemplateId = e.TsWeekTemplateId,
            //        TemplateName = e.TemplateName
            //    }).ToList();
            //entry.WeekTemplatesList = templates;
            return View(entry);
        }

        // POST: Timesheet/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(TsEntryViewModel model)
        {
            string userID = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var dbEntry = db.TsEntries.SingleOrDefault(t => t.TsEntryId == model.TsEntryId && t.UserId == userID);
            if (dbEntry == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            dbEntry.StartDate = model.StartDate;
            dbEntry.EndDate = model.EndDate;
            dbEntry.TotalHours = model.TotalHours;
            db.SaveChanges();
            return RedirectToAction("Index");

        }


        // POST: Timesheet/Delete/5
        [System.Web.Mvc.HttpPost]
        public string Delete(int id)
        {
            string userID = User.Identity.GetUserId();
            var dbEntry = db.TsEntries.SingleOrDefault(t => t.TsEntryId == id && t.UserId == userID);
            if (dbEntry == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            db.TsEntries.Remove(dbEntry);
            db.SaveChanges();
            return "success";
        }
    }
}
