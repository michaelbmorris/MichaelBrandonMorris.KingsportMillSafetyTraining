using System.Linq;
using System.Threading.Tasks;
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
            Store = store;
        }

        /// <summary>
        /// Gets or sets the store.
        /// </summary>
        /// <value>The store.</value>
        /// TODO Edit XML Comment Template for Store
        private new TrainingResultStore Store
        {
            get;
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

        /// <summary>
        /// Adds the quiz result.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="questionsCorrect">The questions correct.</param>
        /// <param name="totalQuestions">The total questions.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for AddQuizResult
        public Task AddQuizResult(
            int id,
            int questionsCorrect,
            int totalQuestions)
        {
            return Store.AddQuizResult(id, questionsCorrect, totalQuestions);
        }
    }
}