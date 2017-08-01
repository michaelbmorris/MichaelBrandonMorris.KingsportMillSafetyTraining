using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class SelectGroupViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for SelectGroupViewModel
    public class SelectGroupViewModel
    {
        /// <summary>
        ///     Gets or sets the default index of the group.
        /// </summary>
        /// <value>The default index of the group.</value>
        /// TODO Edit XML Comment Template for DefaultGroupIndex
        public int DefaultGroupIndex
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the groups.
        /// </summary>
        /// <value>The groups.</value>
        /// TODO Edit XML Comment Template for Groups
        public IList<GroupViewModel> Groups
        {
            get;
            set;
        }
    }
}