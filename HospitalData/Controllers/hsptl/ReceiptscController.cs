using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalData.Models;

namespace HospitalData.Controllers.hsptl
{
    public class ReceiptsWrapper
    {
    
        public Receipts rec { get; set; }
        public int trans { get; set; }
        public String result { get; set; }

    
    
    }

    public class ReceiptscController : Controller
    {
        [HttpGet]
        [Route("api/Receiptsc/GetReceipts")]
        public IQueryable<Receipts> GetReceipts(int str)
        {
            hospitalsContext db = new hospitalsContext();
            return db.Receipts.Where(a => a.ReceiptNo == str);
        }
        [HttpPost]
        [Route("api/Receiptsc/PostReceiptscwrapper")]
        public ReceiptsWrapper PostReceiptscwrapper([FromBody] ReceiptsWrapper rw)
        {
            hospitalsContext db = new hospitalsContext();
            string msg = "";
            try
            {
                switch (rw.trans)
                {
                    case 1:
                        if (descision(rw.rec.AdminssionId))
                        { 
                            db.Receipts.Add(rw.rec);
                            db.SaveChanges();
                            msg = "ok";
                        }
                        else
                        {
                            msg = "not possible";
                        }
                        break;
                    case 2:
                        var pat = db.Receipts.Where(a => a.ReceiptNo == rw.rec.ReceiptNo).FirstOrDefault();
                        pat.AdminssionId = rw.rec.AdminssionId;
                        pat.Amt = rw.rec.Amt;
                        pat.Dat = rw.rec.Dat;
                        db.SaveChanges();
                        msg = "ok";
                        break;
                    case 3:
                        var ptd = db.Receipts.Where(a => a.ReceiptNo == rw.rec.ReceiptNo).FirstOrDefault();
                        db.Receipts.Remove(ptd);
                        db.SaveChanges();
                       
                        break;



                }

            }
            catch (Exception ee)
            {
                msg = ee.Message;
            }

            rw.result = msg;
            return rw;
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
