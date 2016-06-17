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
        public int question_id { get; set; }

        public int station_id { get; set; }

        public string question_name { get; set; }
        public string question_type { get; set; }
        public string option_type { get; set; }
        public string option_1 { get; set; }
        public string option_2 { get; set; }
        public string option_3 { get; set; }
        public string option_4 { get; set; }
        public string correct_option { get; set; }

        public int answering_duration { get; set; }

        [ForeignKey("station_id")]
        [JsonIgnore]
        public Stations station { get; set; }
    }
}
