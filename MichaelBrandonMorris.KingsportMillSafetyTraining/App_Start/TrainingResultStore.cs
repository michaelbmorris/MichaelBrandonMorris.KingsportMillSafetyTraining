﻿using System.Data.Entity;
using System.Linq;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class TrainingResultStore.
    /// </summary>
    /// <seealso cref="EntityStore{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for TrainingResultStore
    public class TrainingResultStore : EntityStore<TrainingResult, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="TrainingResultStore" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public TrainingResultStore(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        ///     Gets the training results.
        /// </summary>
        /// <value>The training results.</value>
        /// TODO Edit XML Comment Template for TrainingResults
        public IQueryable<TrainingResult> TrainingResults => Entities;
    }
}