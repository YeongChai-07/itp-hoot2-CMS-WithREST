using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class StationTypeDataGateway:DataGateway<StationType>
    {
        public StationType SelectById(string id)
        {
            return dbData.Find(id);
        }
    }
}