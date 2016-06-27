/*using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;*/

using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using HootHoot_CMS.DAL;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.Controllers.REST_API
{
    [EnableCors(origins: "http://hootsq-mantro.azurewebsites.net", headers:"*", methods:"*") ]
    public class FunFactsController : ApiController
    {
        private FunFactsDataGateway funfactsGateway = new FunFactsDataGateway();

        // GET: api/FunFacts
        public IEnumerable<FunFacts> GetFunFacts()
        {
            //return db.FunFacts;
            return funfactsGateway.SelectAll();
        }

        [ResponseType(typeof(FunFacts))]
        public FunFacts GetFunFact()
        {
            return funfactsGateway.SelectRandomFunFact();
        }

        // GET: api/FunFacts/5
        [ResponseType(typeof(FunFacts))]
        public FunFacts GetFunFact(string station_ID)
        {
            return funfactsGateway.SelectByStationID(station_ID);
        }


        // COMMENTED OUT Codes (NOT IN USE CODES) 

        /* private HootHootDbContext db = new HootHootDbContext();

        // GET: api/FunFacts/5
        [ResponseType(typeof(FunFacts))]
        public IHttpActionResult GetFunFacts(int id)
        {
            FunFacts funFacts = db.FunFacts.Find(id);
            if (funFacts == null)
            {
                return NotFound();
            }

            return Ok(funFacts);
        }

        // PUT: api/FunFacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFunFacts(int id, FunFacts funFacts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != funFacts.funfact_id)
            {
                return BadRequest();
            }

            db.Entry(funFacts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FunFactsExists(id))
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

        // POST: api/FunFacts
        [ResponseType(typeof(FunFacts))]
        public IHttpActionResult PostFunFacts(FunFacts funFacts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FunFacts.Add(funFacts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = funFacts.funfact_id }, funFacts);
        }

        // DELETE: api/FunFacts/5
        [ResponseType(typeof(FunFacts))]
        public IHttpActionResult DeleteFunFacts(int id)
        {
            FunFacts funFacts = db.FunFacts.Find(id);
            if (funFacts == null)
            {
                return NotFound();
            }

            db.FunFacts.Remove(funFacts);
            db.SaveChanges();

            return Ok(funFacts);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FunFactsExists(int id)
        {
            return db.FunFacts.Count(e => e.funfact_id == id) > 0;
        } 
         
         */

    }
}