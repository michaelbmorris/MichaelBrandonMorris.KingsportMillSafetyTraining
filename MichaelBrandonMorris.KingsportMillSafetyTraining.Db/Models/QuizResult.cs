using System;
using System.ComponentModel;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    /// <summary>
    /// Class QuizResult.
    /// </summary>
    public class QuizResult
    {
        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <value>The score.</value>
        /// TODO Edit XML Comment Template for Score
        public string Score => TotalQuestions == 0
            ? 0.ToString("P")
            : (QuestionsCorrect / (float)TotalQuestions).ToString("P");

        /// <summary>
        /// Gets the time to complete string.
        /// </summary>
        /// <value>The time to complete string.</value>
        public string TimeToCompleteString =>
                    $"{TimeToComplete.TotalMinutes:#.##} Minutes";

        /// <summary>
        /// Gets or sets the attempt number.
        /// </summary>
        /// <value>The attempt number.</value>
        public int AttemptNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the questions correct.
        /// </summary>
        /// <value>The questions correct.</value>
        [DisplayName("Questions Correct")]
        public int QuestionsCorrect
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time to complete.
        /// </summary>
        /// <value>The time to complete.</value>
        [DisplayName("Time to Complete")]
        public TimeSpan TimeToComplete
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the total questions.
        /// </summary>
        /// <value>The total questions.</value>
        [DisplayName("Total Questions")]
        public int TotalQuestions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the training result.
        /// </summary>
        /// <value>The training result.</value>
        public virtual TrainingResult TrainingResult
        {
            get;
            set;
        }
    }
}