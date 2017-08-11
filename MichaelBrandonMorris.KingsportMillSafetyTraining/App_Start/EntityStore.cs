using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    public class EntityStore<TEntity, TKey> : IDisposable where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public EntityStore(DbContext context)
        {
            Context = context;
        }

        public DbContext Context
        {
            get;
        }

        public IQueryable<TEntity> Entities => Context.Set<TEntity>();

        public virtual Task CreateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return Context.SaveChangesAsync();
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return Context.SaveChangesAsync();
        }

        public virtual Task<TEntity> FindByIdAsync(TKey key)
        {
            return Entities.FirstOrDefaultAsync(entity => entity.Id.Equals(key));
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}