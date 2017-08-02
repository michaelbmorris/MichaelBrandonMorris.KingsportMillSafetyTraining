using System.Linq;
using System.Security.Principal;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Views
{
    /// <summary>
    ///     Class Extensions.
    /// </summary>
    /// TODO Edit XML Comment Template for Extensions
    public static class Extensions
    {
        /// <summary>
        ///     Determines whether [is own training result] [the
        ///     specified identifier].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     <c>true</c> if [is own training result] [the specified
        ///     identifier]; otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for IsOwnTrainingResult
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