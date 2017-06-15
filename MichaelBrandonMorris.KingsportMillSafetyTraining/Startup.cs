using MichaelBrandonMorris.KingsportMillSafetyTraining;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
            UpdateCurrentIndices();
            app.CreatePerOwinContext<ApplicationUserManager>(
                ApplicationUserManager.Create);
        }

        private static void UpdateCurrentIndices()
        {
            using (var db = new ApplicationDbContext())
            {
                db.UpdateCurrentAnswerIndex();
                db.UpdateCurrentCategoryIndex();
                db.UpdateCurrentSlideIndex();
            }
        }

        private void CreateRoles()
        {
            using (var db = new ApplicationDbContext())
            {
                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(db));

                if (!roleManager.RoleExists("Administrator"))
                    roleManager.Create(
                        new IdentityRole
                        {
                            Name = "Administrator"
                        });

                if (!roleManager.RoleExists("User"))
                    roleManager.Create(
                        new IdentityRole
                        {
                            Name = "User"
                        });
            }
        }
    }
}