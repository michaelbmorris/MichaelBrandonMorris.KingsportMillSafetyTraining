using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "Company Name")]
        [Required]
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

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required]
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

        [Display(Name = "Phone Number")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number.")]
        public string PhoneNumber
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