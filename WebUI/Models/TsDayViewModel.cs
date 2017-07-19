using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Data.DataModels;

namespace WebUI.Models
{
    public class TsDayViewModel
    {
        public int TsDayEntryId { get; set; }
        [Required]
        [Display(Name ="Date")]
        public DateTime EventDate { get; set; }

        public int TsWeekEntryId { get; set; }
        [Required]
        public TsWeekEntry TsWeekEntry { get; set; }
        [Required]
        public TimeSpan Hours { get; set; }
    }
}