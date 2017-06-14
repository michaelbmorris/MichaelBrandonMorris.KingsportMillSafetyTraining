using System;
using System.Linq;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.User
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public UserViewModel(ApplicationUser user)
        {
            BirthDate = user.BirthDate;
            CompanyName = user.CompanyName;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            PhoneNumber = user.PhoneNumber;

            var lastTrainingResult = user.TrainingResults.LastOrDefault();

            if (lastTrainingResult == null)
            {
                return;
            }

            LastTrainingResultDateTime =
                lastTrainingResult.CompletionDateTime;

            LastTrainingResultId = lastTrainingResult.Id;
        }

        public DateTime BirthDate
        {
            get;
            set;
        }

        public string CompanyName
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public DateTime LastTrainingResultDateTime
        {
            get;
            set;
        }

        public int LastTrainingResultId
        {
            get;
            set;
        }

        public string MiddleName
        {
            get;
            set;
        }

        public string PhoneNumber
        {
            get;
            set;
        }
    }
}