using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class CategoryStore.
    /// </summary>
    /// <seealso
    ///     cref="KingsportMillSafetyTraining.EntityStore{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for CategoryStore
    public class CategoryStore : EntityStore<Category, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="CategoryStore" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public CategoryStore(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        ///     Gets the categories.
        /// </summary>
        /// <value>The categories.</value>
        /// TODO Edit XML Comment Template for Categories
        public IQueryable<Category> Categories => Entities;

        /// <summary>
        ///     Pairs the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="group">The group.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for Pair
        public Task Pair(Category category, Group group)
        {
            category.Groups.Add(group);
            return Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Removes the groups.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for RemoveGroups
        public Task RemoveGroups(int id)
        {
            var category = Categories.Include(c => c.Groups)
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new KeyNotFoundException();
            }

            foreach (var group in category.Groups.ToList())
            {
                category.Groups.Remove(group);
            }

            return Context.SaveChangesAsync();
        }
    }
}