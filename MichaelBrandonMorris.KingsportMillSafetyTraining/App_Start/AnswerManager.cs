using System.Linq;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class AnswerManager.
    /// </summary>
    /// <seealso cref="EntityManager{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for AnswerManager
    public class AnswerManager : EntityManager<Answer, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AnswerManager" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public AnswerManager(AnswerStore store)
            : base(store)
        {
            Store = store;
        }

        /// <summary>
        ///     Gets the answers.
        /// </summary>
        /// <value>The answers.</value>
        /// TODO Edit XML Comment Template for Answers
        public IQueryable<Answer> Answers => Store.Entities;

        /// <summary>
        ///     Gets or sets the store.
        /// </summary>
        /// <value>The store.</value>
        /// TODO Edit XML Comment Template for Store
        private new AnswerStore Store
        {
            get;
        }

        /// <summary>
        ///     Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns>AnswerManager.</returns>
        /// TODO Edit XML Comment Template for Create
        public static AnswerManager Create(
            IdentityFactoryOptions<AnswerManager> options,
            IOwinContext context)
        {
            var manager = new AnswerManager(
                new AnswerStore(
                    context.Get<KingsportMillSafetyTrainingDbContext>()));

            return manager;
        }
    }
}