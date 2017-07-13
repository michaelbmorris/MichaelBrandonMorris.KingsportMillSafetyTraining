using System;
using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    /// <summary>
    /// Class TrainingResult.
    /// </summary>
    /// TODO Edit XML Comment Template for TrainingResult
    public class TrainingResult
    {
        /// <summary>
        /// Gets or sets the completion date time.
        /// </summary>
        /// <value>The completion date time.</value>
        /// TODO Edit XML Comment Template for CompletionDateTime
        public DateTime? CompletionDateTime
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
        /// Gets or sets the quiz results.
        /// </summary>
        /// <value>The quiz results.</value>
        /// TODO Edit XML Comment Template for QuizResults
        public virtual IList<QuizResult> QuizResults
        {
            get;
            set;
        } = new List<QuizResult>();

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        /// TODO Edit XML Comment Template for Group
        public virtual Group Group
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time to complete.
        /// </summary>
        /// <value>The time to complete.</value>
        /// TODO Edit XML Comment Template for TimeToComplete
        public TimeSpan TimeToComplete
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        /// TODO Edit XML Comment Template for User
        public virtual User User
        {
            get;
            set;
        }
    }
}