using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    /// Roles are assigned categories and are assigned to users.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// The role's categories
        /// </summary>
        public virtual IList<Category> Categories
        {
            get;
            set;
        } = new List<Category>();

        /// <summary>
        /// The role's description
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The role's ID, generated automatically by the database
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// The role's title
        /// </summary>
        public string Title
        {
            get;
            set;
        }
    }
}