using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity
{
    public class User : IdentityUser
    {
        public User()
        {
        }

        public User(RegisterViewModel model)
        {
            BirthDate = model.BirthDate;
            CompanyName = model.CompanyName;
            FirstName = model.FirstName;
            LastName = model.LastName;
            MiddleName = model.MiddleName;
        }

        [DisplayName("Birth Date")]
        public DateTime BirthDate
        {
            get;
            set;
        }

        [DisplayName("Company")]
        public string CompanyName
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

        [DisplayName("Last Name")]
        public string LastName
        {
            get;
            set;
        }

        [DisplayName("Latest Quiz Started On")]
        public DateTime? LatestQuizStartDateTime
        {
            get;
            set;
        }

        [DisplayName("Latest Training Started On")]
        public DateTime? LatestTrainingStartDateTime
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


        public new string PhoneNumber
        {
            get;
            set;
        }

        public virtual Role Role
        {
            get;
            set;
        }

        [DisplayName("Training Results")]
        public virtual IList<TrainingResult> TrainingResults
        {
            get;
            set;
        } = new List<TrainingResult>();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(
                this,
                DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}