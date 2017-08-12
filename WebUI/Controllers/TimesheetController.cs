﻿using System;
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
using System.Data.Entity;
using System.Threading.Tasks;

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
                    TemplateName = e.TemplateName,
                    IsDefault = e.IsDefault
                }).ToList();
            templates.Insert(0, new TsTemplateViewModel { TsWeekTemplateId = 0, TemplateName = "<-- Select Template -->" });
            entry.WeekTemplatesList = templates;
            var defaultTempl = templates.Where(i => i.IsDefault).FirstOrDefault();
            if (defaultTempl != null) entry.TsWeekTemplateId = defaultTempl.TsWeekTemplateId;
           
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
                dbEntry.StartDate = (DateTime)model.StartDate;
                dbEntry.EndDate = (DateTime)model.EndDate;
                dbEntry.TotalHours = model.TotalHours;
                dbEntry.Name = $"{((DateTime)model.StartDate).ToString("yyyy-MM-dd")} - {((DateTime)model.EndDate).ToString("yyyy-MM-dd")}";
                dbEntry.UserId = userID;
                db.TsEntries.Add(dbEntry);
                db.SaveChanges();

            // adding new weeks
            var weeks = GetWeeks((DateTime)model.StartDate, (DateTime)model.EndDate);
            var weekTemplate = db.TsWeekTemplates.Where(T=>T.TsWeekTemplateId == model.TsWeekTemplateId).FirstOrDefault();

            decimal totalHours = 0;
            foreach(var item in weeks)
            {
                item.TsEntryId = dbEntry.TsEntryId;
                item.TsEntry = dbEntry;
                item.UserId = userID;
                UpdateHours(item, weekTemplate);
                db.TsWeekEntriesEntries.Add(item);
                db.SaveChanges();
                totalHours += item.TotalHours;
            }
            dbEntry.TotalHours = totalHours;
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
            var dbEntry = db.TsEntries.Include(c => c.Weeks).SingleOrDefault(t => t.TsEntryId == id && t.UserId == userID);
            
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

            entry.Weeks = new List<TsWeekViewModel>();
            foreach(var item in dbEntry.Weeks)
            {
                var newWeek = new TsWeekViewModel();
                newWeek.TsWeekEntryId = item.TsWeekEntryId;
                newWeek.TsEntryId = item.TsEntryId;
                newWeek.StartDate = item.StartDate;
                newWeek.EndDate = item.EndDate;
                newWeek.TotalHours = item.TotalHours;
                newWeek.UserId = item.UserId;

                newWeek.Day1 = item.Day1;
                newWeek.Day1Hours = item.Day1Hours;
                newWeek.Day2 = item.Day2;
                newWeek.Day2Hours = item.Day2Hours;
                newWeek.Day3 = item.Day3;
                newWeek.Day3Hours = item.Day3Hours;
                newWeek.Day4 = item.Day4;
                newWeek.Day4Hours = item.Day4Hours;
                newWeek.Day5 = item.Day5;
                newWeek.Day5Hours = item.Day5Hours;
                newWeek.Day6 = item.Day6;
                newWeek.Day6Hours = item.Day6Hours;
                newWeek.Day7 = item.Day7;
                newWeek.Day7Hours = item.Day7Hours;

                newWeek.Day1StartTime = item.Day1StartTime;
                newWeek.Day1EndTime = item.Day1EndTime;

                newWeek.Day2StartTime = item.Day2StartTime;
                newWeek.Day2EndTime = item.Day2EndTime;

                newWeek.Day3StartTime = item.Day3StartTime;
                newWeek.Day3EndTime = item.Day3EndTime;

                newWeek.Day4StartTime = item.Day4StartTime;
                newWeek.Day4EndTime = item.Day4EndTime;

                newWeek.Day5StartTime = item.Day5StartTime;
                newWeek.Day5EndTime = item.Day5EndTime;

                newWeek.Day6StartTime = item.Day6StartTime;
                newWeek.Day6EndTime = item.Day6EndTime;

                newWeek.Day7StartTime = item.Day7StartTime;
                newWeek.Day7EndTime = item.Day7EndTime;

                entry.Weeks.Add(newWeek);
            }
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
            dbEntry.StartDate = (DateTime)model.StartDate;
            dbEntry.EndDate = (DateTime)model.EndDate;
            dbEntry.TotalHours = model.TotalHours;
            dbEntry.Name = $"{((DateTime)model.StartDate).ToString("yyyy-MM-dd")} - {((DateTime)model.EndDate).ToString("yyyy-MM-dd")}";
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult EditHours(int weekId)
        {
            ViewBag.WeekId = weekId;
            var userId = User.Identity.GetUserId();
            var weekDb = db.TsWeekEntriesEntries.SingleOrDefault(t => t.TsWeekEntryId == weekId && t.UserId == userId);
            if (weekDb == null) return HttpNotFound();
            var viewWeek = new TsWeekViewModel();
            viewWeek.TsWeekEntryId = weekId;
            viewWeek.UserId = weekDb.UserId;
            viewWeek.TsEntryId = weekDb.TsEntryId;
            viewWeek.TotalHours = weekDb.TotalHours;

            viewWeek.Day1 = weekDb.Day1;
            viewWeek.Day1Hours = weekDb.Day1Hours;
            viewWeek.Day2 = weekDb.Day2;
            viewWeek.Day2Hours = weekDb.Day2Hours;
            viewWeek.Day3 = weekDb.Day3;
            viewWeek.Day3Hours = weekDb.Day3Hours;
            viewWeek.Day4 = weekDb.Day4;
            viewWeek.Day4Hours = weekDb.Day4Hours;
            viewWeek.Day5 = weekDb.Day5;
            viewWeek.Day5Hours = weekDb.Day5Hours;
            viewWeek.Day6 = weekDb.Day6;
            viewWeek.Day6Hours = weekDb.Day6Hours;
            viewWeek.Day7 = weekDb.Day7;
            viewWeek.Day7Hours = weekDb.Day7Hours;

            viewWeek.Day1StartTime = weekDb.Day1StartTime;
            viewWeek.Day1EndTime = weekDb.Day1EndTime;

            viewWeek.Day2StartTime = weekDb.Day2StartTime;
            viewWeek.Day2EndTime = weekDb.Day2EndTime;

            viewWeek.Day3StartTime = weekDb.Day3StartTime;
            viewWeek.Day3EndTime = weekDb.Day3EndTime;

            viewWeek.Day4StartTime = weekDb.Day4StartTime;
            viewWeek.Day4EndTime = weekDb.Day4EndTime;

            viewWeek.Day5StartTime = weekDb.Day5StartTime;
            viewWeek.Day5EndTime = weekDb.Day5EndTime;

            viewWeek.Day6StartTime = weekDb.Day6StartTime;
            viewWeek.Day6EndTime = weekDb.Day6EndTime;

            viewWeek.Day7StartTime = weekDb.Day7StartTime;
            viewWeek.Day7EndTime = weekDb.Day7EndTime;

            viewWeek.StartDate = weekDb.StartDate;
            viewWeek.EndDate = weekDb.EndDate;
            return PartialView("_EditHours", viewWeek);
        }

        [System.Web.Mvc.HttpPost]
        public string EditHours(TsWeekViewModel model)
        {
            string userID = User.Identity.GetUserId();
            //if (!ModelState.IsValid)
            //{
            //    return View(weekModel);
            //}
            var dbWeekEntry = db.TsWeekEntriesEntries.SingleOrDefault(t => t.TsWeekEntryId == model.TsWeekEntryId && t.UserId == userID);
            if (dbWeekEntry == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            //dbWeekEntry.StartDate = model.StartDate;
            //dbWeekEntry.EndDate = model.EndDate;
            dbWeekEntry.Day1Hours = model.Day1Hours;
            dbWeekEntry.Day2Hours = model.Day2Hours;
            dbWeekEntry.Day3Hours = model.Day3Hours;
            dbWeekEntry.Day4Hours = model.Day4Hours;
            dbWeekEntry.Day5Hours = model.Day5Hours;
            dbWeekEntry.Day6Hours = model.Day6Hours;
            dbWeekEntry.Day7Hours = model.Day7Hours;
            dbWeekEntry.Day1StartTime = model.Day1StartTime;
            dbWeekEntry.Day1EndTime = model.Day1EndTime;
            dbWeekEntry.Day2StartTime = model.Day2StartTime;
            dbWeekEntry.Day2EndTime = model.Day2EndTime;
            dbWeekEntry.Day3StartTime = model.Day3StartTime;
            dbWeekEntry.Day3EndTime = model.Day3EndTime;
            dbWeekEntry.Day4StartTime = model.Day4StartTime;
            dbWeekEntry.Day4EndTime = model.Day4EndTime;
            dbWeekEntry.Day5StartTime = model.Day5StartTime;
            dbWeekEntry.Day5EndTime = model.Day5EndTime;
            dbWeekEntry.Day6StartTime = model.Day6StartTime;
            dbWeekEntry.Day6EndTime = model.Day6EndTime;
            dbWeekEntry.Day7StartTime = model.Day7StartTime;
            dbWeekEntry.Day7EndTime = model.Day7EndTime;

            TimeSpan totalTime = model.Day1Hours + model.Day2Hours + model.Day3Hours + model.Day4Hours + model.Day5Hours + model.Day6Hours + model.Day7Hours;
            dbWeekEntry.TotalHours = (decimal)totalTime.TotalHours;
            db.SaveChanges();
            UpdateTimesheetTotalHours(dbWeekEntry.TsEntryId);
            return "success";
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

        [System.Web.Mvc.HttpGet]
        public ActionResult GetWeekDays(int weekId)
        {
            var model = this.GetPartialWeekViewModel(weekId);
            return PartialView("_Days", model);
        }

        private TsWeekViewModel GetPartialWeekViewModel(int weekId)
        {
            var userId = User.Identity.GetUserId();
            var weekDb = db.TsWeekEntriesEntries.SingleOrDefault(t => t.TsWeekEntryId == weekId && t.UserId == userId);
            var viewWeek = new TsWeekViewModel();
            if (weekDb == null) return null;

            viewWeek.TsWeekEntryId = weekId;
            viewWeek.UserId = weekDb.UserId;
            viewWeek.TsEntryId = weekDb.TsEntryId;
            viewWeek.TotalHours = weekDb.TotalHours;

            viewWeek.Day1 = weekDb.Day1;
            viewWeek.Day1Hours = weekDb.Day1Hours;
            viewWeek.Day2 = weekDb.Day2;
            viewWeek.Day2Hours = weekDb.Day2Hours;
            viewWeek.Day3 = weekDb.Day3;
            viewWeek.Day3Hours = weekDb.Day3Hours;
            viewWeek.Day4 = weekDb.Day4;
            viewWeek.Day4Hours = weekDb.Day4Hours;
            viewWeek.Day5 = weekDb.Day5;
            viewWeek.Day5Hours = weekDb.Day5Hours;
            viewWeek.Day6 = weekDb.Day6;
            viewWeek.Day6Hours = weekDb.Day6Hours;
            viewWeek.Day7 = weekDb.Day7;
            viewWeek.Day7Hours = weekDb.Day7Hours;
            viewWeek.Day1StartTime = weekDb.Day1StartTime;
            viewWeek.Day1EndTime = weekDb.Day1EndTime;
            viewWeek.Day2StartTime = weekDb.Day2StartTime;
            viewWeek.Day2EndTime = weekDb.Day2EndTime;
            viewWeek.Day3StartTime = weekDb.Day3StartTime;
            viewWeek.Day3EndTime = weekDb.Day3EndTime;
            viewWeek.Day4StartTime = weekDb.Day4StartTime;
            viewWeek.Day4EndTime = weekDb.Day4EndTime;
            viewWeek.Day5StartTime = weekDb.Day5StartTime;
            viewWeek.Day5EndTime = weekDb.Day5EndTime;
            viewWeek.Day6StartTime = weekDb.Day6StartTime;
            viewWeek.Day6EndTime = weekDb.Day6EndTime;
            viewWeek.Day7StartTime = weekDb.Day7StartTime;
            viewWeek.Day7EndTime = weekDb.Day7EndTime;

            viewWeek.StartDate = weekDb.StartDate;
            viewWeek.EndDate = weekDb.EndDate;
            return viewWeek;
        }

        protected List<TsWeekEntry>GetWeeks(DateTime StartDate, DateTime EndDate)
        {
            List<TsWeekEntry> weeks = new List<TsWeekEntry>();

            TsWeekEntry week = new TsWeekEntry();
            week.StartDate = StartDate;
            int dayNum = 1;

            for (var dt = StartDate; dt <= EndDate; dt = dt.AddDays(1))
            {
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    week = new TsWeekEntry();
                    week.StartDate = dt;
                    dayNum = 1;
                }
                week.EndDate = dt;
                switch (dayNum)
                {
                    case 1:
                        week.Day1 = dt;
                        break;
                    case 2:
                        week.Day2 = dt;
                        break;
                    case 3:
                        week.Day3 = dt;
                        break;
                    case 4:
                        week.Day4 = dt;
                        break;
                    case 5:
                        week.Day5 = dt;
                        break;
                    case 6:
                        week.Day6 = dt;
                        break;
                    case 7:
                        week.Day7 = dt;
                        break;
                }
                dayNum++;

                if (dt.DayOfWeek == DayOfWeek.Saturday || dt == EndDate)
                {
                    weeks.Add(week);
                }
            }
            return weeks;
        }

        protected void UpdateHours(TsWeekEntry week, TsWeekTemplate template)
        {
            double totalHours = 0;
            if (template != null)
            {
                if (week.Day1 != null)
                {
                    switch (((DateTime)week.Day1).DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            week.Day1Hours = TimeSpan.FromHours(template.FillDay1 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay1 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Monday:
                            week.Day1Hours = TimeSpan.FromHours(template.FillDay2 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay2 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Tuesday:
                            week.Day1Hours = TimeSpan.FromHours(template.FillDay3 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay3 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Wednesday:
                            week.Day1Hours = TimeSpan.FromHours(template.FillDay4 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay4 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Thursday:
                            week.Day1Hours = TimeSpan.FromHours(template.FillDay5 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay5 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Friday:
                            week.Day1Hours = TimeSpan.FromHours(template.FillDay6 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay6 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Saturday:
                            week.Day1Hours = TimeSpan.FromHours(template.FillDay7 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay7 ? (double)template.HoursInDay : 0;
                            break;
                    }
                    if (week.Day1Hours.TotalMinutes > 0)
                    {
                        week.Day1StartTime = template.StartTime;
                        week.Day1EndTime = template.EndTime;
                    }
                }
                if (week.Day2 != null)
                {
                    switch (((DateTime)week.Day2).DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            week.Day2Hours = TimeSpan.FromHours(template.FillDay1 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay1 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Monday:
                            week.Day2Hours = TimeSpan.FromHours(template.FillDay2 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay2 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Tuesday:
                            week.Day2Hours = TimeSpan.FromHours(template.FillDay3 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay3 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Wednesday:
                            week.Day2Hours = TimeSpan.FromHours(template.FillDay4 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay4 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Thursday:
                            week.Day2Hours = TimeSpan.FromHours(template.FillDay5 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay5 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Friday:
                            week.Day2Hours = TimeSpan.FromHours(template.FillDay6 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay6 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Saturday:
                            week.Day2Hours = TimeSpan.FromHours(template.FillDay7 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay7 ? (double)template.HoursInDay : 0;
                            break;
                    }
                    if (week.Day2Hours.TotalMinutes > 0)
                    {
                        week.Day2StartTime = template.StartTime;
                        week.Day2EndTime = template.EndTime;
                    }
                }
                if (week.Day3 != null)
                {
                    switch (((DateTime)week.Day3).DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            week.Day3Hours = TimeSpan.FromHours(template.FillDay1 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay1 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Monday:
                            week.Day3Hours = TimeSpan.FromHours(template.FillDay2 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay2 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Tuesday:
                            week.Day3Hours = TimeSpan.FromHours(template.FillDay3 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay3 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Wednesday:
                            week.Day3Hours = TimeSpan.FromHours(template.FillDay4 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay4 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Thursday:
                            week.Day3Hours = TimeSpan.FromHours(template.FillDay5 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay5 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Friday:
                            week.Day3Hours = TimeSpan.FromHours(template.FillDay6 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay6 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Saturday:
                            week.Day3Hours = TimeSpan.FromHours(template.FillDay7 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay7 ? (double)template.HoursInDay : 0;
                            break;
                    }
                    if (week.Day3Hours.TotalMinutes > 0)
                    {
                        week.Day3StartTime = template.StartTime;
                        week.Day3EndTime = template.EndTime;
                    }
                }
                if (week.Day4 != null)
                {
                    switch (((DateTime)week.Day4).DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            week.Day4Hours = TimeSpan.FromHours(template.FillDay1 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay1 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Monday:
                            week.Day4Hours = TimeSpan.FromHours(template.FillDay2 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay2 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Tuesday:
                            week.Day4Hours = TimeSpan.FromHours(template.FillDay3 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay3 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Wednesday:
                            week.Day4Hours = TimeSpan.FromHours(template.FillDay4 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay4 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Thursday:
                            week.Day4Hours = TimeSpan.FromHours(template.FillDay5 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay5 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Friday:
                            week.Day4Hours = TimeSpan.FromHours(template.FillDay6 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay6 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Saturday:
                            week.Day4Hours = TimeSpan.FromHours(template.FillDay7 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay7 ? (double)template.HoursInDay : 0;
                            break;
                    }
                    if (week.Day4Hours.TotalMinutes > 0)
                    {
                        week.Day4StartTime = template.StartTime;
                        week.Day4EndTime = template.EndTime;
                    }
                }
                if (week.Day5 != null)
                {
                    switch (((DateTime)week.Day5).DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            week.Day5Hours = TimeSpan.FromHours(template.FillDay1 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay1 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Monday:
                            week.Day5Hours = TimeSpan.FromHours(template.FillDay2 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay2 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Tuesday:
                            week.Day5Hours = TimeSpan.FromHours(template.FillDay3 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay3 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Wednesday:
                            week.Day5Hours = TimeSpan.FromHours(template.FillDay4 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay4 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Thursday:
                            week.Day5Hours = TimeSpan.FromHours(template.FillDay5 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay5 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Friday:
                            week.Day5Hours = TimeSpan.FromHours(template.FillDay6 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay6 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Saturday:
                            week.Day5Hours = TimeSpan.FromHours(template.FillDay7 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay7 ? (double)template.HoursInDay : 0;
                            break;
                    }
                    if (week.Day5Hours.TotalMinutes > 0)
                    {
                        week.Day5StartTime = template.StartTime;
                        week.Day5EndTime = template.EndTime;
                    }
                }
                if (week.Day6 != null)
                {
                    switch (((DateTime)week.Day6).DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            week.Day6Hours = TimeSpan.FromHours(template.FillDay1 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay1 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Monday:
                            week.Day6Hours = TimeSpan.FromHours(template.FillDay2 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay2 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Tuesday:
                            week.Day6Hours = TimeSpan.FromHours(template.FillDay3 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay3 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Wednesday:
                            week.Day6Hours = TimeSpan.FromHours(template.FillDay4 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay4 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Thursday:
                            week.Day6Hours = TimeSpan.FromHours(template.FillDay5 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay5 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Friday:
                            week.Day6Hours = TimeSpan.FromHours(template.FillDay6 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay6 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Saturday:
                            week.Day6Hours = TimeSpan.FromHours(template.FillDay7 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay7 ? (double)template.HoursInDay : 0;
                            break;
                    }
                    if (week.Day6Hours.TotalMinutes > 0)
                    {
                        week.Day6StartTime = template.StartTime;
                        week.Day6EndTime = template.EndTime;
                    }
                }
                if (week.Day7 != null)
                {
                    switch (((DateTime)week.Day7).DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            week.Day7Hours = TimeSpan.FromHours(template.FillDay1 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay1 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Monday:
                            week.Day7Hours = TimeSpan.FromHours(template.FillDay2 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay2 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Tuesday:
                            week.Day7Hours = TimeSpan.FromHours(template.FillDay3 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay3 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Wednesday:
                            week.Day7Hours = TimeSpan.FromHours(template.FillDay4 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay4 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Thursday:
                            week.Day7Hours = TimeSpan.FromHours(template.FillDay5 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay5 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Friday:
                            week.Day7Hours = TimeSpan.FromHours(template.FillDay6 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay6 ? (double)template.HoursInDay : 0;
                            break;
                        case DayOfWeek.Saturday:
                            week.Day7Hours = TimeSpan.FromHours(template.FillDay7 ? (double)template.HoursInDay : 0);
                            totalHours += template.FillDay1 ? (double)template.HoursInDay : 0;
                            break;
                    }
                    if (week.Day7Hours.TotalMinutes > 0)
                    {
                        week.Day7StartTime = template.StartTime;
                        week.Day7EndTime = template.EndTime;
                    }
                }
                week.TotalHours = (decimal)totalHours;
            }
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult GetTotalHours(int entryId)
        {
            string userID = User.Identity.GetUserId();
            var dbEntry = db.TsEntries.Include(c => c.Weeks).SingleOrDefault(t => t.TsEntryId == entryId && t.UserId == userID);

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

            var finalvalue = new { success = true, totalHours = entry.TotalHours.ToString() };
            return Json(finalvalue, JsonRequestBehavior.AllowGet);
        }

        protected void UpdateTimesheetTotalHours(int id)
        {
            string userID = User.Identity.GetUserId();
            var dbEntry = db.TsEntries.Include(c => c.Weeks).SingleOrDefault(t => t.TsEntryId == id && t.UserId == userID);

            if (dbEntry == null)
            {
                return;
            }

            decimal totalHours = 0;

            foreach (var item in dbEntry.Weeks)
            {
                totalHours += item.TotalHours;
            }
            dbEntry.TotalHours = totalHours;
            db.SaveChanges();
        }
    }
}
