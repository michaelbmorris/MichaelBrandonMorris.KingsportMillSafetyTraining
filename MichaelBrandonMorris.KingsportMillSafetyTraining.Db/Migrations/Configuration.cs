using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Migrations
{
    internal sealed class Configuration
        : DbMigrationsConfiguration<KingsportMillSafetyTrainingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(KingsportMillSafetyTrainingDbContext db)
        {
            db.Companies.AddOrUpdate(
                company => company.Name,
                new Company
                {
                    Name = "Other"
                });

            if (!db.Roles.Any(role => role.Name == "Administrator"))
            {
                var roleStore = new RoleStore<Role>(db);
                var roleManager = new RoleManager<Role>(roleStore);

                var role = new Role
                {
                    Name = "Administrator"
                };

                roleManager.Create(role);
            }

            // ReSharper disable once InvertIf
            if (!db.Users.Any(user => user.UserName == "admin"))
            {
                var userStore =
                    new UserStore<User, Role, string, IdentityUserLogin,
                        IdentityUserRole, IdentityUserClaim>(db);

                var userManager = new UserManager<User, string>(userStore);

                var user = new User
                {
                    UserName = "admin@admin.admin",
                    Email = "admin@admin.admin",
                };

                var createResult = userManager.Create(user, "password");

                if (createResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                }
                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        Debug.WriteLine(error);
                    }
                }
            }
        }
    }
}