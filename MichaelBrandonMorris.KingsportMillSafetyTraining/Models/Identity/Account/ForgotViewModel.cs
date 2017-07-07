using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Account
{
    /// <summary>
    ///     Class ForgotViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for ForgotViewModel
    public class ForgotViewModel
    {
        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        /// TODO Edit XML Comment Template for Email
        [Required]
        [Display(Name = "Email")]
        public string Email
        {
            get;
            set;
        }
    }
}