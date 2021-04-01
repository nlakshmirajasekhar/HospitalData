using System;
using System.Collections.Generic;

namespace HospitalData.Models
{
    public partial class Consultations
    {
        public int ConsultationId { get; set; }
        public string DrName { get; set; }
        public DateTime? Dat { get; set; }
        public int? AdminssionId { get; set; }
        public double? ConsultationAmt { get; set; }

        public virtual PatientAdmissions Adminssion { get; set; }
    }
}
