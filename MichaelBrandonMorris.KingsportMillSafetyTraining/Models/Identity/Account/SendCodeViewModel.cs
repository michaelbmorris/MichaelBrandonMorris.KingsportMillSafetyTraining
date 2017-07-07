using System.Collections.Generic;
using System.Web.Mvc;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Account
{
    /// <summary>
    ///     Class SendCodeViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for SendCodeViewModel
    public class SendCodeViewModel
    {
        /// <summary>
        ///     Gets or sets the providers.
        /// </summary>
        /// <value>The providers.</value>
        /// TODO Edit XML Comment Template for Providers
        public ICollection<SelectListItem> Providers
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

        /// <summary>
        ///     Gets or sets the selected provider.
        /// </summary>
        /// <value>The selected provider.</value>
        /// TODO Edit XML Comment Template for SelectedProvider
        public string SelectedProvider
        {
            get;
            set;
        }
    }
}