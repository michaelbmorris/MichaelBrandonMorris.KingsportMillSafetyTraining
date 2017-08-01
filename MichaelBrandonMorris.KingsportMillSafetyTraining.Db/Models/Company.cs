using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    /// <summary>
    ///     Class Company.
    /// </summary>
    /// TODO Edit XML Comment Template for Company
    public class Company
    {
        /// <summary>
        ///     Gets or sets the employees.
        /// </summary>
        /// <value>The employees.</value>
        /// TODO Edit XML Comment Template for Employees
        public virtual IList<User> Employees
        {
            get;
            set;
        } = new List<User>();

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// TODO Edit XML Comment Template for Id
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        /// TODO Edit XML Comment Template for Name
        public string Name
        {
            get;
            set;
        }
    }
}