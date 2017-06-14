using System;
using System.ComponentModel;
using System.Linq;

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
            QuizAttemptsCount = trainingResult.QuizAttemptsCount;
            RoleTitle = trainingResult.Role.Title;
            TimeToComplete = trainingResult.TimeToComplete;
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

        [DisplayName("Number of Quiz Attempts")]
        public int QuizAttemptsCount
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