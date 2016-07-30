using System.Collections.Generic;
using System.Linq;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class FunFactsDataGateway : DataGateway<FunFacts>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunFacts SelectRandomFunFact()
        {
            return getRandom_FunFact(this.SelectAll().ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public FunFacts SelectByStationName(string stationName)
        {
            return getRandom_FunFact(this.dbData
                .Where(fact => fact.station_name.Equals(stationName)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inFunFacts"></param>
        /// <returns></returns>
        private FunFacts getRandom_FunFact(List<FunFacts> inFunFacts)
        {
            RandomItem_Generator<FunFacts> random = new RandomItem_Generator<FunFacts>
                (inFunFacts, Constants.MAX_RANDOM_FUNFACTS);
            random.preparesRandomIndex();

            return random.getRandomItem().First();
        }
    }
}