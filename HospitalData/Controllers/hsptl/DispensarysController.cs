using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalData.Models;

namespace HospitalData.Controllers.hsptl
{
    public class DispensarysController : Controller
    {
        [HttpGet]
        [Route("api/Labc/getd")]
        public IQueryable<Dispensary> getd(String? str)
        {
            hospitalsContext db = new hospitalsContext();
            return db.Dispensary.Where(a=>a.Billno.c
        }


    }
}
