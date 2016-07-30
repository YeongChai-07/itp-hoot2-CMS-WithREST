using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;

using HootHoot_CMS.DAL;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.Controllers.REST_API
{
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class FunFactsController : ApiController
    {
        private FunFactsDataGateway funfactsGateway = new FunFactsDataGateway();

        // GET: api/FunFacts
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FunFacts> GetFunFacts()
        {
            return funfactsGateway.SelectAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(FunFacts))]
        public FunFacts GetFunFact()
        {
            return funfactsGateway.SelectRandomFunFact();
        }

        // GET: api/FunFacts/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station_name"></param>
        /// <returns></returns>
        [ResponseType(typeof(FunFacts))]
        public FunFacts GetFunFact(string station_name)
        {
            return funfactsGateway.SelectByStationName(station_name);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}