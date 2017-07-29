using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class WeekController : BaseController
    {
        // GET: Week
        public ActionResult Index()
        {
            return View();
        }

        // GET: Week/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Week/Create
        public ActionResult Create(int Id)
        {
            var model = new TsWeekViewModel();
            model.TsEntryId = Id;
            return View(model);
        }

        // POST: Week/Create
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Week/Edit/5
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var weekDb = db.TsWeekEntriesEntries.SingleOrDefault(t => t.TsWeekEntryId == id && t.UserId == userId);
            if (weekDb == null) return HttpNotFound();
            var viewWeek = new TsWeekViewModel();
            viewWeek.TsWeekEntryId = id;
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
           
            viewWeek.StartDate = weekDb.StartDate;
            viewWeek.EndDate = weekDb.EndDate;

            return View(viewWeek);
        }

        // POST: Week/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(TsWeekViewModel weekModel)
        {
            string userID = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View(weekModel);
            }
            var dbWeekEntry = db.TsWeekEntriesEntries.SingleOrDefault(t => t.TsWeekEntryId == weekModel.TsWeekEntryId && t.UserId == userID);
            if (dbWeekEntry == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            dbWeekEntry.StartDate = weekModel.StartDate;
            dbWeekEntry.EndDate = weekModel.EndDate;
            dbWeekEntry.TotalHours = weekModel.TotalHours;
            db.SaveChanges();
            return RedirectToAction("Edit", "Timesheet", new { @id = dbWeekEntry.TsEntryId});
        }

        // GET: Week/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Week/Delete/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
