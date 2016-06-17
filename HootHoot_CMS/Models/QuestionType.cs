using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HootHoot_CMS.Models
{
    public class QuestionType
    {
        public QuestionType()
        {
            question = new List<Questions>();
        }

        [Key]
        public int questiontype_id { get; set; }
        
        public string questiontype { get; set; }

        public ICollection<Questions> question { get; set; }


    }
}