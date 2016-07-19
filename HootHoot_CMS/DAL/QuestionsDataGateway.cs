using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class QuestionsDataGateway:DataGateway<Questions>
    {
        public IEnumerable<Questions> SelectByStationID(string stationID)
        {
            RandomItem_Generator<Questions> qnsGenerate = new RandomItem_Generator<Questions>
                (this.dbData.Where(qns => qns.station_id.Equals(stationID)).ToList(), Constants.MAX_QUESTION_ITEMS);

            qnsGenerate.preparesRandomIndex();

            return qnsGenerate.getRandomItem().AsEnumerable();
        }

        public IEnumerable<KeyValuePair<string,string>> GetStationHasQuestions()
        {
            //First we will retrieve ALL Distinct station_id from the Stations Table
            IEnumerable<Stations> allStations = new StationDataGateway().SelectAll();
            
            var stationsHasQuestions = (from station in allStations
                                        join question in dbData
                                        on station.station_id equals question.station_id
                                        select station).Distinct();

            Dictionary<string, string> stationsKVP = new Dictionary<string, string>();

            foreach(Stations station in stationsHasQuestions)
            {
                stationsKVP.Add(station.station_id, station.station_name);
            }

            return stationsKVP;

        }
    }
}