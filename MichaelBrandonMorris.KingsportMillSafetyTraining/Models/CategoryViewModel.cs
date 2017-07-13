using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class CategoryViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for CategoryViewModel
    public class CategoryViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="CategoryViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public CategoryViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="CategoryViewModel" /> class.
        /// </summary>
        /// <param name="category">The category.</param>
        /// TODO Edit XML Comment Template for #ctor
        public CategoryViewModel(Category category)
        {
            Description = category.Description;
            Id = category.Id;
            Index = category.Index;
            Roles = category.Groups;
            Slides = category.Slides;
            Title = category.Title;
        }

        /// <summary>
        ///     Gets the roles list.
        /// </summary>
        /// <value>The roles list.</value>
        /// TODO Edit XML Comment Template for RolesList
        public string RolesList => Roles.Aggregate(
            string.Empty,
            (current, role) => current + "<li>" + role.Title + "</li>");

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        /// TODO Edit XML Comment Template for Description
        [AllowHtml]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// TODO Edit XML Comment Template for Id
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        /// TODO Edit XML Comment Template for Index
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        /// TODO Edit XML Comment Template for Groups
        public IList<Group> Roles
        {
            get;
            set;
        } = new List<Group>();

        /// <summary>
        ///     Gets or sets the slides.
        /// </summary>
        /// <value>The slides.</value>
        /// TODO Edit XML Comment Template for Slides
        public IList<Slide> Slides
        {
            get;
            set;
        } = new List<Slide>();

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        /// TODO Edit XML Comment Template for Title
        [Required]
        public string Title
        {
            get;
            set;
        }
    }
}