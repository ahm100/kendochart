using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ken4read.Models;
using Kendo.DynamicLinq;
using Newtonsoft.Json;

namespace ken4read.Controllers
{
    public class EmployeeListsController : ApiController
    {
        private mycontext db = new mycontext();

        public class DataSourceRequest
        {
            public int Take { get; set; }
            public int Skip { get; set; }
            public IEnumerable<Sort> Sort { get; set; }
            public Filter Filter { get; set; }
        }

        // GET: api/EmployeeLists
        //public DataSourceResult GetEmployeeList(HttpRequestMessage requestMessage)
        //{

        //    DataSourceRequest request = JsonConvert.DeserializeObject<DataSourceRequest>(
        //        // The request is in the format GET api/products?{take:10,skip:0} and ParseQueryString treats it as a key without value
        //        requestMessage.RequestUri.ParseQueryString().GetKey(0)
        //    );

        //    var res = db.EmployeeList.OrderBy(x => x.EmployeeId).Select( x=> new { EmployeeId=x.EmployeeId, FirstName =x.FirstName, LastName =x.LastName, Company =x.Company}).ToList()
        //        .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        //    return res;
        //}
        public IHttpActionResult GetEmployeeList()
        {
            var res = db.EmployeeList.OrderBy(x=>x.LastName).Select(x => new { EmployeeId = x.EmployeeId, FirstName = x.FirstName, LastName = x.LastName, Company = x.Company }).ToList();
            return Ok(res);
        }

        // GET: api/EmployeeLists/5

        [ResponseType(typeof(EmployeeList))]
        public IHttpActionResult GetEmployeeList(int id)
        {
            EmployeeList employeeList = db.EmployeeList.Find(id);
            if (employeeList == null)
            {
                return NotFound();
            }

            return Ok(employeeList);
        }

        // PUT: api/EmployeeLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeList(int id, EmployeeList employeeList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeList.EmployeeId)
            {
                return BadRequest();
            }

            db.Entry(employeeList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmployeeLists
        [ResponseType(typeof(EmployeeList))]
        public IHttpActionResult PostEmployeeList(EmployeeList employeeList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeList.Add(employeeList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeList.EmployeeId }, employeeList);
        }

        // DELETE: api/EmployeeLists/5
        [ResponseType(typeof(EmployeeList))]
        public IHttpActionResult DeleteEmployeeList(int id)
        {
            EmployeeList employeeList = db.EmployeeList.Find(id);
            if (employeeList == null)
            {
                return NotFound();
            }

            db.EmployeeList.Remove(employeeList);
            db.SaveChanges();

            return Ok(employeeList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeListExists(int id)
        {
            return db.EmployeeList.Count(e => e.EmployeeId == id) > 0;
        }
    }
}