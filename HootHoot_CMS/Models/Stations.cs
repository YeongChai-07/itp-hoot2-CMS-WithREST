using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HootHoot_CMS.Models
{
    public class Stations
    {
        public Stations(){ }

        [Key]
        public string station_id { get; set; }
        public string station_name { get; set; }
        public string station_type_id { get; set; }
        [NotMapped]
        public string station_type { get; set; }

        [JsonIgnore]
        public ICollection<Questions> questions { get; set; }

        [ForeignKey("station_type_id")]
        [JsonIgnore]
        public StationType stationtype { get; set; }
    }
}