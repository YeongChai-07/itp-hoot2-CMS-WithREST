using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class QuestionsDataGateway:DataGateway<Questions>
    {
        const int MAX_QUESTION_ITEMS = 10;
        public IEnumerable<Questions> SelectByStationID(int stationID)
        {
            RandomItem_Generator<Questions> qnsGenerate = new RandomItem_Generator<Questions>
                (this.dbData.Where(qns => qns.station_id.Equals(stationID)).ToList(), 4);

            qnsGenerate.preparesRandomIndex();

            /*return this.dbData
                .Where(qns => qns.station_id.Equals(stationID)).Take(MAX_QUESTION_ITEMS)
                .ToList();*/
            return qnsGenerate.getRandomItem().AsEnumerable();
        }
    }
}