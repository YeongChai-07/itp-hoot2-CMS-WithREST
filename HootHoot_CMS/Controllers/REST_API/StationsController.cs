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

        // GET: api/Stations
        public IQueryable<Stations> GetStations()
        {
            return db.Stations;
        }

        // GET: api/Stations/5
        [ResponseType(typeof(Stations))]
        public IHttpActionResult GetStation(int id)
        {
            Stations stations = db.Stations.Find(id);
            if (stations == null)
            {
                return NotFound();
            }

            return Ok(stations);
        }

        // PUT: api/Stations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStations(int id, Stations stations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stations.station_ID)
            {
                return BadRequest();
            }

            db.Entry(stations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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

        // POST: api/Stations
        [ResponseType(typeof(Stations))]
        public IHttpActionResult PostStations(Stations stations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stations.Add(stations);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stations.station_ID }, stations);
        }

        // DELETE: api/Stations/5
        [ResponseType(typeof(Stations))]
        public IHttpActionResult DeleteStations(int id)
        {
            Stations stations = db.Stations.Find(id);
            if (stations == null)
            {
                return NotFound();
            }

            db.Stations.Remove(stations);
            db.SaveChanges();

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
            return db.Stations.Count(e => e.station_ID == id) > 0;
        }
    }
}