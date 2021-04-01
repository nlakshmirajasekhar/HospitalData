using System;
using System.Collections.Generic;

namespace HospitalData.Models
{
    public partial class Labs
    {
        public int TestId { get; set; }
        public string Testname { get; set; }
        public DateTime? Dat { get; set; }
        public int? AdminssionId { get; set; }
        public double? LabAmt { get; set; }

        public virtual PatientAdmissions Adminssion { get; set; }
    }
}
