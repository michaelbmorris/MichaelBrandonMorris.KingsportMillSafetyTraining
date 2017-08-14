using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class EntityStore.
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <seealso cref="IDisposable" />
    /// TODO Edit XML Comment Template for EntityStore`2
    public class EntityStore<TEntity, TKey> : IDisposable
        where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="EntityStore{TEntity, TKey}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public EntityStore(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        ///     Gets the context.
        /// </summary>
        /// <value>The context.</value>
        /// TODO Edit XML Comment Template for Context
        public DbContext Context
        {
            get;
        }

        /// <summary>
        ///     Gets the entities.
        /// </summary>
        /// <value>The entities.</value>
        /// TODO Edit XML Comment Template for Entities
        public IQueryable<TEntity> Entities => Context.Set<TEntity>();

        /// <summary>
        ///     Performs application-defined tasks associated with
        ///     freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// TODO Edit XML Comment Template for Dispose
        public void Dispose()
        {
            Context?.Dispose();
        }

        /// <summary>
        ///     Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for CreateAsync
        public virtual Task CreateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for DeleteAsync
        public virtual Task DeleteAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Task&lt;TEntity&gt;.</returns>
        /// TODO Edit XML Comment Template for FindByIdAsync
        public virtual Task<TEntity> FindByIdAsync(TKey key)
        {
            return Entities.FirstOrDefaultAsync(
                entity => entity.Id.Equals(key));
        }

        /// <summary>
        ///     Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for UpdateAsync
        public virtual Task UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }
    }
}