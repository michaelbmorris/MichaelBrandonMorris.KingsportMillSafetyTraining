using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    /// <summary>
    /// Class Answer.
    /// </summary>
    /// TODO Edit XML Comment Template for Answer
    public class Answer
    {
        /// <summary>
        /// The current index
        /// </summary>
        /// TODO Edit XML Comment Template for CurrentIndex
        public static int CurrentIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Answer"/> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public Answer()
        {
            Index = ++CurrentIndex;
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
        /// Gets or sets the slide.
        /// </summary>
        /// <value>The slide.</value>
        /// TODO Edit XML Comment Template for Slide
        public virtual Slide Slide
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        /// TODO Edit XML Comment Template for Title
        [Required]
        public string Title
        {
            get;
            set;
        }
    }
}