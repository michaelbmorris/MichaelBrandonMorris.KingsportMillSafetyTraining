using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class ChangePasswordViewModel
    {
        public string UserId
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }
    }
}