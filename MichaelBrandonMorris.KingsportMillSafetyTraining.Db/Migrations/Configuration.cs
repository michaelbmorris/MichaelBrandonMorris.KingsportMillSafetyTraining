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
        }
    }
}