using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
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