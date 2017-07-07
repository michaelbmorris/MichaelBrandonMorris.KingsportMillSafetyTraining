using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Manage
{
    /// <summary>
    ///     Class IndexViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for IndexViewModel
    public class IndexViewModel
    {
        /// <summary>
        ///     Gets or sets a value indicating whether [browser
        ///     remembered].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [browser remembered]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for BrowserRemembered
        public bool BrowserRemembered
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance
        ///     has password.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has password;
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for HasPassword
        public bool HasPassword
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the logins.
        /// </summary>
        /// <value>The logins.</value>
        /// TODO Edit XML Comment Template for Logins
        public IList<UserLoginInfo> Logins
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        /// TODO Edit XML Comment Template for PhoneNumber
        public string PhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [two factor].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [two factor]; otherwise, <c>false</c>
        ///     .
        /// </value>
        /// TODO Edit XML Comment Template for TwoFactor
        public bool TwoFactor
        {
            get;
            set;
        }
    }
}