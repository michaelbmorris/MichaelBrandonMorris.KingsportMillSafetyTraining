using System.Data.Entity;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Migrations;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db
{
    /// <summary>
    ///     Class KingsportMillSafetyTrainingDbContext. This class
    ///     cannot be inherited.
    /// </summary>
    /// <seealso cref="IdentityDbContext{TUser}" />
    /// <seealso cref="User" />
    /// TODO Edit XML Comment Template for KingsportMillSafetyTrainingDbContext
    public sealed class KingsportMillSafetyTrainingDbContext
        : IdentityDbContext<User, Role, string, IdentityUserLogin,
            IdentityUserRole, IdentityUserClaim>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="KingsportMillSafetyTrainingDbContext" />
        ///     class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public KingsportMillSafetyTrainingDbContext()
        {
            Answers = Set<Answer>();
            Categories = Set<Category>();
            Companies = Set<Company>();
            Slides = Set<Slide>();
            TrainingResults = Set<TrainingResult>();
            Groups = Set<Group>();

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<
                    KingsportMillSafetyTrainingDbContext, Configuration>());
        }

        /// <summary>
        ///     Gets or sets the answers.
        /// </summary>
        /// <value>The answers.</value>
        /// TODO Edit XML Comment Template for Answers
        internal DbSet<Answer> Answers
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        /// TODO Edit XML Comment Template for Categories
        internal DbSet<Category> Categories
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the companies.
        /// </summary>
        /// <value>The companies.</value>
        /// TODO Edit XML Comment Template for Companies
        internal DbSet<Company> Companies
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the training groups.
        /// </summary>
        /// <value>The training groups.</value>
        /// TODO Edit XML Comment Template for Groups
        internal DbSet<Group> Groups
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the slides.
        /// </summary>
        /// <value>The slides.</value>
        /// TODO Edit XML Comment Template for Slides
        internal DbSet<Slide> Slides
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the training results.
        /// </summary>
        /// <value>The training results.</value>
        /// TODO Edit XML Comment Template for TrainingResults
        internal DbSet<TrainingResult> TrainingResults
        {
            get;
            set;
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>KingsportMillSafetyTrainingDbContext.</returns>
        /// TODO Edit XML Comment Template for Create
        public static KingsportMillSafetyTrainingDbContext Create()
        {
            return new KingsportMillSafetyTrainingDbContext();
        }
    }
}