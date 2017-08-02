using System.Configuration;
using System.Linq;
using System.Web.Security;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.Owin;
using Owin;
using UserStore =
    Microsoft.AspNet.Identity.EntityFramework.UserStore<
        MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models.User,
        MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models.Role, string,
        Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin,
        Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole, Microsoft.
        AspNet.Identity.EntityFramework.IdentityUserClaim>;

[assembly: OwinStartup(typeof(Startup))]

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    /// Class Startup.
    /// </summary>
    /// TODO Edit XML Comment Template for Startup
    public partial class Startup
    {
        /// <summary>
        ///     Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// TODO Edit XML Comment Template for Configuration
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateOwner();
            UpdateCurrentIndices();
            UpdateUserRoles();
        }

        /// <summary>
        ///     Updates the current indices.
        /// </summary>
        /// TODO Edit XML Comment Template for UpdateCurrentIndices
        private static void UpdateCurrentIndices()
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                db.UpdateCurrentAnswerIndex();
                db.UpdateCurrentCategoryIndex();
                db.UpdateCurrentGroupIndex();
                db.UpdateCurrentRoleIndex();
            }
        }

        /// <summary>
        ///     Creates the owner account.
        /// </summary>
        /// TODO Edit XML Comment Template for CreateRoles
        private async void CreateOwner()
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            using (var store = new UserStore(db))
            using (var manager = new ApplicationUserManager(store)
            {
                EmailService = new EmailService()
            })
            {
                var ownerUserName =
                    ConfigurationManager.AppSettings["OwnerUserName"];

                var user = await manager.FindByNameAsync(ownerUserName);

                if (user != null)
                {
                    return;
                }

                user = new User
                {
                    UserName = ownerUserName,
                    Email = ownerUserName
                };

                var password = Membership.GeneratePassword(8, 1);
                await manager.CreateAsync(user, password);
                await manager.AddToRoleAsync(user.Id, "Owner");
                await manager.SendEmailAsync(user.Id, "Password", password);
            }
        }

        /// <summary>
        /// Updates the user roles.
        /// </summary>
        /// TODO Edit XML Comment Template for UpdateUserRoles
        private async void UpdateUserRoles()
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            using (var store = new UserStore(db))
            using (var manager = new ApplicationUserManager(store))
            {
                foreach (var user in manager.Users.ToList())
                {
                    if (user.Roles.IsNullOrEmpty())
                    {
                        await manager.AddToRoleAsync(user.Id, "User");
                    }
                }
            }
        }
    }
}