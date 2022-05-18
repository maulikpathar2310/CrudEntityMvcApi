using CrudEntityMvcApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrudEntityMvcApi.Controllers
{
    public class EmpController : ApiController
    {
        CrudEntityApiEntities dbobj = new CrudEntityApiEntities();

        public IHttpActionResult getemp()
        {           
            var res = dbobj.EmpRegs.ToList();
            return Ok(res);
        }

        [HttpPost]
        public IHttpActionResult insertemp(EmpReg model)
        {
            dbobj.EmpRegs.Add(model);
            dbobj.SaveChanges();
            return Ok();
        }

        public IHttpActionResult GetEmpid(int id)
        {
            EmpClass empdetails = null;
            empdetails = dbobj.EmpRegs.Where(x => x.Empid == id).Select(x => new EmpClass()
            {
                Empid=x.Empid,
                Empname=x.Empname,
                Empemail=x.Empemail,
                Emplocation=x.Emplocation,
                Empdesgination=x.Empdesgination
            }).FirstOrDefault<EmpClass>();
            if(empdetails == null)
            {
                return NotFound();
            }
            return Ok(empdetails);
        }

        public IHttpActionResult Put(EmpClass model)
        {
            var updatedata = dbobj.EmpRegs.Where(x => x.Empid == model.Empid).FirstOrDefault<EmpReg>();
            if(updatedata != null)
            {
                updatedata.Empid = model.Empid;
                updatedata.Empname = model.Empname;
                updatedata.Empemail = model.Empemail;
                updatedata.Emplocation = model.Emplocation;
                updatedata.Empdesgination = model.Empdesgination;
                dbobj.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        public IHttpActionResult delete(int id)
        {
            var empdel = dbobj.EmpRegs.Where(x => x.Empid == id).FirstOrDefault();
            dbobj.Entry(empdel).State = System.Data.Entity.EntityState.Deleted;
            dbobj.SaveChanges();
            return Ok();
        }
    }
}
