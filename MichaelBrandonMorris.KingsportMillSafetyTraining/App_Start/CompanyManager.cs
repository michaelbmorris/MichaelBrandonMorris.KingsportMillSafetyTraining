using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class CompanyManager.
    /// </summary>
    /// <seealso cref="IDisposable" />
    /// TODO Edit XML Comment Template for CompanyManager
    public class CompanyManager : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="CompanyManager" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public CompanyManager(CompanyStore store)
        {
            Store = store;
        }

        /// <summary>
        ///     Gets the companies.
        /// </summary>
        /// <value>The companies.</value>
        /// TODO Edit XML Comment Template for Companies
        public virtual IQueryable<Company> Companies => Store.Companies;

        /// <summary>
        ///     Gets or sets the store.
        /// </summary>
        /// <value>The store.</value>
        /// TODO Edit XML Comment Template for Store
        protected internal CompanyStore Store
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
        ///     Creates the specified context.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context">The context.</param>
        /// <returns>CompanyManager.</returns>
        /// TODO Edit XML Comment Template for Create
        public static CompanyManager Create(
            IdentityFactoryOptions<CompanyManager> options,
            IOwinContext context)
        {
            var manager = new CompanyManager(
                new CompanyStore(
                    context.Get<KingsportMillSafetyTrainingDbContext>()));

            return manager;
        }

        /// <summary>
        ///     Creates the asynchronous.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for CreateAsync
        public virtual Task CreateAsync(Company company)
        {
            return Store.CreateAsync(company);
        }

        /// <summary>
        ///     Deletes the asynchronous.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for DeleteAsync
        public virtual Task DeleteAsync(Company company)
        {
            return Store.DeleteAsync(company);
        }

        /// <summary>
        ///     Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Company&gt;.</returns>
        /// TODO Edit XML Comment Template for FindByIdAsync
        public virtual Task<Company> FindByIdAsync(int id)
        {
            return Store.FindByIdAsync(id);
        }

        /// <summary>
        ///     Updates the asynchronous.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for UpdateAsync
        public virtual Task UpdateAsync(Company company)
        {
            return Store.UpdateAsync(company);
        }
    }

    /// <summary>
    ///     Class CompanyStore.
    /// </summary>
    /// <seealso cref="IDisposable" />
    /// TODO Edit XML Comment Template for CompanyStore
    public class CompanyStore : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="CompanyStore" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public CompanyStore(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        ///     Gets the companies.
        /// </summary>
        /// <value>The companies.</value>
        /// TODO Edit XML Comment Template for Companies
        public IQueryable<Company> Companies => Context.Set<Company>();

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
        /// <param name="company">The company.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for CreateAsync
        public virtual Task CreateAsync(Company company)
        {
            Context.Set<Company>().Add(company);
            return Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Deletes the asynchronous.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for DeleteAsync
        public virtual Task DeleteAsync(Company company)
        {
            Context.Entry(company).State = EntityState.Deleted;
            return Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for FindByIdAsync
        public virtual Task<Company> FindByIdAsync(int id)
        {
            return Companies.FirstOrDefaultAsync(company => company.Id == id);
        }

        /// <summary>
        ///     Updates the asynchronous.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for UpdateAsync
        public virtual Task UpdateAsync(Company company)
        {
            Context.Entry(company).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }
    }
}