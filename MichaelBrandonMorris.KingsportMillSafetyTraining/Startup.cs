using MichaelBrandonMorris.KingsportMillSafetyTraining;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
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
                db.UpdateCurrentSlideIndex();
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
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(db));

                if (!roleManager.RoleExists("Administrator"))
                {
                    roleManager.Create(
                        new IdentityRole
                        {
                            Name = "Administrator"
                        });
                }

                if (!roleManager.RoleExists("User"))
                {
                    roleManager.Create(
                        new IdentityRole
                        {
                            Name = "User"
                        });
                }
            }
        }
    }
}