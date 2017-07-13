using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Account
{
    /// <summary>
    ///     Class RegisterViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for RegisterViewModel
    public class RegisterViewModel
    {
        private const string RequiredErrorMessage = "This field is required.";

        /// <summary>
        ///     Gets or sets the company identifier.
        /// </summary>
        /// <value>The identifier of the company.</value>
        /// TODO Edit XML Comment Template for CompanyName
        [Display(Name = "Company")]
        [Range(1, int.MaxValue, ErrorMessage = "This field is required.")]
        public int CompanyId
        {
            get;
            set;
        }

        public IList<Company> Companies
        {
            get;
            set;
        }

        [DisplayName("Other Company")]
        public string OtherCompanyName
        {
            get;
            set;
        }

        public SelectListItem DefaultCompanyItem => new SelectListItem
        {
            Value = 0.ToString(),
            Text = "Select a company..."
        };

        public IList<SelectListItem> CompanyItems => DefaultCompanyItem.Append(Companies.Select(
            company => new SelectListItem
            {
                Value = company.Id.ToString(),
                Text = company.Name
            }));

        /// <summary>
        ///     Gets or sets the confirm password.
        /// </summary>
        /// <value>The confirm password.</value>
        /// TODO Edit XML Comment Template for ConfirmPassword
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare(
            "Password",
            ErrorMessage =
                "The password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        /// TODO Edit XML Comment Template for Email
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        /// TODO Edit XML Comment Template for FirstName
        [Display(Name = "First Name")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        /// TODO Edit XML Comment Template for LastName
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        /// TODO Edit XML Comment Template for MiddleName
        [Display(Name = "Middle Name")]
        public string MiddleName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        /// TODO Edit XML Comment Template for Password
        [Required(ErrorMessage = RequiredErrorMessage)]
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

        /// <summary>
        ///     Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        /// TODO Edit XML Comment Template for PhoneNumber
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(
            @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
            ErrorMessage = "Not a valid phone number.")]
        public string PhoneNumber
        {
            get;
            set;
        }
    }
}