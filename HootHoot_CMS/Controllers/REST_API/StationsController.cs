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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Stations> GetStations()
        {
            //return db.Stations;
            return stationGateway.SelectAll();
        }

        // GET: api/Stations/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station_id"></param>
        /// <returns></returns>
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
    }
}
 