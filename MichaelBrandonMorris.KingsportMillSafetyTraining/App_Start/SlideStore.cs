using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class SlideStore.
    /// </summary>
    /// <seealso cref="EntityStore{TEntity, TKey}" />
    /// TODO Edit XML Comment Template for SlideStore
    public class SlideStore : EntityStore<Slide, int>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="SlideStore" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for #ctor
        public SlideStore(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        ///     Gets the slides.
        /// </summary>
        /// <value>The slides.</value>
        /// TODO Edit XML Comment Template for Slides
        public IQueryable<Slide> Slides => Entities;
    }
}