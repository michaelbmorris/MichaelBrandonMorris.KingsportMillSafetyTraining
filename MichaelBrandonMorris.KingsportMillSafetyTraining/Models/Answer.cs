namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Represents a single answer belonging to a slide.
    /// </summary>
    public class Answer
    {
        /// <summary>
        ///     The answer's ID, generated automatically by the database.
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     The answer's index, assigned by its parent slide
        /// </summary>
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        ///     The answer's parent slide
        /// </summary>
        public virtual Slide Slide
        {
            get;
            set;
        }

        /// <summary>
        ///     The answer's title/text
        /// </summary>
        public string Title
        {
            get;
            set;
        }
    }
}