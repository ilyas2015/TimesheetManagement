using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class TsDayViewModel
    {
        public int TsWeekEntryId { get; set; }
        public DateTime CurrentDate { get; set; }
        public decimal Hours { get; set; }
    }
}