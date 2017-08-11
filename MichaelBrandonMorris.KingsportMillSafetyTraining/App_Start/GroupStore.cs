using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using System.Linq;
using System.Data.Entity;

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
    }
}