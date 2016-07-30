using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class StationDataGateway : DataGateway<Stations>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Stations> SelectAll_Joint()
        {
            return dbData.Include(s => s.questions).Include(s => s.stationtype).AsEnumerable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        public Stations SelectByStationID(string stationID)
        {
            //return dbData.Find(stationID);
            Stations stationByID = SelectAll_Joint().First(station => station.station_id.Equals(stationID));
            stationByID.station_type = stationByID.stationtype.station_type;

            return stationByID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Stations> SelectAll()
        {
            IEnumerable<Stations> allStations = SelectAll_Joint();

            foreach (Stations station in allStations)
            {
                station.station_type = station.stationtype.station_type;
            }

            return allStations;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllStationNames()
        {
            var stationNames = from station in dbData
                               select station.station_name;

            return stationNames.Distinct().AsEnumerable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        public string GetStationName_ByStationID(string stationID)
        {
            var stationNameById = from station in dbData
                                  where station.station_id.Equals(stationID)
                                  select station.station_name;

            return stationNameById.First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationType_ID"></param>
        /// <returns></returns>
        public IEnumerable<Stations> GetStations_ByStationType(string stationType_ID)
        {
            var stations = (from station in dbData
                              where station.station_type_id == stationType_ID
                             select station);

            return stations.AsEnumerable();
        }
    }
}