using System.Data.Entity.Migrations;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

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

            db.Roles.AddOrUpdate(
                role => role.Name,
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
        }
    }
}