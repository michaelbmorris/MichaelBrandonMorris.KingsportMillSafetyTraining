using System.Configuration;
using System.Linq;
using System.Web.Security;
using MichaelBrandonMorris.KingsportMillSafetyTraining;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class Startup.
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
            {
                var ownerUserName =
                    ConfigurationManager.AppSettings["OwnerUserName"];

                var userManager =
                    new ApplicationUserManager(
                        new UserStore<User, Role, string, IdentityUserLogin,
                            IdentityUserRole, IdentityUserClaim>(db))
                    {
                        EmailService = new EmailService()
                    };

                userManager.RegisterTwoFactorProvider("Email Code", new
                    EmailTokenProvider<User>
                    {
                        Subject = "Security Code", BodyFormat =
                            "Your security code is {0}"
                    });

                var user = await userManager.FindByNameAsync(ownerUserName);

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
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user.Id, "Owner");

                await userManager.SendEmailAsync(
                    user.Id,
                    "Password",
                    password);
            }
        }
    }
}