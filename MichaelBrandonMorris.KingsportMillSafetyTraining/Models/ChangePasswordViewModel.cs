using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class ChangePasswordViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for ChangePasswordViewModel
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare(
            "Password",
            ErrorMessage =
                "The new password and confirmation password do not match.")]
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
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the new password.
        /// </summary>
        /// <value>The new password.</value>
        /// TODO Edit XML Comment Template for NewPassword
        [Required]
        [StringLength(
            100,
            ErrorMessage = "The {0} must be at least {2} characters long.",
            MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        /// TODO Edit XML Comment Template for UserId
        public string UserId
        {
            get;
            set;
        }
    }
}