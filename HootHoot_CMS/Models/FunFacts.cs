using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HootHoot_CMS.Models
{
    public class FunFacts
    {
        public FunFacts() { }

        [Key]
        public int funfact_id { get; set; }

        public string station_name { get; set; }

        public string funfact { get; set; }

    }
}