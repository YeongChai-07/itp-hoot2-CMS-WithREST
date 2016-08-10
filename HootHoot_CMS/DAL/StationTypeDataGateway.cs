using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class StationTypeDataGateway:DataGateway<StationType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationType> SelectAll_Joint()
        {
            // Retrieving the Dataset returned from the data source using the "Eager-Loading" strategy
            // More details are availabel from:
            // http://www.codeproject.com/Articles/732426/Deferred-Execution-Vs-Lazy-Loading-Vs-Eager-Loadin
            return dbData.Include(st => st.station).AsEnumerable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StationType SelectById(string id)
        {
            return dbData.Find(id);
        }
    }
}