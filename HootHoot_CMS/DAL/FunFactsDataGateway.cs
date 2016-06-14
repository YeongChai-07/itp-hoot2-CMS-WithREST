using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class FunFactsDataGateway:DataGateway<FunFacts>
    {
        public FunFacts SelectRandomFunFact ()
        {
            RandomItem_Generator<FunFacts> random = new RandomItem_Generator<FunFacts>
                (this.SelectAll().ToList());

            return random.getRandomItem(1).First();
        }
        public FunFacts SelectByStationID(int stationID)
        {
            return this.dbData
                .Where(fact => fact.station_id.Equals(stationID))
                .First();
        }
    }
}