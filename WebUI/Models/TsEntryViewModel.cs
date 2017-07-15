using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Data.DataModels;

namespace WebUI.Models
{
    public class TsEntryViewModel
    {
        public int TsEntryId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [MaxLength(120)]
        [Display(Name = "Time Period")]
        public string Name { get; set; }

        [Display(Name = "Total Hours")]
        public decimal TotalHours { get; set; }

        public List<TsWeekViewModel> Weeks { get; set; }
        
        public string UserId { get; set; }

        public List<TsTemplateViewModel> WeekTemplatesList { get; set; }
    }
}