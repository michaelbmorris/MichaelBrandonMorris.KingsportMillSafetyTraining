using System;
using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.Account
{
    public class RegisterViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        [Required]
        public DateTime BirthDate
        {
            get;
            set;
        }

        [Display(Name = "Company Name")]
        public string CompanyName
        {
            get;
            set;
        }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(
            "Password",
            ErrorMessage =
                "The password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email
        {
            get;
            set;
        }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName
        {
            get;
            set;
        }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName
        {
            get;
            set;
        }

        [Display(Name = "Middle Name")]
        public string MiddleName
        {
            get;
            set;
        }

        [Required]
        [StringLength(
            100,
            ErrorMessage = "The {0} must be at least {2} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            get;
            set;
        }
    }
}