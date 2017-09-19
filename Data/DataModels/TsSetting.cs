using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataModels
{
    public class TsSetting
    {
        public int TsSettingId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessHST { get; set; }
        public string InvoiceToCompanyName { get; set; }
        public string InvoiceToCompanyAddress { get; set; }
        public string InvoiceProject { get; set; }
        public decimal HourRate { get; set; }
        public decimal HST { get; set; }
        public int LastInvoiceNumber { get; set; }
        public string UserId { get; set; }        
    }
}
