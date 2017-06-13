using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.DataModels;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.ViewModels
{
    public class AssignRolesViewModel
    {
        public AssignRolesViewModel()
        {
        }

        public AssignRolesViewModel(
            IList<Category> categories,
            IList<RoleViewModel> roleViewModels)
        {
            Categories = categories;
            RoleViewModels = roleViewModels;
        }

        public IList<Category> Categories
        {
            get;
            set;
        }

        public IList<RoleViewModel> RoleViewModels
        {
            get;
            set;
        }
    }
}