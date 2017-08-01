using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Views
{
    public static class Extensions
    {
        public static bool IsOwnTrainingResult(this IPrincipal user, int id)
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                return db.GetUser(user.GetId())
                    .TrainingResults
                    .Any(trainingResult => trainingResult.Id == id);
            }
        }
    }
}