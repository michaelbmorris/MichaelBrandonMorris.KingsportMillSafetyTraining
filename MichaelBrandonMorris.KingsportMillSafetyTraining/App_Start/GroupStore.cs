using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class GroupStore.
    /// </summary>
    /// <seealso
    ///     cref="KingsportMillSafetyTraining.EntityStore{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for GroupStore
    public class GroupStore : EntityStore<Group, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GroupStore" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public GroupStore(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        ///     Gets the groups.
        /// </summary>
        /// <value>The groups.</value>
        /// TODO Edit XML Comment Template for Groups
        public IQueryable<Group> Groups => Entities;

        /// <summary>
        ///     Pairs the specified group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="category">The category.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for Pair
        public Task Pair(Group group, Category category)
        {
            group.Categories.Add(category);
            return Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Removes the categories.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// TODO Edit XML Comment Template for RemoveCategories
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

        /// <summary>
        ///     Updates the index of the current.
        /// </summary>
        /// TODO Edit XML Comment Template for UpdateCurrentIndex
        public void UpdateCurrentIndex()
        {
            Group.CurrentIndex = !Groups.Any()
                ? 0
                : Groups.Max(answer => answer.Index);
        }
    }
}