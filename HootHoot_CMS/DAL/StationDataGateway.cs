﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.DAL
{
    public class StationDataGateway : DataGateway<Stations>
    {
        public Stations SelectByStationID(string stationID)
        {
            return dbData.Find(stationID);
        }

        public new IEnumerable<Stations> SelectAll()
        {
            StationTypeDataGateway gateway = new StationTypeDataGateway();
            IEnumerable<Stations> allStations = dbData.AsEnumerable<Stations>();

            foreach (Stations station in allStations)
            {
                StationType type = gateway.SelectById(station.station_type_id);
                station.station_type = type.station_type;
            }

            return allStations;
        }

        public IEnumerable<string> GetAllStationNames()
        {
            var stationNames = from station in dbData
                               select station.station_name;

            return stationNames.Distinct().AsEnumerable();
        }
    }
}