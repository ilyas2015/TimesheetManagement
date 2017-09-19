using Data.DataModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class DocumentController : BaseController
    {
        // GET: Document
        public ActionResult Index()
        {
            List<TsDocumentEntity> docsForDownload = new List<TsDocumentEntity>();
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/SavedDocBox"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");

            string userId = User.Identity.GetUserId();
            var dbUserDocs = this.db.TsDocumentEntries.Where(d => d.UserId == userId);

            var admin = db.Users.Where(u => u.Email == "admin@admin.com").FirstOrDefault();
            if (admin != null)
            {
                var defDoc = this.db.TsDocumentEntries.Where(d => d.UserId == admin.Id).FirstOrDefault();
                if (defDoc != null)
                {
                    docsForDownload.Add(defDoc);
                }
            }
            
            if (dbUserDocs != null)
            {
                foreach(var userDoc in dbUserDocs)
                {
                    if (fileNames.Where(i => i.FullName.Contains(userDoc.SavedName)).FirstOrDefault() != null)
                    {
                        docsForDownload.Add(userDoc);
                    }
                }
            }

            var newModel = new TsDocumentViewerModel();

            var docViews = docsForDownload.Select(e=> new TsDocumentListModel()
            {
                TsDocumentEntityId  = e.TsDocumentEntityId,
                DocumentName  = e.DocumentName,
                UserId = e.UserId,
                DocGuid = e.DocGuid,
                SavedName = e.SavedName,
                SystemDefault = e.SystemDefault
            }).ToList();

            newModel.UploadedDocuments = docViews;
            return View(newModel);
            
        }

        public FileResult Download(string SavedName)
        {
            return File("~/SavedDocBox/" + SavedName, System.Net.Mime.MediaTypeNames.Application.Octet);
        }

        [HttpPost]
        public ActionResult Upload(TsDocumentViewerModel model)
        {

            HttpPostedFileBase file = model.DocumentUpload;

            Guid newDocGuid = Guid.NewGuid();

            var userDoc = model.UploadedDocuments.Where(d => !d.SystemDefault).FirstOrDefault();
            if (userDoc!=null)
            {
                newDocGuid = userDoc.DocGuid;
            }

            try
            {
                if (file.ContentLength > 0)
                {

                    string userId = User.Identity.GetUserId();
                    if (!string.IsNullOrEmpty(userId))
                    {
                        TsDocumentEntity newDoc = new TsDocumentEntity();
                        newDoc.UserId = userId;
                        var fileName = Path.GetFileName(file.FileName);
                        newDoc.DocumentName = fileName;
                        //Guid newDocGuid = Guid.NewGuid();
                        newDoc.DocGuid = newDocGuid;
                        newDoc.SavedName = string.Format("TS_{0}.docx", newDocGuid.ToString());
                        var path = Path.Combine(Server.MapPath("~/SavedDocBox"), newDoc.SavedName);
                        file.SaveAs(path);

                        var dbDoc = db.TsDocumentEntries.SingleOrDefault(d => d.UserId == userId);
                        if (dbDoc==null)
                        {
                            dbDoc = new TsDocumentEntity();
                            dbDoc.UserId = userId;
                            dbDoc.SystemDefault = false;
                            dbDoc.DocumentName = newDoc.DocumentName;
                            dbDoc.DocGuid = newDoc.DocGuid;
                            dbDoc.SavedName = newDoc.SavedName;
                            db.TsDocumentEntries.Add(dbDoc);
                        }
                        else
                        {
                            dbDoc.DocumentName = newDoc.DocumentName;
                            dbDoc.DocGuid = newDoc.DocGuid;
                            dbDoc.SavedName = newDoc.SavedName;
                        }
                        db.SaveChanges();
                    }
                }
                ViewBag.Message = "Upload successful";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Upload failed";
                return RedirectToAction("Uploads");
            }
        }


    }
}