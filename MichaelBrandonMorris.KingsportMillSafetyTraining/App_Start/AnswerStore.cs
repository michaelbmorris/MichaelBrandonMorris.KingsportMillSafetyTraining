using System.Data.Entity;
using System.Linq;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class AnswerStore.
    /// </summary>
    /// <seealso cref="EntityStore{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for AnswerStore
    public class AnswerStore : EntityStore<Answer, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AnswerStore" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public AnswerStore(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        ///     Gets the answers.
        /// </summary>
        /// <value>The answers.</value>
        /// TODO Edit XML Comment Template for Answers
        public IQueryable<Answer> Answers => Entities;

        /// <summary>
        ///     Updates the index of the current.
        /// </summary>
        /// TODO Edit XML Comment Template for UpdateCurrentIndex
        public void UpdateCurrentIndex()
        {
            Answer.CurrentIndex = !Answers.Any()
                ? 0
                : Answers.Max(answer => answer.Index);
        }
    }
}