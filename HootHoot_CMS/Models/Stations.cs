/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HootHoot_CMS.Models
{
    public class Stations
    {
        public Stations()
        {
            questions = new List<Questions>();
            funfacts = new List<FunFacts>();
        }
        [Key]
        public string station_id { get; set; }
        public string station_name { get; set; }
        
        public string station_type { get; set; }

        [JsonIgnore]
        public ICollection<Questions> questions { get; set; }
        [JsonIgnore]
        public ICollection<FunFacts> funfacts { get; set; }

        [ForeignKey("station_type")]
        [JsonIgnore]
        public StationType stationtype { get; set; }
    }
}
