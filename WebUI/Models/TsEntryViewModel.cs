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
        [Display(Name = "Start Date")]
        
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        
        public DateTime EndDate { get; set; }
        [MaxLength(120)]
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Display(Name = "Total Hours")]
        [ReadOnly(true)]
        public decimal TotalHours { get; set; }
        public List<TsWeekEntry> Weeks { get; set; }
        
        public string UserId { get; set; }

        public List<TsTemplateViewModel> WeekTemplatesList { get; set; }
    }
}