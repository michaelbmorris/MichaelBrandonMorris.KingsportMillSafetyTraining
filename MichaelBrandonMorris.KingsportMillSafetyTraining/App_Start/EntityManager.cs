using System;
using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class EntityManager.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="IDisposable" />
    /// TODO Edit XML Comment Template for EntityManager`2
    public class EntityManager<TEntity, TKey> : IDisposable
        where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="EntityManager{TEntity, TKey}" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public EntityManager(EntityStore<TEntity, TKey> store)
        {
            Store = store;
        }

        /// <summary>
        ///     Gets the entities.
        /// </summary>
        /// <value>The entities.</value>
        /// TODO Edit XML Comment Template for Entities
        public IQueryable<TEntity> Entities => Store.Entities;

        /// <summary>
        ///     Gets or sets the store.
        /// </summary>
        /// <value>The store.</value>
        /// TODO Edit XML Comment Template for Store
        protected internal EntityStore<TEntity, TKey> Store
        {
            get;
            set;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with
        ///     freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// TODO Edit XML Comment Template for Dispose
        public void Dispose()
        {
            Store?.Dispose();
        }

        /// <summary>
        ///     Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for CreateAsync
        public virtual Task CreateAsync(TEntity entity)
        {
            return Store.CreateAsync(entity);
        }

        /// <summary>
        ///     Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for DeleteAsync
        public virtual Task DeleteAsync(TEntity entity)
        {
            return Store.DeleteAsync(entity);
        }

        /// <summary>
        ///     Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Task&lt;TEntity&gt;.</returns>
        /// TODO Edit XML Comment Template for FindByIdAsync
        public virtual Task<TEntity> FindByIdAsync(TKey key)
        {
            return Store.FindByIdAsync(key);
        }

        /// <summary>
        ///     Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for UpdateAsync
        public virtual Task UpdateAsync(TEntity entity)
        {
            return Store.UpdateAsync(entity);
        }
    }
}