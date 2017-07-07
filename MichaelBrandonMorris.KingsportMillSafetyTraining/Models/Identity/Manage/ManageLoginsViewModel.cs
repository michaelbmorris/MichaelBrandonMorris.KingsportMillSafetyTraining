using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    Manage
{
    /// <summary>
    ///     Class ManageLoginsViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for ManageLoginsViewModel
    public class ManageLoginsViewModel
    {
        /// <summary>
        ///     Gets or sets the current logins.
        /// </summary>
        /// <value>The current logins.</value>
        /// TODO Edit XML Comment Template for CurrentLogins
        public IList<UserLoginInfo> CurrentLogins
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the other logins.
        /// </summary>
        /// <value>The other logins.</value>
        /// TODO Edit XML Comment Template for OtherLogins
        public IList<AuthenticationDescription> OtherLogins
        {
            get;
            set;
        }
    }
}