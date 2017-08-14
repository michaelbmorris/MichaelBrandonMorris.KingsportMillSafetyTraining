﻿using System.Linq;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class TrainingResultManager.
    /// </summary>
    /// <seealso cref="EntityManager{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for TrainingResultManager
    public class TrainingResultManager : EntityManager<TrainingResult, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="TrainingResultManager" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public TrainingResultManager(TrainingResultStore store)
            : base(store)
        {
        }

        /// <summary>
        ///     Gets the training results.
        /// </summary>
        /// <value>The training results.</value>
        /// TODO Edit XML Comment Template for TrainingResults
        public virtual IQueryable<TrainingResult> TrainingResults => Store
            .Entities;

        /// <summary>
        ///     Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns>CompanyManager.</returns>
        /// TODO Edit XML Comment Template for Create
        public static TrainingResultManager Create(
            IdentityFactoryOptions<TrainingResultManager> options,
            IOwinContext context)
        {
            var manager = new TrainingResultManager(
                new TrainingResultStore(
                    context.Get<KingsportMillSafetyTrainingDbContext>()));

            return manager;
        }
    }
}