using System;
using System.Collections.Generic;

namespace HospitalData.Models
{
    public partial class Dispensary
    {
        public int Billno { get; set; }
        public DateTime? Dat { get; set; }
        public int? AdminssionId { get; set; }
        public double? BillAmt { get; set; }

        public virtual PatientAdmissions Adminssion { get; set; }
    }
}
