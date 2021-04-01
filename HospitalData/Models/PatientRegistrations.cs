using System;
using System.Collections.Generic;

namespace HospitalData.Models
{
    public partial class PatientRegistrations
    {
        public PatientRegistrations()
        {
            PatientAdmissions = new HashSet<PatientAdmissions>();
        }

        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Mobilenumber { get; set; }
        public string Addr { get; set; }
        public string City { get; set; }

        public virtual ICollection<PatientAdmissions> PatientAdmissions { get; set; }
    }
}
