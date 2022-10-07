#pragma warning disable CS8618

using System;

using System.ComponentModel.DataAnnotations;

namespace DojoSurveyValidation.Models

{
    public class Survey
    {
        [Display(Name = "Userame")]
        [Required]
        [MinLength(5, ErrorMessage = "Field must be at least 5 characters.")]
        public string Username {get;set;}

        [Display(Name = "Location")]
        [Required]
        [MinLength(4, ErrorMessage = "Field must be at least 4 characters")]
        public string Location {get;set;}

        [Display(Name = "FavoriteLanguage")]
        [Required]
        [MinLength(1, ErrorMessage = "Please select a language.")]
        public string FavoriteLanguage {get;set;}

        [Display(Name = "Comments")]
        [Required]
        [MinLength(20, ErrorMessage = "Comments must be at least 20 characters.")]
        public string Comments {get;set;}
    }
}