﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class TsDayEntry
    {
        public int TsDayEntryId { get; set; }
        [Required]
        public DateTime EventDate { get; set; }

        public int TsEntryId { get; set; }
        [Required]
        public TsEntry TsEntry { get; set; }
        [Required]
        public TimeSpan Hours { get; set; }
    }
}