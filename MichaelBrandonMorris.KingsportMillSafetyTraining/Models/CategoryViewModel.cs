using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {         
        }

        public CategoryViewModel(Category category)
        {
            Description = category.Description;
            Id = category.Id;
            Index = category.Index;
            Roles = category.Roles;
            Title = category.Title;
        }

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

        public int Index
        {
            get;
            set;
        }

        public IList<Role> Roles
        {
            get;
            set;
        } = new List<Role>();

        public string Title
        {
            get;
            set;
        }
    }
}