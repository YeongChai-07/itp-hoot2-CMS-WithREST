using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace HootHoot_CMS.Models
{
    [Bind(Exclude = "station,questionType,optionType")]
    public class Questions
    {
        public Questions(){ }

        [Key]
        public int question_id { get; set; }

        [Required(ErrorMessage = "Please select the associated Station for this question.")]
        [Display (Name="Associated Station: ")]
        public string station_id { get; set; }
        
        [Required(ErrorMessage="Please enter the Question title .")]
        [Display (Name="Question:")]
        public string question_name { get; set; }

        [Required]
        [Display(Name= "Questions Type:")]
        public string question_type { get; set; }

        [Required]
        [Display(Name= "Option Type:")]
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

        [Required]
        [Display(Name = "Correct Option:")]
        public string correct_option { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Please select whether this question has hint(s) .")]
        [Display(Name = "Are there any hints?")]
        public string question_has_hint { get; set; }

        [Required(ErrorMessage = "Please provide the hint for the question .")]
        [Display(Name = "Question Hint:")]
        public string hint { get; set; }


        [Required(ErrorMessage = "Please enter the duration of this question .")]
        [Display(Name = "Maximum time allowed to answer (seconds): ")]
        [Range(20,60)]
        public int answering_duration { get; set; }

        [ForeignKey("station_id")]
        [JsonIgnore]
        public Stations station { get; set; }

        [ForeignKey("question_type")]
        [JsonIgnore]
        public QuestionType questionType { get; set; }

        [ForeignKey("option_type")]
        [JsonIgnore]
        public OptionType optionType { get; set; }
    }
}
