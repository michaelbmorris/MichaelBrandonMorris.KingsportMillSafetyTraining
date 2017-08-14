using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    /// Class GroupStore.
    /// </summary>
    /// <seealso cref="KingsportMillSafetyTraining.EntityStore{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for GroupStore
    public class GroupStore : EntityStore<Group, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupStore"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public GroupStore(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <value>The groups.</value>
        /// TODO Edit XML Comment Template for Groups
        public IQueryable<Group> Groups => Entities;

        public Task Pair(Group group, Category category)
        {
            group.Categories.Add(category);
            return Context.SaveChangesAsync();
        }

        public Task RemoveCategories(int id)
        {
            var group = Groups.Include(g => g.Categories)
                .FirstOrDefault(g => g.Id == id);

            if (group == null)
            {
                throw new KeyNotFoundException();
            }

            foreach (var category in group.Categories.ToList())
            {
                group.Categories.Remove(category);
            }

            return Context.SaveChangesAsync();
        }
    }
}