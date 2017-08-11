using System;
using System.Data.Entity;
using System.Linq;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class CompanyStore.
    /// </summary>
    /// <seealso cref="IDisposable" />
    /// TODO Edit XML Comment Template for CompanyStore
    public class CompanyStore : EntityStore<Company, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="CompanyStore" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public CompanyStore(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        ///     Gets the companies.
        /// </summary>
        /// <value>The companies.</value>
        /// TODO Edit XML Comment Template for Companies
        public IQueryable<Company> Companies => Entities;
    }
}