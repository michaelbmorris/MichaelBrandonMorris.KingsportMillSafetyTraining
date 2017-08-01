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
            CreateRoles();
            UpdateCurrentIndices();
            app.CreatePerOwinContext<ApplicationUserManager>(
                ApplicationUserManager.Create);
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
        ///     Creates the roles.
        /// </summary>
        /// TODO Edit XML Comment Template for CreateRoles
        private async void CreateRoles()
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                db.AddOrUpdate<Company>(
                    new Company
                    {
                        Name = "Other"
                    });

                db.AddOrUpdate<Role>(
                    new Role
                    {
                        Index = 0,
                        Name = "User"
                    },
                    new Role
                    {
                        Index = 1,
                        Name = "Supervisor"
                    },
                    new Role
                    {
                        Index = 2,
                        Name = "Security"
                    },
                    new Role
                    {
                        Index = 3,
                        Name = "Collaborator"
                    },
                    new Role
                    {
                        Index = 4,
                        Name = "Administrator"
                    },
                    new Role
                    {
                        Index = 5,
                        Name = "Owner"
                    });

                var ownerUserName =
                    ConfigurationManager.AppSettings["OwnerUserName"];

                // ReSharper disable once InvertIf
                if (!db.Users.Any(user => user.UserName == ownerUserName))
                {
                    var userManager =
                        new ApplicationUserManager(
                            new UserStore<User, Role, string, IdentityUserLogin,
                                IdentityUserRole, IdentityUserClaim>(db))
                        {
                            EmailService = new EmailService()
                        };

                    var user = new User
                    {
                        UserName = ownerUserName,
                        Email = ownerUserName
                    };

                    var password = Membership.GeneratePassword(8, 1);
                    await userManager.AddToRoleAsync(user.Id, "Owner");

                    await userManager.SendEmailAsync(
                        user.Id,
                        "Password",
                        password);

                    var code = await userManager
                        .GenerateEmailConfirmationTokenAsync(user.Id);

                    await userManager.ConfirmEmailAsync(user.Id, code);
                }
            }
        }
    }
}