using Microsoft.AspNet.Identity.EntityFramework;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models
{
    /// <summary>
    ///     Class Role.
    /// </summary>
    /// <seealso
    ///     cref="Microsoft.AspNet.Identity.EntityFramework.IdentityRole" />
    /// TODO Edit XML Comment Template for Role
    public class Role : IdentityRole
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Role" />
        ///     class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public Role()
        {
            Index = ++CurrentIndex;
        }

        /// <summary>
        ///     Gets or sets the index of the current.
        /// </summary>
        /// <value>The index of the current.</value>
        /// TODO Edit XML Comment Template for CurrentIndex
        public static int CurrentIndex
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        /// TODO Edit XML Comment Template for Description
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        /// TODO Edit XML Comment Template for Index
        public int Index
        {
            get;
            set;
        }
    }
}