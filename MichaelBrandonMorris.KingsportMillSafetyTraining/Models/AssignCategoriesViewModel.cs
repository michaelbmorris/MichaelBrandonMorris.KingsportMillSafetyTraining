using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class AssignCategoriesViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for AssignCategoriesViewModel
    public class AssignCategoriesViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AssignCategoriesViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public AssignCategoriesViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AssignCategoriesViewModel" /> class.
        /// </summary>
        /// <param name="groups">The groups.</param>
        /// <param name="categoryViewModels">The category view models.</param>
        /// TODO Edit XML Comment Template for #ctor
        public AssignCategoriesViewModel(
            IList<Group> groups,
            IList<CategoryViewModel> categoryViewModels)
        {
            Groups = groups;
            CategoryViewModels = categoryViewModels;
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AssignCategoriesViewModel" /> class.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="categoryViewModels">The category view models.</param>
        /// TODO Edit XML Comment Template for #ctor
        public AssignCategoriesViewModel(
            Group role,
            IList<CategoryViewModel> categoryViewModels)
        {
            Groups = new List<Group>
            {
                role
            };

            CategoryViewModels = categoryViewModels;
        }

        /// <summary>
        ///     Gets or sets the category view models.
        /// </summary>
        /// <value>The category view models.</value>
        /// TODO Edit XML Comment Template for CategoryViewModels
        public IList<CategoryViewModel> CategoryViewModels
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the groups.
        /// </summary>
        /// <value>The groups.</value>
        /// TODO Edit XML Comment Template for Groups
        public IList<Group> Groups
        {
            get;
            set;
        }
    }
}