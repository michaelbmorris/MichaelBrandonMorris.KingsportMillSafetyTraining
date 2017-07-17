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
            }
        }

        /// <summary>
        ///     Creates the roles.
        /// </summary>
        /// TODO Edit XML Comment Template for CreateRoles
        private void CreateRoles()
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                var roleManager =
                    new RoleManager<Role>(
                        new RoleStore<Role>(db));

                if (!roleManager.RoleExists("Administrator"))
                {
                    roleManager.Create(
                        new Role
                        {
                            Name = "Administrator"
                        });
                }

                if (!roleManager.RoleExists("User"))
                {
                    roleManager.Create(
                        new Role
                        {
                            Name = "User"
                        });
                }
            }
        }
    }
}