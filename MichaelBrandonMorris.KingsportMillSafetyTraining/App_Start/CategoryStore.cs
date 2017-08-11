using System.Data.Entity;
using System.Linq;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using System.Threading.Tasks;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    /// Class CategoryStore.
    /// </summary>
    /// <seealso cref="KingsportMillSafetyTraining.EntityStore{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for CategoryStore
    public class CategoryStore : EntityStore<Category, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryStore"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public CategoryStore(DbContext context)
            :base(context)
        {
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <value>The categories.</value>
        /// TODO Edit XML Comment Template for Categories
        public IQueryable<Category> Categories => Entities;

        public Task RemoveGroups()
        {
            foreach(var category in Categories.Include(c => c.Groups))
            {
                foreach(var group in category.Groups.ToList())
                {
                    category.Groups.Remove(group);
                }
            }

            return Context.SaveChangesAsync();
        }

        public Task RemoveGroups(int id)
        {
            var category = Categories.Include(c => c.Groups).FirstOrDefault(c => c.Id == id);

            foreach (var group in category.Groups.ToList())
            {
                category.Groups.Remove(group);
            }

            return Context.SaveChangesAsync();
        }
    }
}