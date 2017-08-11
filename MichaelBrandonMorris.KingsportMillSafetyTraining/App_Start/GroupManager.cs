using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Linq;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    /// Class GroupManager.
    /// </summary>
    /// <seealso cref="KingsportMillSafetyTraining.EntityManager{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for GroupManager
    public class GroupManager : EntityManager<Group, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupManager"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public GroupManager(EntityStore<Group, int> store) : base(store)
        {
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <value>The groups.</value>
        /// TODO Edit XML Comment Template for Groups
        public IQueryable<Group> Groups => Store.Entities;

        /// <summary>
        /// Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns>GroupManager.</returns>
        /// TODO Edit XML Comment Template for Create
        public static GroupManager Create(
            IdentityFactoryOptions<GroupManager> options,
            IOwinContext context)
        {
            var manager = new GroupManager(
                new GroupStore(
                    context.Get<KingsportMillSafetyTrainingDbContext>()));

            return manager;
        }
    }
}