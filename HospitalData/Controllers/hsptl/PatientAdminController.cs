using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalData.Models;

namespace HospitalData.Controllers.hsptl
{
    public class Patientadminwrapper
    {
        public PatientAdmissions ptad { get; set; }
        public int trans { get; set;}
        public String result { get; set; }
    
    
    }



    public class PatientAdminController : Controller
    {
        [HttpGet]
        [Route("api/PatientAdmin/getPatientadmin")]
        public List<PatientAdmissions> getPatientadmin()

        {
            hospitalsContext db = new hospitalsContext();
            return db.PatientAdmissions.ToList();
        }
        [HttpGet]
        [Route("api/PatientAdmin/getPatientadminbyID")]
        public IQueryable<PatientAdmissions> getPatientadminbyID(int aid)

        {
            hospitalsContext db = new hospitalsContext();
            return db.PatientAdmissions.Where(a => a.AdminssionId == aid);
        }
        [HttpPost]
        [Route("api/PatientAdmin/Postadmin")]
        public Patientadminwrapper Postadmin([FromBody] Patientadminwrapper paw)
        {
            String msg = "";
            hospitalsContext db = new hospitalsContext();
            try
            {
                switch (paw.trans)
                {
                    case 1:
                        db.PatientAdmissions.Add(paw.ptad);
                        db.SaveChanges();
                        msg = "ok";
                        break;
                    case 2:
                        var pu = db.PatientAdmissions.Where(a => a.AdminssionId == paw.ptad.AdminssionId).FirstOrDefault();
                        pu.ClosingDate = paw.ptad.ClosingDate;
                        pu.JoiningDate = paw.ptad.JoiningDate;
                        pu.Pos = paw.ptad.Pos;
                        pu.Roomno = paw.ptad.Roomno;
                        pu.DailyAmt = paw.ptad.DailyAmt;
                        db.SaveChanges();
                        msg = "ok";
                        break;
                    case 3:
                        var pd = db.PatientAdmissions.Where(a => a.AdminssionId == paw.ptad.AdminssionId && a.Pos == 0).FirstOrDefault();
                        db.PatientAdmissions.Remove(pd);
                        db.SaveChanges();
                        msg = "ok";
                        break;
                }

            }
            catch(Exception ee)
            {
                msg = ee.Message;

            }


            paw.result = msg;
            return paw;

        }
        


    }
}
