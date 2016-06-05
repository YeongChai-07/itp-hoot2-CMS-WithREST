/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HootHoot_CMS.Models
{
    public class Questions
    {
        [Key]
        public int question_ID { get; set; }
        [ForeignKey("station_ID")]
        public int station_ID { get; set; }
        
        public string question_Name { get; set; }
        public string question_Type { get; set; }
        public string option_Type { get; set; }
        public string option_1 { get; set; }
        public string option_2 { get; set; }
        public string option_3 { get; set; }
        public string option_4 { get; set; }
    }
}
