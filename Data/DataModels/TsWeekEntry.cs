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

        public List<TsDayEntry> Days { get; set; }


    }
}
