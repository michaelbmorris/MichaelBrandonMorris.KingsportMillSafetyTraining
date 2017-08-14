using System.Data.Entity;
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
    }
}