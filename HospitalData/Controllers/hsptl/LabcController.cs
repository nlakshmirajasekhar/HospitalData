using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalData.Models;

namespace HospitalData.Controllers.hsptl
{
    public class LabscWrapper
    {
        public Labs labs { get; set; }
        public int trans { get; set; }
        public String result { get; set; }
    }

    public class LabcController : Controller
    {
        [HttpGet]
        [Route("api/Labc/getl")]
        public List<Labs> getl()
        {
            hospitalsContext db = new hospitalsContext();
            return db.Labs.ToList();
        }

        [HttpPost]
        [Route("api/Labc/PostLabs")]
        public LabscWrapper PostLabs([FromBody] LabscWrapper lc)
        {
            hospitalsContext db = new hospitalsContext();
            String msg = "";

            try
            {
                if (descision(lc.labs.AdminssionId))
                {
                    switch (lc.trans)
                    {
                        case 1:
                            db.Labs.Add(lc.labs);
                            db.SaveChanges();
                            msg = "ok";
                            break;
                        case 2:
                            var ld = db.Labs.Where(a => a.TestId == lc.labs.TestId).FirstOrDefault();
                            ld.Testname = lc.labs.Testname;
                            ld.AdminssionId = lc.labs.AdminssionId;
                            ld.LabAmt = lc.labs.LabAmt;
                            ld.Dat = lc.labs.Dat;
                            db.SaveChanges();
                            msg = "ok";
                            break;
                        case 3:
                            var lk = db.Labs.Where(a => a.TestId == lc.labs.TestId).FirstOrDefault();
                            db.Labs.Remove(lk);
                            db.SaveChanges();
                            msg = "ok";
                            break;

                    }
                }
                else
                {
                    msg = "not exists";
                }
            }
            catch (Exception eee)
            {
                msg = eee.Message;
            }
            lc.result = msg;
            return lc;


        }



        public Boolean descision(int? pid)
        {
            var b = false;
            hospitalsContext db = new hospitalsContext();
            var y = db.PatientAdmissions.Where(a => a.PatientId == pid && a.Pos==1).FirstOrDefault();
            if (y != null)
            {
                b = true;
            }

            return b;
        }

    }
}
