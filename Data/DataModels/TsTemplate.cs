using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class TsWeekTemplate
    {
        public int TsWeekTemplateId { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string TemplateName { get; set; }
        //public DayOfWeek FirstDayOfWeek { get; set; }
        public decimal HoursInDay { get; set; }
        public bool FillDay1 { get; set; }
        public bool FillDay2 { get; set; }
        public bool FillDay3 { get; set; }
        public bool FillDay4 { get; set; }
        public bool FillDay5 { get; set; }
        public bool FillDay6 { get; set; }
        public bool FillDay7 { get; set; }
        public bool IsDefault { get; set; }
    }
}
