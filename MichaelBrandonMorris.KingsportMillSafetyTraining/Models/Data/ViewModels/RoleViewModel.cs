using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels
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
            Question = role.Question;
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

        public string Question
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