using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class AssignRolesViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for AssignRolesViewModel
    public class AssignRolesViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AssignRolesViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public AssignRolesViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AssignRolesViewModel" /> class.
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// <param name="roleViewModels">The role view models.</param>
        /// TODO Edit XML Comment Template for #ctor
        public AssignRolesViewModel(
            IList<Category> categories,
            IList<RoleViewModel> roleViewModels)
        {
            Categories = categories;
            RoleViewModels = roleViewModels;
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AssignRolesViewModel" /> class.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="roleViewModels">The role view models.</param>
        /// TODO Edit XML Comment Template for #ctor
        public AssignRolesViewModel(
            Category category,
            IList<RoleViewModel> roleViewModels)
        {
            Categories = new List<Category>
            {
                category
            };

            RoleViewModels = roleViewModels;
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
        /// TODO Edit XML Comment Template for RoleViewModels
        public IList<RoleViewModel> RoleViewModels
        {
            get;
            set;
        }
    }
}