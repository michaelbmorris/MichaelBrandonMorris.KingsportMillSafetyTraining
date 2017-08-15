using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
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
        ///     Determines whether the specified identifier is
        ///     employee.
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// TODO Edit XML Comment Template for IsEmployee
        public static async Task<bool> IsEmployee(
            this IPrincipal currentUser,
            string id)
        {
            using (var context = new KingsportMillSafetyTrainingDbContext())
            using (var store = new UserStore(context))
            using (var manager = new UserManager(store))
            {
                var userId = currentUser.GetId();

                var user = await manager.Users.Include(u => u.Company.Employees)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                return user.Company.Employees.Any(e => e.Id == id);
            }
        }

        /// <summary>
        ///     Determines whether [is employee training result] [the
        ///     specified identifier].
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// TODO Edit XML Comment Template for IsEmployeeTrainingResult
        public static async Task<bool> IsEmployeeTrainingResult(
            this IPrincipal currentUser,
            int id)
        {
            using (var context = new KingsportMillSafetyTrainingDbContext())
            using (var userStore = new UserStore(context))
            using (var userManager = new UserManager(userStore))
            {
                var userId = currentUser.GetId();

                var user = await userManager.Users
                    .Include(
                        u => u.Company.Employees.Select(e => e.TrainingResults))
                    .FirstOrDefaultAsync(u => u.Id == userId);

                var employees = user.Company.Employees;

                var employeeTrainingResults =
                    employees.SelectMany(e => e.TrainingResults);

                return employeeTrainingResults.Any(t => t.Id == id);
            }
        }

        /// <summary>
        ///     Determines whether [is own training result] [the
        ///     specified identifier].
        /// </summary>
        /// <param name="currentUser">The user.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     <c>true</c> if [is own training result] [the specified
        ///     identifier]; otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for IsOwnTrainingResult
        public static async Task<bool> IsOwnTrainingResult(
            this IPrincipal currentUser,
            int id)
        {
            using (var context = new KingsportMillSafetyTrainingDbContext())
            using (var store = new UserStore(context))
            using (var manager = new UserManager(store))
            {
                var userId = currentUser.GetId();

                var user = await manager.Users.Include(u => u.TrainingResults)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                return user.TrainingResults.Any(t => t.Id == id);
            }
        }
    }
}