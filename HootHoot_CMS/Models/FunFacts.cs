/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HootHoot_CMS.Models
{
    public class FunFacts
    {
        public FunFacts() { }

        [Key]
        public int funfact_id{ get; set; }
        
        public string station_id { get; set; }

        public string funfact { get; set; }

        [ForeignKey ("station_id")]
        [JsonIgnore]
        public Stations station { get; set; }
    }
}
