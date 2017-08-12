using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Data.DataModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebUI.Models;

namespace WebUI.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TemplateController : BaseController
    {
        // GET: Template
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var templates =
                this.db.TsWeekTemplates.OrderBy(i => i.TsWeekTemplateId)
                    .Where(e => e.ApplicationUserId == userId)
                    .Select(e => new TsTemplateViewModel()
                    {
                        TsWeekTemplateId = e.TsWeekTemplateId,
                        //ApplicationUserId = userId,
                        TemplateName = e.TemplateName,
                        HoursInDay = e.HoursInDay,
                        //FirstDayOfWeek = e.FirstDayOfWeek,
                        FillDay1 = e.FillDay1,
                        FillDay2 = e.FillDay2,
                        FillDay3 = e.FillDay3,
                        FillDay4 = e.FillDay4,
                        FillDay5 = e.FillDay5,
                        FillDay6 = e.FillDay6,
                        FillDay7 = e.FillDay6,
                        IsDefault = e.IsDefault
                    }).ToList();
            return View(templates);
        }

        
        public ActionResult Create()
        {
            var template = new TsTemplateViewModel();
            //var list = new List<ListItem>();
            //list.Add(new ListItem { Value = DayOfWeek.Sunday.ToString(), Text = Enum.GetName(typeof(DayOfWeek), DayOfWeek.Sunday)});
            //template.ddlFirstDayOfWeek = list;
            return View(template);
        }

        [ValidateAntiForgeryToken]
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(TsTemplateViewModel template)
        {
            if (!ModelState.IsValid)
            {
                return View(template);
            }
            var userId = User.Identity.GetUserId();
            var dbTemplate = new TsWeekTemplate();
            dbTemplate.ApplicationUserId = userId;
            dbTemplate.TemplateName = template.TemplateName;
            dbTemplate.HoursInDay = template.HoursInDay;
            dbTemplate.FillDay1 = template.FillDay1;
            dbTemplate.FillDay2 = template.FillDay2;
            dbTemplate.FillDay3 = template.FillDay3;
            dbTemplate.FillDay4 = template.FillDay4;
            dbTemplate.FillDay5 = template.FillDay5;
            dbTemplate.FillDay6 = template.FillDay6;
            dbTemplate.FillDay7 = template.FillDay7;
            dbTemplate.IsDefault = template.IsDefault;
            dbTemplate.StartTime = template.StartTime;
            dbTemplate.EndTime = template.EndTime;

            db.TsWeekTemplates.Add(dbTemplate);
            db.SaveChanges();

            // un-checking all other isDefault values
            if (dbTemplate.IsDefault)
            {
                var otherTemplates = db.TsWeekTemplates.Where(e => e.ApplicationUserId == userId && e.IsDefault && e.TsWeekTemplateId != dbTemplate.TsWeekTemplateId).ToList();
                if (otherTemplates.Count > 0)
                {
                    foreach (var item in otherTemplates)
                    {
                        item.IsDefault = false;
                    }
                    db.SaveChanges();
                }
            }
            
            

            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var template = db.TsWeekTemplates.SingleOrDefault(t => t.TsWeekTemplateId == id && t.ApplicationUserId == userId);
            if (template == null) return HttpNotFound();

            var viewTemplate = new TsTemplateViewModel();
            viewTemplate.TemplateName = template.TemplateName;
            viewTemplate.HoursInDay = template.HoursInDay;
            viewTemplate.TsWeekTemplateId = template.TsWeekTemplateId;
            viewTemplate.FillDay1 = template.FillDay1;
            viewTemplate.FillDay2 = template.FillDay2;
            viewTemplate.FillDay3 = template.FillDay3;
            viewTemplate.FillDay4 = template.FillDay4;
            viewTemplate.FillDay5 = template.FillDay5;
            viewTemplate.FillDay6 = template.FillDay6;
            viewTemplate.FillDay7 = template.FillDay7;
            viewTemplate.IsDefault = template.IsDefault;
            viewTemplate.StartTime = template.StartTime;
            viewTemplate.EndTime = template.EndTime;
            return View(viewTemplate);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(TsTemplateViewModel template)
        {
            if (!ModelState.IsValid)
                return View(template);
            var userId = User.Identity.GetUserId();
            var dbTemplate = db.TsWeekTemplates.SingleOrDefault(t => t.TsWeekTemplateId == template.TsWeekTemplateId);
            dbTemplate.TemplateName = template.TemplateName;
            dbTemplate.HoursInDay = template.HoursInDay;
            dbTemplate.FillDay1 = template.FillDay1;
            dbTemplate.FillDay2 = template.FillDay2;
            dbTemplate.FillDay3 = template.FillDay3;
            dbTemplate.FillDay4 = template.FillDay4;
            dbTemplate.FillDay5 = template.FillDay5;
            dbTemplate.FillDay6 = template.FillDay6;
            dbTemplate.FillDay7 = template.FillDay7;
            dbTemplate.ApplicationUserId = userId;
            dbTemplate.IsDefault = template.IsDefault;
            dbTemplate.StartTime = template.StartTime;
            dbTemplate.EndTime = template.EndTime;

            db.SaveChanges();

            // un-checking all other isDefault values
            if (dbTemplate.IsDefault)
            {
                var otherTemplates = db.TsWeekTemplates.Where(e => e.ApplicationUserId == userId && e.IsDefault && e.TsWeekTemplateId != dbTemplate.TsWeekTemplateId).ToList();
                if (otherTemplates.Count > 0)
                {
                    foreach (var item in otherTemplates)
                    {
                        item.IsDefault = false;
                    }
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [System.Web.Mvc.HttpPost]
        public string Delete(int id)
        {
            var dbTemplate = db.TsWeekTemplates.SingleOrDefault(t => t.TsWeekTemplateId == id);
            if (dbTemplate == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            db.TsWeekTemplates.Remove(dbTemplate);
            db.SaveChanges();
            return "success";
        }
    }
}