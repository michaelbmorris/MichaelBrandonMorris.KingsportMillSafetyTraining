using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data
{
    /// <summary>
    /// Answers are assigned to slides
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Tracks the current index being used by answers.
        /// </summary>
        public static int CurrentIndex;

        /// <summary>
        /// Creates a new <see cref="Answer"/> with the next 
        /// <see cref="Index"/>.
        /// </summary>
        public Answer()
        {
            Index = ++CurrentIndex;
        }

        /// <summary>
        /// The answer's ID, generated automatically by the database.
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// The answer's index, assigned by its parent slide
        /// </summary>
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// The answer's parent slide
        /// </summary>
        public virtual Slide Slide
        {
            get;
            set;
        }

        /// <summary>
        /// The answer's title/text
        /// </summary>
        [Required]
        public string Title
        {
            get;
            set;
        }
    }
}