using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HootHoot_CMS.Models
{
    public class StationType
    {
        public StationType(){ }
        
        [Key]
        public string station_type_id { get; set; }
        public string station_type { get; set; }

        public ICollection<Stations> station { get; set; }
    }
}