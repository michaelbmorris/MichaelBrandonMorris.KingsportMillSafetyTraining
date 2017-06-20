using System.Collections.Generic;
using System.Linq;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.
    ViewModels
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
            Slides = category.Slides;
            Title = category.Title;
        }

        public string RolesList => Roles.Aggregate(
            string.Empty,
            (current, role) => current + "<li>" + role.Title + "</li>");

        public int SlidesCount => Slides.Count;
        public int RolesCount => Roles.Count;

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

        public IList<Slide> Slides
        {
            get;
            set;
        } = new List<Slide>();

        public string Title
        {
            get;
            set;
        }
    }
}