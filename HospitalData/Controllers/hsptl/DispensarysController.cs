using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalData.Models;

namespace HospitalData.Controllers.hsptl
{

    public class DispensaryWrapper
    {
        public Dispensary dis { get; set; }
        public int trans { get; set; }
        public String result { get; set; }
    
    
    
    }

    public class DispensarysController : Controller
    {
        [HttpGet]
        [Route("api/Dispensarys/getd")]
        public IQueryable<Dispensary> getd(int str)
        {
            hospitalsContext db = new hospitalsContext();
            return db.Dispensary.Where(a =>a.Billno==str);
        }
        [HttpPost]
        [Route("api/Dispensarys/postd")]
        public DispensaryWrapper postd([FromBody] DispensaryWrapper di)

        {
            String msg = "";
            hospitalsContext db = new hospitalsContext();
            try
            {
                switch(di.trans)
                {
                    case 1:
                        db.Dispensary.Add(di.dis);
                        db.SaveChanges();
                        msg = "ok";
                        break;
                    case 2:
                        var du = db.Dispensary.Where(a => a.Billno == di.dis.Billno).FirstOrDefault();
                        du.BillAmt = di.dis.BillAmt;
                        du.Dat = di.dis.Dat;
                        du.BillAmt = di.dis.BillAmt;
                        db.SaveChanges();
                        msg = "ok";
                        break;
                    case 3:
                        if(Diss(di.dis.AdminssionId))
                        {
                            var dd = db.Dispensary.Where(a => a.Billno == di.dis.Billno).FirstOrDefault();
                            db.Dispensary.Remove(dd);
                            db.SaveChanges();
                            msg = "ok";
                        }
                        else
                        {
                            msg = "not exists";
                        }
                        break;
            }
            }
            catch(Exception ee)
            {
                msg = ee.Message;
            }
            di.result = msg;
            return di;

        }
        private Boolean Diss(int? aid)
        {
            hospitalsContext db = new hospitalsContext();
            var x = db.PatientAdmissions.Where(a => a.AdminssionId == aid).FirstOrDefault();
            if(x==null)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

    }
}
