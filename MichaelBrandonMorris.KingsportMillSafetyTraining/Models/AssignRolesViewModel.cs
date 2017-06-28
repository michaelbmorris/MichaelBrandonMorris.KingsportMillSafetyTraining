using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

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