using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Account
{
    /// <summary>
    ///     Class VerifyCodeViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for VerifyCodeViewModel
    public class VerifyCodeViewModel
    {
        /// <summary>
        ///     Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        /// TODO Edit XML Comment Template for Code
        [Required]
        [Display(Name = "Code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        /// TODO Edit XML Comment Template for Provider
        [Required]
        public string Provider
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [remember
        ///     browser].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [remember browser]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for RememberBrowser
        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser
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
        public bool RememberMe
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the return URL.
        /// </summary>
        /// <value>The return URL.</value>
        /// TODO Edit XML Comment Template for ReturnUrl
        public string ReturnUrl
        {
            get;
            set;
        }
    }
}