using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.Manage
{
    public class IndexViewModel
    {
        public bool BrowserRemembered
        {
            get;
            set;
        }

        public bool HasPassword
        {
            get;
            set;
        }

        public IList<UserLoginInfo> Logins
        {
            get;
            set;
        }

        public string PhoneNumber
        {
            get;
            set;
        }

        public bool TwoFactor
        {
            get;
            set;
        }

        public string RoleTitle
        {
            get;
            set;
        }
    }
}