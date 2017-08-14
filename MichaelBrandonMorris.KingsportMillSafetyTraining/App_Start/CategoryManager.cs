using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class CategoryManager.
    /// </summary>
    /// <seealso
    ///     cref="KingsportMillSafetyTraining.EntityManager{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for CategoryManager
    public class CategoryManager : EntityManager<Category, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="CategoryManager" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public CategoryManager(CategoryStore store)
            : base(store)
        {
            Store = store;
        }

        private new CategoryStore Store
        {
            get;
        }

        /// <summary>
        ///     Gets the categories.
        /// </summary>
        /// <value>The categories.</value>
        /// TODO Edit XML Comment Template for Categories
        public virtual IQueryable<Category> Categories => Store.Entities;

        /// <summary>
        ///     Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns>CompanyManager.</returns>
        /// TODO Edit XML Comment Template for Create
        public static CategoryManager Create(
            IdentityFactoryOptions<CategoryManager> options,
            IOwinContext context)
        {
            var manager = new CategoryManager(
                new CategoryStore(
                    context.Get<KingsportMillSafetyTrainingDbContext>()));

            return manager;
        }

        /// <summary>
        ///     Pairs the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="group">The group.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for Pair
        public Task Pair(Category category, Group group)
        {
            return Store.Pair(category, group);
        }

        /// <summary>
        ///     Removes the groups.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for RemoveGroups
        public Task RemoveGroups(int id)
        {
            return Store.RemoveGroups(id);
        }
    }
}