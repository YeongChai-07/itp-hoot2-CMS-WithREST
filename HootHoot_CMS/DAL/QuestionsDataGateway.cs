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
        public IEnumerable<Questions> SelectByStationID(string stationID)
        {
            RandomItem_Generator<Questions> qnsGenerate = new RandomItem_Generator<Questions>
                (this.dbData.Where(qns => qns.station_id.Equals(stationID)).ToList(), MAX_QUESTION_ITEMS);

            qnsGenerate.preparesRandomIndex();

            /*return this.dbData
                .Where(qns => qns.station_id.Equals(stationID)).Take(MAX_QUESTION_ITEMS)
                .ToList();*/
            return qnsGenerate.getRandomItem().AsEnumerable();
        }
        public IEnumerable<string> GetStationName_StationID()
        {
            //First we will retrieve ALL Distinct station_id from the Questions Table
            /*var distinctStationID = (from question in dbData
                                     select question);*/
            IEnumerable<Stations> allStations = new StationDataGateway().SelectAll();
            var stationNames = (from station in allStations
                                join question in dbData
                                on station.station_id equals question.station_id
                                select station.station_name);

            return stationNames.Distinct();

        }
    }
}