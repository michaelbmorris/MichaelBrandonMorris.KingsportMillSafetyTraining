using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    /// Class CategoryManager.
    /// </summary>
    /// <seealso cref="KingsportMillSafetyTraining.EntityManager{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for CategoryManager
    public class CategoryManager : EntityManager<Category, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryManager"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public CategoryManager(CategoryStore store) 
            : base(store)
        {
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <value>The categories.</value>
        /// TODO Edit XML Comment Template for Categories
        public virtual IQueryable<Category> Categories => Store.Entities;

        /// <summary>
        /// Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns>CompanyManager.</returns>
        /// TODO Edit XML Comment Template for Create
        public static CompanyManager Create(
            IdentityFactoryOptions<CategoryManager> options,
            IOwinContext context)
        {
            var manager = new CompanyManager(
                new CompanyStore(
                    context.Get<KingsportMillSafetyTrainingDbContext>()));

            return manager;
        }

        public Task RemoveGroups()
        {
            return ((CategoryStore)Store).RemoveGroups();
        }

        public Task RemoveGroups(int id)
        {
            return ((CategoryStore) Store).RemoveGroups(id);
        }
    }
}