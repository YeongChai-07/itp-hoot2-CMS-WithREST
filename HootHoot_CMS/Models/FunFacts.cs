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
        [Key]
        public int funFact_ID { get; set; }
        [ForeignKey ("station_ID")]
        public int station_ID { get; set; }

        public string funFact_Desc { get; set; }
    }
}
