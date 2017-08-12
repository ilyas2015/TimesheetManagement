using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class TsDocumentEntity
    {
        public int TsDocumentEntityId { get; set; }
        public string DocumentName { get; set; }
        [Required]
        public string UserId { get; set; }
        public Guid DocGuid { get; set; }
        public string SavedName { get; set; }
        public bool SystemDefault { get; set; }
    }
}
