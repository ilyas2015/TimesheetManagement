using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DataModels;

namespace WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Base
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}