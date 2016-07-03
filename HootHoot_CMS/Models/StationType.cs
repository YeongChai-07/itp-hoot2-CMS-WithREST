using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HootHoot_CMS.Models
{
    public class StationType
    {
        public StationType()
        {
            station = new List<Stations>();   
        }
        
        [Key]
        public string station_type_id { get; set; }
        public string station_type { get; set; }

        public ICollection<Stations> station { get; set; }
    }
}