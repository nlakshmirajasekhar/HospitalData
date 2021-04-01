using System;
using System.Collections.Generic;

namespace HospitalData.Models
{
    public partial class Receipts
    {
        public int ReceiptNo { get; set; }
        public DateTime? Dat { get; set; }
        public int? AdminssionId { get; set; }
        public double? Amt { get; set; }

        public virtual PatientAdmissions Adminssion { get; set; }
    }
}
