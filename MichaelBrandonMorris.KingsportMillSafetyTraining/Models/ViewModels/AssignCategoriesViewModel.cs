using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.DataModels;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.ViewModels
{
    public class AssignCategoriesViewModel
    {
        public AssignCategoriesViewModel()
        {
        }

        public AssignCategoriesViewModel(
            IList<Role> roles,
            IList<CategoryViewModel> categoryViewModels)
        {
            Roles = roles;
            CategoryViewModels = categoryViewModels;
        }

        public IList<CategoryViewModel> CategoryViewModels
        {
            get;
            set;
        }

        public IList<Role> Roles
        {
            get;
            set;
        }
    }
}