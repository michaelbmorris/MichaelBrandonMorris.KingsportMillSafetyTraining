using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class GroupManager.
    /// </summary>
    /// <seealso
    ///     cref="KingsportMillSafetyTraining.EntityManager{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for GroupManager
    public class GroupManager : EntityManager<Group, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GroupManager" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public GroupManager(EntityStore<Group, int> store)
            : base(store)
        {
        }

        /// <summary>
        ///     Gets the groups.
        /// </summary>
        /// <value>The groups.</value>
        /// TODO Edit XML Comment Template for Groups
        public IQueryable<Group> Groups => Store.Entities;

        /// <summary>
        ///     Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns>GroupManager.</returns>
        public static GroupManager Create(
            IdentityFactoryOptions<GroupManager> options,
            IOwinContext context)
        {
            var manager = new GroupManager(
                new GroupStore(
                    context.Get<KingsportMillSafetyTrainingDbContext>()));

            return manager;
        }

        /// <summary>
        ///     Pairs the specified group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="category">The category.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for Pair
        public Task Pair(Group group, Category category)
        {
            return ((GroupStore) Store).Pair(group, category);
        }

        /// <summary>
        ///     Removes the categories.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for RemoveCategories
        public Task RemoveCategories(int id)
        {
            return ((GroupStore) Store).RemoveCategories(id);
        }
    }
}