using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HootHoot_CMS.Models
{
    public class QuestionType
    {
        public QuestionType(){ }

        [Key]
        public string questiontype { get; set; }

        public ICollection<Questions> question { get; set; }


    }
}