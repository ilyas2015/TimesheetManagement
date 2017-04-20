using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class TsEntry
    {
        public int TsEntryId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal TotalHours { get; set; }
        public List<TsWeekEntry> Weeks { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
