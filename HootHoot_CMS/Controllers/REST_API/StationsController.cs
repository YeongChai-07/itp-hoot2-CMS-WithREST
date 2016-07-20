using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;

using HootHoot_CMS.DAL;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.Controllers.REST_API
{
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class StationsController : ApiController
    {
        private StationDataGateway stationGateway = new StationDataGateway();

        // GET: api/Stations
        public IEnumerable<Stations> GetStations()
        {
            //return db.Stations;
            return stationGateway.SelectAll();
        }

        // GET: api/Stations/5
        [ResponseType(typeof(Stations))]
        public IHttpActionResult GetStation(string station_id)
        {
            //Stations stations = db.Stations.Find(id);
            //Stations stations = sdg.SelectById(id);
            Stations stations = stationGateway.SelectByStationID(station_id);
            if (stations == null)
            {
                return NotFound();
            }

            return Ok(stations);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        // COMMENTED OUT Codes (NOT IN USE CODES) 

        /* private HootHootDbContext db = new HootHootDbContext();

        // UPDATE Stations by station ID
        // PUT: api/Stations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStations(string id, Stations stations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if ( ! id.Equals(stations.station_id) )
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


        // CREATE new Station
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

            return CreatedAtRoute("DefaultApi", new { id = stations.station_id }, stations);
        }

        // DELETE Stations
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

        private bool StationsExists(string id)
        {
            return db.Stations.Count(e => e.station_id == id) > 0;
        }

        */
    }
}
 