using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public ApplicationUser(RegisterViewModel model)
        {
            BirthDate = model.BirthDate;
            CompanyName = model.CompanyName;
            FirstName = model.FirstName;
            LastName = model.LastName;
            MiddleName = model.MiddleName;
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

        public string MiddleName
        {
            get;
            set;
        }

        public string MobilePhoneNumber
        {
            get;
            set;
        }

        public virtual Role Role
        {
            get;
            set;
        }

        public DateTime LatestTrainingStartDateTime
        {
            get;
            set;
        }

        public virtual IList<TrainingResult> TrainingResults
        {
            get;
            set;
        } = new List<TrainingResult>();

        public string WorkPhoneNumber
        {
            get;
            set;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(
                this,
                DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}