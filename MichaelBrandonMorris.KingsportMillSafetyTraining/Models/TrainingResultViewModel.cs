using System;
using System.Collections.Generic;
using System.ComponentModel;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class TrainingResultViewModel.
    /// </summary>
    public class TrainingResultViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="TrainingResultViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public TrainingResultViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="TrainingResultViewModel" /> class.
        /// </summary>
        /// <param name="trainingResult">The training result.</param>
        /// TODO Edit XML Comment Template for #ctor
        public TrainingResultViewModel(TrainingResult trainingResult)
        {
            CompletionDateTime = trainingResult.CompletionDateTime;
            Id = trainingResult.Id;
            QuizResults = trainingResult.QuizResults;
            GroupTitle = trainingResult.Group.Title;
            TimeToComplete = trainingResult.TimeToComplete;
            var user = trainingResult.User;

            if (user == null)
            {
                return;
            }

            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
            UserId = user.Id;
            var company = user.Company;

            if (company != null)
            {
                CompanyName = user.Company.Name;
            }
        }

        public bool IsUserTrainingResult
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the completion date time string.
        /// </summary>
        /// <value>The completion date time string.</value>
        /// TODO Edit XML Comment Template for CompletionDateTimeString
        [DisplayName("Completed On")]
        public string CompletionDateTimeString => CompletionDateTime == null
            ? "Training not completed."
            : CompletionDateTime.ToString();

        /// <summary>
        ///     Gets the quiz attempts count.
        /// </summary>
        /// <value>The quiz attempts count.</value>
        /// TODO Edit XML Comment Template for QuizAttemptsCount
        [DisplayName("Number of Quiz Attempts")]
        public int QuizAttemptsCount => QuizResults.Count;

        /// <summary>
        ///     Gets the time to complete string.
        /// </summary>
        /// <value>The time to complete string.</value>
        [DisplayName("Time to Complete")]
        public string TimeToCompleteString =>
            new DateTime(TimeToComplete.Ticks).ToString("mm:ss");

        /// <summary>
        ///     Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        /// TODO Edit XML Comment Template for CompanyName
        [DisplayName("Company")]
        public string CompanyName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the completion date time.
        /// </summary>
        /// <value>The completion date time.</value>
        /// TODO Edit XML Comment Template for CompletionDateTime
        [DisplayName("Completed On")]
        public DateTime? CompletionDateTime
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        /// TODO Edit XML Comment Template for Email
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        /// TODO Edit XML Comment Template for FirstName
        [DisplayName("First Name")]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the role title.
        /// </summary>
        /// <value>The role title.</value>
        /// TODO Edit XML Comment Template for GroupTitle
        [DisplayName("Group")]
        public string GroupTitle
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
        ///     Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        /// TODO Edit XML Comment Template for LastName
        [DisplayName("Last Name")]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        /// TODO Edit XML Comment Template for PhoneNumber
        [DisplayName("Phone Number")]
        public string PhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the quiz results.
        /// </summary>
        /// <value>The quiz results.</value>
        /// TODO Edit XML Comment Template for QuizResults
        [DisplayName("Quiz Results")]
        public IList<QuizResult> QuizResults
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the time to complete.
        /// </summary>
        /// <value>The time to complete.</value>
        /// TODO Edit XML Comment Template for TimeToComplete
        public TimeSpan TimeToComplete
        {
            get;
            set;
        }
    }
}