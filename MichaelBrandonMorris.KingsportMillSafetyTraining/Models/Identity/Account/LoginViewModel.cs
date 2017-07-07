using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Account
{
    /// <summary>
    ///     Class LoginViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for LoginViewModel
    public class LoginViewModel
    {
        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        /// TODO Edit XML Comment Template for Email
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
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
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [remember me]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for RememberMe
        [Display(Name = "Remember me?")]
        public bool RememberMe
        {
            get;
            set;
        }
    }
}