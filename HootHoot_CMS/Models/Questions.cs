/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HootHoot_CMS.Models
{
    public class Questions
    {
        public Questions() { }

        [Key]
        public int question_ID { get; set; }

        public int station_ID { get; set; }

        public string question_Name { get; set; }
        public string question_Type { get; set; }
        public string option_Type { get; set; }
        public string option_1 { get; set; }
        public string option_2 { get; set; }
        public string option_3 { get; set; }
        public string option_4 { get; set; }
        public string correct_option { get; set; }

        [ForeignKey("station_ID")]
        [JsonIgnore]
        public Stations station { get; set; }
    }
}
