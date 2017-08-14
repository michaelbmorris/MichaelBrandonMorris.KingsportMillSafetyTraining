using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.Account
{
    /// <summary>
    ///     Preview model for confirming external login.
    /// </summary>
    public class ExternalLoginConfirmationViewModel
    {
        /// <summary>Gets or sets the email address.</summary>
        /// <value>The email address.</value>
        [Required]
        [Display(Name = "Email")]
        public string Email
        {
            get;
            set;
        }
    }
}