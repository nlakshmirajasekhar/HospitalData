using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalData.Models;

namespace HospitalData.Controllers.hsptl
{
    public class patientRegWrapper
    {
        public PatientRegistrations PRW { get; set; }
        public int trans { get; set; }
        public String result { get; set; }
    
    
    }




    public class PatientRegController : Controller
    {
        [HttpGet]
        [Route("api/PatientReg/getPatients")]
        public List<PatientRegistrations> GetPatients(String pname)
        {
            hospitalsContext db = new hospitalsContext();

            return db.PatientRegistrations.Where(a => a.PatientName.Contains(pname) || a.PatientName.Contains(pname) || a.Mobilenumber.Contains(pname)).ToList();
        }

        [HttpPost]
        [Route("api/PatientReg/postPat")]
        public patientRegWrapper postPat([FromBody] patientRegWrapper pr)
        {
            String msg=" ";
            hospitalsContext db = new hospitalsContext();
            try
            {
                switch (pr.trans)
                {
                    case 1:

                        db.PatientRegistrations.Add(pr.PRW);
                        db.SaveChanges();
                        msg = "ok";
                        break;
                    case 2:
                        var pat = db.PatientRegistrations.Where(a => a.PatientId == pr.PRW.PatientId).FirstOrDefault();
                        pat.PatientName = pr.PRW.PatientName;
                        pat.Addr = pr.PRW.Addr;
                        pat.City = pr.PRW.City;
                        db.SaveChanges();
                        msg = "ok";
                        break;
                    case 3:
                        var ptd = db.PatientRegistrations.Where(a => a.PatientId == pr.PRW.PatientId).FirstOrDefault();
                        var add = db.PatientAdmissions.Where(a=>a.Pos == 1).FirstOrDefault();
                        if(add==null)
                        {
                            db.PatientRegistrations.Remove(ptd);
                            db.SaveChanges();
                        }
                        else
                        {
                            msg = "in use";
                        }
                        break;
                 
                
                
                }

            }
            catch(Exception ee)
            {
                msg = ee.Message;
            }

            pr.result = msg;
            return pr;
        }





    }
}
