/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HootHoot_CMS.Models
{
    public class FunFacts
    {
        public FunFacts() { }
        [Key]
        public int funFact_ID { get; set; }

        public int station_ID { get; set; }

        public string funFact_Desc { get; set; }

        [ForeignKey("station_ID")]
        public Stations station { get; set; }
    }
}
