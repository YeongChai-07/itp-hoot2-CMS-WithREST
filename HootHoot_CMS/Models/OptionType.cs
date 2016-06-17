using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace HootHoot_CMS.Models
{
    public class OptionType
    {
        public OptionType()
        {
            question = new List<Questions>();
        }

        [Key]
        public int optiontype_id { get; set; }
        
        public string optiontype { get; set; }

        public ICollection<Questions> question { get; set; }
    }
}