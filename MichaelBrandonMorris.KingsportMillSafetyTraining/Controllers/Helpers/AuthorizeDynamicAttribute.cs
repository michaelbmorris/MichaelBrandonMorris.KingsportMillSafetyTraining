using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizeDynamicAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase context)
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                throw new NotImplementedException();
            }
        }
    }
}