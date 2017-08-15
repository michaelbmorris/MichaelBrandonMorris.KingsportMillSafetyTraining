using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class UserStore.
    /// </summary>
    /// <seealso
    ///     cref="Microsoft.AspNet.Identity.EntityFramework.UserStore{T}" />
    /// TODO Edit XML Comment Template for UserStore
    public class UserStore
        : UserStore<User, Role, string, IdentityUserLogin, IdentityUserRole,
            IdentityUserClaim>
    {
        /// <summary>
        ///     Constructor which takes a db context and wires up the
        ///     stores with default instances using the context
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public UserStore(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        ///     Adds the training result.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for AddTrainingResult
        public async Task AddTrainingResult(string id)
        {
            var user = await FindByIdAsync(id);

            if (user.TrainingResults.LastOrDefault() != null
                && user.TrainingResults.Last().CompletionDateTime == null)
            {
                return;
            }

            user.TrainingResults.Add(
                new TrainingResult
                {
                    Group = user.Group
                });

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Gets the group.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Group&gt;.</returns>
        /// TODO Edit XML Comment Template for GetGroup
        public async Task<Group> GetGroup(string id)
        {
            var user = await FindByIdAsync(id);
            return user.Group;
        }

        /// <summary>
        ///     Sets the company.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="company">The company.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for SetCompany
        public async Task SetCompany(string id, Company company)
        {
            var user = await FindByIdAsync(id);
            user.Company = company;
            await Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Sets the group.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="group">The group.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for SetGroup
        public async Task SetGroup(string userId, Group group)
        {
            var user = await Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.Group = group;
            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Sets the latest log on date time.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for SetLatestLogOnDateTime
        public async Task SetLatestLogOnDateTime(string userName)
        {
            var user = await FindByNameAsync(userName);
            user.LastLogOnDateTime = DateTime.Now;
            await Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Sets the latest quiz start date time.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for SetLatestQuizStartDateTime
        public async Task SetLatestQuizStartDateTime(string id)
        {
            var user = await FindByIdAsync(id);
            user.LatestQuizStartDateTime = DateTime.Now;
            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Sets the latest training start date time.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for SetLatestTrainingStartDateTime
        public async Task SetLatestTrainingStartDateTime(string id)
        {
            var user = await FindByIdAsync(id);
            user.LatestTrainingStartDateTime = DateTime.Now;
            await Context.SaveChangesAsync();
        }
    }
}