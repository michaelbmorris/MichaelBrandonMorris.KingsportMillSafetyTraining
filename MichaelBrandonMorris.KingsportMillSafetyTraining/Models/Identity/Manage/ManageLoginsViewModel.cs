using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.Manage
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins
        {
            get;
            set;
        }

        public IList<AuthenticationDescription> OtherLogins
        {
            get;
            set;
        }
    }
}