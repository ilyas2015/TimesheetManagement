using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class TsWeekEntry
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

        
        public DateTime? Day1 { get; set; }
        public TimeSpan Day1Hours { get; set; }
        public DateTime? Day1StartTime { get; set; }
        public DateTime? Day1EndTime { get; set; }

        public DateTime? Day2 { get; set; }
        public TimeSpan Day2Hours { get; set; }
        public DateTime? Day2StartTime { get; set; }
        public DateTime? Day2EndTime { get; set; }

        public DateTime? Day3 { get; set; }
        public TimeSpan Day3Hours { get; set; }
        public DateTime? Day3StartTime { get; set; }
        public DateTime? Day3EndTime { get; set; }

        public DateTime? Day4 { get; set; }
        public TimeSpan Day4Hours { get; set; }
        public DateTime? Day4StartTime { get; set; }
        public DateTime? Day4EndTime { get; set; }

        public DateTime? Day5 { get; set; }
        public TimeSpan Day5Hours { get; set; }
        public DateTime? Day5StartTime { get; set; }
        public DateTime? Day5EndTime { get; set; }

        public DateTime? Day6 { get; set; }
        public TimeSpan Day6Hours { get; set; }
        public DateTime? Day6StartTime { get; set; }
        public DateTime? Day6EndTime { get; set; }

        public DateTime? Day7 { get; set; }
        public TimeSpan Day7Hours { get; set; }
        public DateTime? Day7StartTime { get; set; }
        public DateTime? Day7EndTime { get; set; }
    }
}
