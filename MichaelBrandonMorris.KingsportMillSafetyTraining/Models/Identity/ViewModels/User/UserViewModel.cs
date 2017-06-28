using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.User
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public UserViewModel(Db.Models.User user)
        {
            CompanyName = user.CompanyName;
            Email = user.Email;
            FirstName = user.FirstName;
            Id = user.Id;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            PhoneNumber = user.PhoneNumber;

            var lastTrainingResult = user.TrainingResults.LastOrDefault();

            if (lastTrainingResult == null)
            {
                return;
            }

            LastTrainingResultDateTime = lastTrainingResult.CompletionDateTime;

            LastTrainingResultId = lastTrainingResult.Id;
        }

        [DisplayName("Company")]
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

        [DisplayName("First Name")]
        public string FirstName
        {
            get;
            set;
        }

        public string Id
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

        [DisplayName("Last Training Completed On")]
        public DateTime? LastTrainingResultDateTime
        {
            get;
            set;
        }

        public int LastTrainingResultId
        {
            get;
            set;
        }

        [DisplayName("Middle Name")]
        public string MiddleName
        {
            get;
            set;
        }

        [DisplayName("Phone Number")]
        public string PhoneNumber
        {
            get;
            set;
        }
    }
}