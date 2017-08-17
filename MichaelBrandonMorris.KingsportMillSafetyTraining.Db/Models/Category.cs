using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    /// <summary>
    ///     Class Category.
    /// </summary>
    /// TODO Edit XML Comment Template for Category
    public class Category : IEntity<int>
    {
        /// <summary>
        ///     The current index
        /// </summary>
        /// TODO Edit XML Comment Template for CurrentIndex
        public static int CurrentIndex
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        /// TODO Edit XML Comment Template for Description
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        /// TODO Edit XML Comment Template for DisplayName
        [DisplayName("Display Name")]
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        /// TODO Edit XML Comment Template for Groups
        public virtual IList<Group> Groups
        {
            get;
            set;
        } = new List<Group>();

        /// <summary>
        ///     Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        /// TODO Edit XML Comment Template for Index
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        /// TODO Edit XML Comment Template for Name
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the slides.
        /// </summary>
        /// <value>The slides.</value>
        /// TODO Edit XML Comment Template for Slides
        public virtual ICollection<Slide> Slides
        {
            get;
            set;
        } = new Collection<Slide>();

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// TODO Edit XML Comment Template for Id
        public int Id
        {
            get;
            set;
        }
    }
}