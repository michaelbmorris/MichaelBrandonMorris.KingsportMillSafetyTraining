﻿using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.
    ViewModels
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

        public AssignCategoriesViewModel(
            Role role,
            IList<CategoryViewModel> categoryViewModels)
        {
            Roles = new List<Role>
            {
                role
            };

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