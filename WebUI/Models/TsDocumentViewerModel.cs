using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Models.Validations;

namespace WebUI.Models
{
    public class TsDocumentViewerModel
    {
        public List<TsDocumentListModel> UploadedDocuments { get; set; }
        [AttachmentAttribute]
        public HttpPostedFileBase DocumentUpload { get; set; }
    }

    public class TsDocumentListModel
    {
        public int TsDocumentEntityId { get; set; }
        public string DocumentName { get; set; }
        public string UserId { get; set; }
        public Guid DocGuid { get; set; }
        public string SavedName { get; set; }
        public bool SystemDefault { get; set; }
    }
}