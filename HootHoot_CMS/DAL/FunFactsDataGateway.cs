using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class FunFactsDataGateway:DataGateway<FunFacts>
    {
        public FunFacts SelectByStationID(int station_ID)
        {
            return this.dbData
                .Where(fact => fact.station_ID.Equals(station_ID))
                .First();
        }
    }
}