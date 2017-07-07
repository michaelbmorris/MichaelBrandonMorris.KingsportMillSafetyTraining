using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Account
{
    /// <summary>
    ///     Class ResetPasswordViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for ResetPasswordViewModel
    public class ResetPasswordViewModel
    {
        /// <summary>
        ///     Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        /// TODO Edit XML Comment Template for Code
        public string Code
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
        [Compare(
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
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        /// TODO Edit XML Comment Template for Password
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