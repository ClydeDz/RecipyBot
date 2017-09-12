using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipyBotWeb.Models
{
    public class ContactUsDataModel
    {
        [Required(ErrorMessage = "This is a required field")]
        [MinLength(1, ErrorMessage = "A minimum of 1 character is required")]
        [MaxLength(20, ErrorMessage = "Only a maximum of 20 characters are allowed for the name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [MaxLength(200, ErrorMessage = "Only a maximum of 200 characters are allowed for this field")]
        [Display(Name = "Comments")]
        public string Comments { get; set; }
    }
}