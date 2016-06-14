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
using HootHoot_CMS.DAL;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.Controllers.REST_API
{
    public class StationsController : ApiController
    {
        private HootHootDbContext db = new HootHootDbContext();
        private StationDataGateway sdg = new StationDataGateway();

        // GET: api/Stations
        public IEnumerable<Stations> GetStations()
        {
            //return db.Stations;
            return sdg.SelectAll();
        }

        // GET: api/Stations/5
        [ResponseType(typeof(Stations))]
        public IHttpActionResult GetStations(int id)
        {
            //Stations stations = db.Stations.Find(id);
            Stations stations = sdg.SelectById(id);
            if (stations == null)
            {
                return NotFound();
            }

            return Ok(stations);
        }

        // UPDATE Stations by station ID
        // PUT: api/Stations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStations(int id, Stations stations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stations.station_id)
            {
                return BadRequest();
            }

            //db.Entry(stations).State = EntityState.Modified;


            try
            {
                //db.SaveChanges();
                sdg.Update(stations);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationsExists(id))
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


        // CREATE new Station
        // POST: api/Stations
        [ResponseType(typeof(Stations))]
        public IHttpActionResult PostStations(Stations stations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sdg.Insert(stations);

            return CreatedAtRoute("DefaultApi", new { id = stations.station_id }, stations);
        }

        // DELETE Stations
        // DELETE: api/Stations/5
        [ResponseType(typeof(Stations))]
        public IHttpActionResult DeleteStations(int id)
        {
            Stations stations = sdg.SelectById(id);
            if (stations == null)
            {
                return NotFound();
            }

            sdg.Delete(stations);

            return Ok(stations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StationsExists(int id)
        {
            return db.Stations.Count(e => e.station_id == id) > 0;
        }
    }
}