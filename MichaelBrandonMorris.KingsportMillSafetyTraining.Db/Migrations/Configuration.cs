using System.Data.Entity.Migrations;

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
        }
    }
}