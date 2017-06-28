using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    /// <summary>
    /// Class Category.
    /// </summary>
    /// TODO Edit XML Comment Template for Category
    public class Category
    {
        /// <summary>
        /// The current index
        /// </summary>
        /// TODO Edit XML Comment Template for CurrentIndex
        public static int CurrentIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public Category()
        {
            Index = ++CurrentIndex;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        /// TODO Edit XML Comment Template for Description
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// TODO Edit XML Comment Template for Id
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        /// TODO Edit XML Comment Template for Index
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        /// TODO Edit XML Comment Template for Roles
        public virtual IList<Role> Roles
        {
            get;
            set;
        } = new List<Role>();

        /// <summary>
        /// Gets or sets the slides.
        /// </summary>
        /// <value>The slides.</value>
        /// TODO Edit XML Comment Template for Slides
        public virtual IList<Slide> Slides
        {
            get;
            set;
        } = new List<Slide>();

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        /// TODO Edit XML Comment Template for Title
        public string Title
        {
            get;
            set;
        }
    }
}