using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class WeekController : Controller
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
        [HttpPost]
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
            return View();
        }

        // POST: Week/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Week/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Week/Delete/5
        [HttpPost]
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
