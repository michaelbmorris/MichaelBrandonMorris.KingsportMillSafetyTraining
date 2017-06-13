using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.DataModels;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.IdentityModels
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

        public string MobilePhoneNumber
        {
            get;
            set;
        }

        public string WorkPhoneNumber
        {
            get;
            set;
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

        public virtual Role Role
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