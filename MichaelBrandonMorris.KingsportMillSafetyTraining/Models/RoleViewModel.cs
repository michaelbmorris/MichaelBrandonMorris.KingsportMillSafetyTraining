using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
        }

        public RoleViewModel(Role role)
        {
            Categories = role.Categories;
            Description = role.Description;
            Id = role.Id;
            Title = role.Title;
        }

        public IList<Category> Categories
        {
            get;
            set;
        } = new List<Category>();

        public string Description
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }
    }
}