using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class AssignGroupsViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for AssignGroupsViewModel
    public class AssignGroupsViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AssignGroupsViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public AssignGroupsViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AssignGroupsViewModel" /> class.
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// <param name="groupViewModels">The role view models.</param>
        /// TODO Edit XML Comment Template for #ctor
        public AssignGroupsViewModel(
            IList<Category> categories,
            IList<GroupViewModel> groupViewModels)
        {
            Categories = categories;
            GroupViewModels = groupViewModels;
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AssignGroupsViewModel" /> class.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="groupViewModels">The role view models.</param>
        /// TODO Edit XML Comment Template for #ctor
        public AssignGroupsViewModel(
            Category category,
            IList<GroupViewModel> groupViewModels)
        {
            Categories = new List<Category>
            {
                category
            };

            GroupViewModels = groupViewModels;
        }

        /// <summary>
        ///     Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        /// TODO Edit XML Comment Template for Categories
        public IList<Category> Categories
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the role view models.
        /// </summary>
        /// <value>The role view models.</value>
        /// TODO Edit XML Comment Template for GroupViewModels
        public IList<GroupViewModel> GroupViewModels
        {
            get;
            set;
        }
    }
}