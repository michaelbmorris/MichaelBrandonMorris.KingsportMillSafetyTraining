using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data
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
        /// The role's index, used to determine role order and the default role
        /// </summary>
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// The role's question, asked to users to determine if they belong to
        /// this role.
        /// </summary>
        public string Question
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