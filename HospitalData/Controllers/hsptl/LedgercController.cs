using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalData.Models;
namespace HospitalData.Controllers
{
    public class PatientLedgerInfo
    {
        public int? pid { get; set; }
        public String pname { get; set; }
        public DateTime dat { get; set; }
        public string account { get; set; }
        public double debit { get; set; }
        public double credit { get; set; }

    }
    public class PatientLedgerInfoWrapper
    {
        public List<PatientLedgerInfo> ledger { get; set; }
        public double balanceAmt { get; set; }
    }
    public class LedgercController : Controller
    {

        [HttpGet]
        [Route("api/Ledgerc/getBillingInfo")]
        public PatientLedgerInfoWrapper getBillingInfo(int admId)
        {
            try
            {
                hospitalsContext db = new hospitalsContext();
                PatientLedgerInfoWrapper det = new PatientLedgerInfoWrapper();

                List<PatientLedgerInfo> ledger = new List<PatientLedgerInfo>();

                var joining = db.PatientAdmissions.Where(a => a.AdminssionId == admId).FirstOrDefault();
                String patientname = "";
                int? paid = 0;
                if (joining != null)
                {
                    paid = joining.PatientId;
                    patientname = db.PatientRegistrations.Where(a => a.PatientId == joining.PatientId).Select(b => b.PatientName).FirstOrDefault();
                    decimal days = Math.Ceiling((decimal)(DateTime.Now - joining.JoiningDate).Value.Days);
                    var rent = (double)days * joining.DailyAmt;
                    ledger.Add(new PatientLedgerInfo
                    {
                        pid = paid,
                        pname = patientname,
                        dat = DateTime.Now,
                        account = "Room Rent",
                        debit = (double)rent,
                        credit = 0
                    });


                    ledger.AddRange((from a in db.Consultations.Where(a => a.AdminssionId == admId)
                                     select new PatientLedgerInfo
                                     {
                                         pid = paid,
                                         pname = patientname,
                                         dat = (DateTime)a.Dat,
                                         account = "Consultation Charges of " + a.DrName,
                                         debit = (double)a.ConsultationAmt,
                                         credit = 0
                                     }

                                      ).ToList());


                    ledger.AddRange((from a in db.Labs.Where(a => a.AdminssionId == admId)
                                     select new PatientLedgerInfo
                                     {
                                         pid = paid,
                                         pname = patientname,
                                         dat = (DateTime)a.Dat,
                                         account = "Lab Charges of " + a.Testname,
                                         debit = (double)a.LabAmt,
                                         credit = 0
                                     }

                                     ).ToList());
                    ledger.AddRange((from a in db.Dispensary.Where(a => a.AdminssionId == admId)
                                     select new PatientLedgerInfo
                                     {
                                         pid = paid,
                                         pname = patientname,
                                         dat = (DateTime)a.Dat,
                                         account = "Medicine Charges ",
                                         debit = (double)a.BillAmt,
                                         credit = 0
                                     }

                                    ).ToList());

                    ledger.AddRange((from a in db.Receipts.Where(a => a.AdminssionId == admId)
                                     select new PatientLedgerInfo
                                     {
                                         pid = paid,
                                         pname = patientname,
                                         dat = (DateTime)a.Dat,
                                         account = "Receipt amount",
                                         debit = 0,
                                         credit = (double)a.Amt
                                     }

                                    ).ToList());




                    det.ledger = ledger.OrderBy(a => a.dat).ToList();
                    det.balanceAmt = ledger.Sum(a => a.debit - a.credit);
                }
                else
                {
                    det = null;
                }


                return det;
            }
            catch (Exception ee)
            {
                return null;
            }
        }

    }
}
