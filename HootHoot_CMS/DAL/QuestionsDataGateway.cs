using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class QuestionsDataGateway:DataGateway<Questions>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Questions> SelectAll_Joint()
        {
            // Retrieving the Dataset returned from the data source using the "Eager-Loading" strategy
            // More details are availabel from:
            // http://www.codeproject.com/Articles/732426/Deferred-Execution-Vs-Lazy-Loading-Vs-Eager-Loadin
            return dbData.Include(q => q.optionType).Include(q => q.questionType).Include(q => q.station).AsEnumerable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Questions SelectById(int? id)
        {
            return SelectAll_Joint().Where(question => question.question_id == id).First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        public IEnumerable<Questions> SelectByStationID(string stationID)
        {
            RandomItem_Generator<Questions> qnsGenerate = new RandomItem_Generator<Questions>
                (this.dbData.Where(qns => qns.station_id.Equals(stationID)).ToList(), Constants.MAX_QUESTION_ITEMS);

            qnsGenerate.preparesRandomIndex();

            return qnsGenerate.getRandomItem().AsEnumerable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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