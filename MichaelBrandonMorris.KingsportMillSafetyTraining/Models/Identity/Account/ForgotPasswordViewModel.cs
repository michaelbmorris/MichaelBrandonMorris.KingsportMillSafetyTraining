using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Account
{
    /// <summary>
    ///     Class ForgotPasswordViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for ForgotPasswordViewModel
    public class ForgotPasswordViewModel
    {
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
    }
}