using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Data.DataModels;

namespace WebUI.Models
{
    public class TsWeekViewModel
    {
        public int TsWeekEntryId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        //public int WeekTemplateId { get; set; }
        //[Required]
        //public TsWeekTemplate WeekTemplate  { get; set; }
        public decimal TotalHours { get; set; }

        [Required]
        public string UserId { get; set; }

        public int TsEntryId { get; set; }
        public TsEntry TsEntry { get; set; }

        public List<TsDayEntry> Days { get; set; }
    }
}