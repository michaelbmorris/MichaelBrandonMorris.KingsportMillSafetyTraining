using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Validation;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Account
{
    /// <summary>
    ///     Class RegisterViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for RegisterViewModel
    public class RegisterViewModel
    {
        /// <summary>
        ///     The required error message
        /// </summary>
        /// TODO Edit XML Comment Template for RequiredErrorMessage
        private const string RequiredErrorMessage = "This field is required.";

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="RegisterViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public RegisterViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="RegisterViewModel" /> class.
        /// </summary>
        /// <param name="companies">The companies.</param>
        /// TODO Edit XML Comment Template for #ctor
        public RegisterViewModel(IList<Company> companies)
        {
            Companies = companies;
        }

        /// <summary>
        ///     Gets the companies.
        /// </summary>
        /// <value>The companies.</value>
        /// TODO Edit XML Comment Template for Companies
        public IList<Company> Companies
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the company select list.
        /// </summary>
        /// <value>The company select list.</value>
        /// TODO Edit XML Comment Template for CompanySelectList
        public IList<SelectListItem> CompanySelectList => Companies.Select(
            company => new SelectListItem
            {
                Value = company.Id.ToString(),
                Text = company.Name
            });

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
        ///     Gets or sets the name of the other company.
        /// </summary>
        /// <value>The name of the other company.</value>
        /// TODO Edit XML Comment Template for OtherCompanyName
        [DisplayName("Other Company")]
        [OtherCompanyValid]
        public string OtherCompanyName
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
            MinimumLength = 8)]
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