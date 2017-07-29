using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI.WebControls;
using Data.DataModels;

namespace WebUI.Models
{
    public class TsTemplateViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a template from the list")]
        public int TsWeekTemplateId { get; set; }
        //public string ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
        [Required]
        [Display(Name = "Template Name" )]
        public string TemplateName { get; set; }
        //public DayOfWeek FirstDayOfWeek { get; set; }
        [Required]
        [Display(Name = "Hours per Day")]
        [Range(0, 24d, ErrorMessage = "Please enter number between 0 and 24")]
        public decimal HoursInDay { get; set; }
        [Display(Name="Sunday")]
        public bool FillDay1 { get; set; }
        [Display(Name = "Monday")]
        public bool FillDay2 { get; set; }
        [Display(Name = "Tuesday")]
        public bool FillDay3 { get; set; }
        [Display(Name = "Wednesday")]
        public bool FillDay4 { get; set; }
        [Display(Name = "Thursday")]
        public bool FillDay5 { get; set; }
        [Display(Name = "Friday")]
        public bool FillDay6 { get; set; }
        [Display(Name = "Saturday")]
        public bool FillDay7 { get; set; }
        [Display(Name ="Is Default")]
        public bool IsDefault { get; set; }
    }
}