/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using Newtonsoft.Json;

namespace HootHoot_CMS.Models
{
    [JsonObject (IsReference =true)]
    public class Stations
    {
        public Stations()
        {
            questions = new List<Questions>();
            funfacts = new List<FunFacts>();
        }
        [Key]
        public int station_ID { get; set; }
        public string station_Name { get; set; }
        public string frameURL { get; set; }

        [JsonIgnore]
        public ICollection<Questions> questions { get; set; }
        [JsonIgnore]
        public ICollection<FunFacts> funfacts { get; set; }
    }
}
