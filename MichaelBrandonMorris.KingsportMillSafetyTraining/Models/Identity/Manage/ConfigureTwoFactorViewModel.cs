using System.Collections.Generic;
using System.Web.Mvc;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Manage
{
    /// <summary>
    ///     Class ConfigureTwoFactorViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for ConfigureTwoFactorViewModel
    public class ConfigureTwoFactorViewModel
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