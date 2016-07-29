/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;*/
using System.ComponentModel.DataAnnotations;

namespace HootHoot_CMS.Models
{
    public class Accounts
    {
        public Accounts() { }

        [Key]
        public int user_id { get; set; }

        [Required(ErrorMessage = "Please provide your username.")]
        [Display(Name = "Username: ")]
        public string user_name { get; set; }

        [Required(ErrorMessage = "Please provide your password.")]
        [Display(Name="Password: ")]
        public string password { get; set; }

        [Display(Name ="Role: ")]
        public string user_role { get; set; }

        [Display(Name ="Full Name: ")]
        public string full_name { get; set; }
    }
}