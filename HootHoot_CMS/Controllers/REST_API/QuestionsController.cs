using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

using HootHoot_CMS.DAL;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.Controllers.REST_API
{
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class QuestionsController : ApiController
    {
        private QuestionsDataGateway qnsGateway = new QuestionsDataGateway();

        // GET: api/Questions
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Questions> GetQuestions()
        {
            //return db.Questions;
            return qnsGateway.SelectAll();
        }

        // GET: api/Questions/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station_ID"></param>
        /// <returns></returns>
        public IEnumerable<Questions> GetQuestions(string station_ID)
        {
            return qnsGateway.SelectByStationID(station_ID);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}