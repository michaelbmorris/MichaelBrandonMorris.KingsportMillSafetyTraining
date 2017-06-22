using System;
using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity
{
    /// <summary>
    ///     Represents a completion of the training program for a user. 
    ///     Training is completed when the user has earned a 100% score on the 
    ///     quiz.
    /// </summary>
    public class TrainingResult
    {
        /// <summary>
        ///     When the training 
        /// </summary>
        public DateTime? CompletionDateTime
        {
            get;
            set;
        }

        /// <summary>
        ///     The unique identifier for the <see cref="TrainingResult"/>.
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     The list of <see cref="QuizResult"/>s belonging to the 
        ///     <see cref="TrainingResult"/>.
        /// </summary>
        public virtual IList<QuizResult> QuizResults
        {
            get;
            set;
        } = new List<QuizResult>();

        /// <summary>
        ///     The <see cref="Role"/> the <see cref="TrainingResult"/> 
        ///     was completed for.
        /// </summary>
        public virtual Role Role
        {
            get;
            set;
        }

        /// <summary>
        ///     How long the <see cref="User"/> spent in training before 
        ///     completing this <see cref="TrainingResult"/>.
        /// </summary>
        public TimeSpan TimeToComplete
        {
            get;
            set;
        }

        /// <summary>
        ///     The user who completed the <see cref="TrainingResult"/>.
        /// </summary>
        public virtual User User
        {
            get;
            set;
        }
    }
}