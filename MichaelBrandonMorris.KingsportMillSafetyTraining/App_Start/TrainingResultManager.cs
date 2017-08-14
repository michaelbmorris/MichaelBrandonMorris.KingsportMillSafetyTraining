using System.Linq;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    public class TrainingResultManager : EntityManager<TrainingResult, int>
    {
        public TrainingResultManager(TrainingResultStore store)
            : base(store)
        {
        }

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