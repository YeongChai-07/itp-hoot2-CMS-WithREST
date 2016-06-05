/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/
using System.ComponentModel.DataAnnotations;

namespace HootHoot_CMS.Models
{
    public class Stations
    {
        [Key]
        public int station_ID { get; set; }
        public string station_Name { get; set; }
        public string frameURL { get; set; }
    }
}
