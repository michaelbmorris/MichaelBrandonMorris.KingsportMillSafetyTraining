using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class SlideManager.
    /// </summary>
    /// <seealso cref="EntityManager{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for SlideManager
    public class SlideManager : EntityManager<Slide, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="SlideManager" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public SlideManager(SlideStore store)
            : base(store)
        {
            Store = store;
        }

        /// <summary>
        ///     Gets the slides.
        /// </summary>
        /// <value>The slides.</value>
        /// TODO Edit XML Comment Template for Slides
        public IQueryable<Slide> Slides => Store.Entities;

        /// <summary>
        ///     Gets or sets the store.
        /// </summary>
        /// <value>The store.</value>
        /// TODO Edit XML Comment Template for Store
        protected new SlideStore Store
        {
            get;
        }

        /// <summary>
        ///     Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns>SlideManager.</returns>
        /// TODO Edit XML Comment Template for Create
        public static SlideManager Create(
            IdentityFactoryOptions<SlideManager> options,
            IOwinContext context)
        {
            var manager = new SlideManager(
                new SlideStore(
                    context.Get<KingsportMillSafetyTrainingDbContext>()));

            return manager;
        }
    }
}