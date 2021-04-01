using System;
using System.Collections.Generic;

namespace HospitalData.Models
{
    public partial class PatientAdmissions
    {
        public PatientAdmissions()
        {
            Consultations = new HashSet<Consultations>();
            Dispensary = new HashSet<Dispensary>();
            Labs = new HashSet<Labs>();
            Receipts = new HashSet<Receipts>();
        }

        public int AdminssionId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string Roomno { get; set; }
        public double? DailyAmt { get; set; }
        public int? Pos { get; set; }
        public DateTime? ClosingDate { get; set; }

        public virtual PatientRegistrations Patient { get; set; }
        public virtual ICollection<Consultations> Consultations { get; set; }
        public virtual ICollection<Dispensary> Dispensary { get; set; }
        public virtual ICollection<Labs> Labs { get; set; }
        public virtual ICollection<Receipts> Receipts { get; set; }
    }
}
