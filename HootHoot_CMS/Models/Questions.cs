/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace HootHoot_CMS.Models
{
    [DataContract(IsReference = true)]
    public class Questions
    {
        public Questions() { }

        [Key]
        public int question_ID { get; set; }
        [DataMember]
        public int station_ID { get; set; }

        [DataMember]
        public string question_Name { get; set; }
        [DataMember]
        public string question_Type { get; set; }
        [DataMember]
        public string option_Type { get; set; }
        [DataMember]
        public string option_1 { get; set; }
        [DataMember]
        public string option_2 { get; set; }
        [DataMember]
        public string option_3 { get; set; }
        [DataMember]
        public string option_4 { get; set; }
        [DataMember]
        public string correct_option { get; set; }

        [ForeignKey("station_ID")]
        [DataMember]
        public Stations station { get; set; }
    }
}
