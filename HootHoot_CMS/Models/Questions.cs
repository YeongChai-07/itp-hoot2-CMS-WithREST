/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/
//using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace HootHoot_CMS.Models
{
    [Bind(Exclude="question_id")]
    public class Questions
    {
        public Questions() { }

        [Key]
        public int question_id { get; set; }

        [Required(ErrorMessage = "Please enter the associated Station ID.")]
        [Display (Name="Associated Station ID: ")]
        public int station_id { get; set; }
        
        [Required(ErrorMessage="Please enter the Question title .")]
        [Display (Name="Question:")]
        public string question_name { get; set; }

        [Required(ErrorMessage = "Please enter the type of question .")]
        [Display(Name="Type of question:")]
        public string question_type { get; set; }

        [Required(ErrorMessage = "Please select the type of options this question has .")]
        [Display(Name= "Type of Options:")]
        public string option_type { get; set; }

        [Required(ErrorMessage="Please provide the FIRST option .")]
        [Display(Name = "Option 1:")]
        public string option_1 { get; set; }

        [Required(ErrorMessage = "Please provide the SECOND option .")]
        [Display(Name = "Option 2:")]
        public string option_2 { get; set; }

        [Required(ErrorMessage = "Please provide the THIRD option .")]
        [Display(Name = "Option 3:")]
        public string option_3 { get; set; }

        [Required(ErrorMessage = "Please provide the FOURTH option .")]
        [Display(Name = "Option 4:")]
        public string option_4 { get; set; }

        [Required(ErrorMessage = "Please provide the CORRECT option for this question .")]
        [Display(Name = "Correct Option:")]
        public string correct_option { get; set; }

        [Required(ErrorMessage = "Please enter the duration of this question .")]
        [Display(Name ="Maximum time allowed to answer this question: ")]
        [Range(20,60)]
        public int answering_duration { get; set; }

        [ForeignKey("station_id")]
        [JsonIgnore]
        public Stations station { get; set; }
    }
}
