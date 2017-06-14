using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.Result
{
    public class TrainingResultViewModel
    {
        public TrainingResultViewModel()
        {
        }

        public TrainingResultViewModel(
            ApplicationUser user,
            TrainingResult trainingResult)
        {
            CompanyName = user.CompanyName;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserId = user.Id;
            CompletionDateTime = trainingResult.CompletionDateTime;
            Id = trainingResult.Id;
            QuizResults = trainingResult.QuizResults;
            RoleTitle = trainingResult.Role.Title;
            TimeToComplete = trainingResult.TimeToComplete;
        }

        [DisplayName("Number of Quiz Attempts")]
        public int QuizAttemptsCount
        {
            get
            {
                return QuizResults.Count;
            }
        }

        [DisplayName("Company")]
        public string CompanyName
        {
            get;
            set;
        }

        [DisplayName("Completed On")]
        public DateTime CompletionDateTime
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        [DisplayName("First Name")]
        public string FirstName
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        [DisplayName("Last Name")]
        public string LastName
        {
            get;
            set;
        }

        [DisplayName("Quiz Results")]
        public IList<QuizResult> QuizResults
        {
            get;
            set;
        }

        [DisplayName("Role")]
        public string RoleTitle
        {
            get;
            set;
        }

        [DisplayName("Time to Complete")]
        public TimeSpan TimeToComplete
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }
    }
}