using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HootHoot_CMS.Models
{
    public class OptionType
    {
        public OptionType() { }

        [Key]
        public string optiontype { get; set; }

        public ICollection<Questions> question { get; set; }
    }
}