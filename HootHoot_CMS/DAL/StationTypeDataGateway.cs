using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class StationTypeDataGateway:DataGateway<StationType>
    {
        public IEnumerable<StationType> SelectAll_Joint()
        {
            return dbData.Include(st => st.station).AsEnumerable();

        }

        public StationType SelectById(string id)
        {
            return dbData.Find(id);
        }
    }
}