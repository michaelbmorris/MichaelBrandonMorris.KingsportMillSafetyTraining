using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    /// <summary>
    ///     Class Role.
    /// </summary>
    /// TODO Edit XML Comment Template for Role
    public class Role
    {
        /// <summary>
        ///     Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        /// TODO Edit XML Comment Template for Categories
        public virtual IList<Category> Categories
        {
            get;
            set;
        } = new List<Category>();

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
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// TODO Edit XML Comment Template for Id
        public int Id
        {
            get;
            set;
        }

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
        ///     Gets or sets the question.
        /// </summary>
        /// <value>The question.</value>
        /// TODO Edit XML Comment Template for Question
        public string Question
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        /// TODO Edit XML Comment Template for Title
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the index of the current.
        /// </summary>
        /// <value>The index of the current.</value>
        /// TODO Edit XML Comment Template for CurrentIndex
        public static int CurrentIndex
        {
            get;
            set;
        }
    }
}