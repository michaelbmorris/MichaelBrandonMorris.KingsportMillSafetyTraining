using System.Data.Entity;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    /// The database context for the program. Stores references to all the 
    /// database tables, as well as methods to perform operations on all the 
    /// data in transactions.
    /// </summary>
    public class KingsportMillSafetyTrainingDbContext : DbContext
    {
        /// <summary>
        /// The answers table.
        /// </summary>
        public DbSet<Answer> Answers
        {
            get;
            set;
        }

        /// <summary>
        /// The categories table.
        /// </summary>
        public DbSet<Category> Categories
        {
            get;
            set;
        }

        /// <summary>
        /// The roles table.
        /// </summary>
        public DbSet<Role> Roles
        {
            get;
            set;
        }

        /// <summary>
        /// The slides table.
        /// </summary>
        public DbSet<Slide> Slides
        {
            get;
            set;
        }
    }
}