using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalData.Models;

namespace HospitalData.Controllers.hsptl
{
    public class consulationwrapper
    {
        public Consultations con { get; set; }
        public int trans { get; set; }
        public String result { get; set; }
    }
    public class ConsultancycController : Controller
    {
      [HttpGet]
      [Route("api/Consultancyc/getConsultancy")]
      public IQueryable<Consultations> getConsultancy(int coid)
        {
            hospitalsContext db = new hospitalsContext();
            return db.Consultations.Where(a => a.ConsultationId == coid);
        }
        [HttpPost]
        [Route("api/Consultancyc/postcon")]
        public consulationwrapper postcon([FromBody] consulationwrapper cw)
        {
            hospitalsContext db = new hospitalsContext();
            string msg = "";
            try
            {
                switch (cw.trans)
                {
                    case 1:
                        if (descision(cw.con.AdminssionId))
                        {
                            db.Consultations.Add(cw.con);
                            db.SaveChanges();
                            msg = "ok";
                        }
                        else
                        {
                            msg = "not possible";
                        }
                        break;
                    case 2:
                        var pat = db.Consultations.Where(a => a.ConsultationId == cw.con.ConsultationId).FirstOrDefault();
                        pat.AdminssionId = cw.con.AdminssionId;
                        pat.ConsultationAmt = cw.con.ConsultationAmt;
                        pat.Dat = cw.con.Dat;
                        db.SaveChanges();
                        msg = "ok";
                        break;
                    case 3:
                        var ptd = db.Consultations.Where(a => a.ConsultationId == cw.con.ConsultationId).FirstOrDefault();
                        db.Consultations.Remove(ptd);
                        db.SaveChanges();

                        break;



                }

            }
            catch (Exception ee)
            {
                msg = ee.Message;
            }

            
            
            cw.result = msg;



            return cw;
        }
        public Boolean descision(int? pid)
        {
            var b = false;
            hospitalsContext db = new hospitalsContext();
            var y = db.PatientAdmissions.Where(a => a.PatientId == pid && a.Pos == 1).FirstOrDefault();
            if (y != null)
            {
                b = true;
            }

            return b;
        }
    }
}
