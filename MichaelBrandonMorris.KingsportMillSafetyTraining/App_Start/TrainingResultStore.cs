using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        ///     Adds the quiz result.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="questionsCorrect">The questions correct.</param>
        /// <param name="totalQuestions">The total questions.</param>
        /// <returns>Task.</returns>
        /// <exception cref="InvalidOperationException">
        ///     User quiz start
        ///     time not recorded.
        /// </exception>
        /// TODO Edit XML Comment Template for AddQuizResult
        public async Task AddQuizResult(
            int id,
            int questionsCorrect,
            int totalQuestions)
        {
            var trainingResult = await TrainingResults.Include(t => t.User)
                .Include(t => t.QuizResults)
                .FirstOrDefaultAsync(t => t.Id == id);

            var user = trainingResult.User;
            var latestQuizStartDateTime = user.LatestQuizStartDateTime;

            if (latestQuizStartDateTime == null)
            {
                throw new InvalidOperationException(
                    "User quiz start time not recorded.");
            }

            trainingResult.QuizResults.Add(
                new QuizResult
                {
                    AttemptNumber = trainingResult.QuizResults.Count + 1,
                    QuestionsCorrect = questionsCorrect,
                    TimeToComplete =
                        DateTime.Now - latestQuizStartDateTime.Value,
                    TotalQuestions = totalQuestions
                });
        }
    }
}