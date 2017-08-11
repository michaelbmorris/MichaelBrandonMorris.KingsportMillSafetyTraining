using System;
using System.Linq;
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
    public class CompanyManager : EntityManager<Company, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="CompanyManager" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public CompanyManager(CompanyStore store)
            : base(store)
        {
        }

        /// <summary>
        ///     Gets the companies.
        /// </summary>
        /// <value>The companies.</value>
        /// TODO Edit XML Comment Template for Companies
        public virtual IQueryable<Company> Companies => Store.Entities;

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
    }
}