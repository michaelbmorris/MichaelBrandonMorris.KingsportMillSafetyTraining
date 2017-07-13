using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class GroupViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for GroupViewModel
    public class GroupViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GroupViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public GroupViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GroupViewModel" /> class.
        /// </summary>
        /// <param name="group">The group.</param>
        /// TODO Edit XML Comment Template for #ctor
        public GroupViewModel(Group group)
        {
            Categories = group.Categories;
            Description = group.Description;
            Id = group.Id;
            Index = group.Index;
            Question = group.Question;
            Title = group.Title;
        }

        /// <summary>
        ///     Gets the categories count.
        /// </summary>
        /// <value>The categories count.</value>
        /// TODO Edit XML Comment Template for CategoriesCount
        public int CategoriesCount => Categories.Count;

        /// <summary>
        ///     Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        /// TODO Edit XML Comment Template for Categories
        public IList<Category> Categories
        {
            get;
            set;
        } = new List<Category>();

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
        ///     Gets or sets the question.
        /// </summary>
        /// <value>The question.</value>
        /// TODO Edit XML Comment Template for Question
        [AllowHtml]
        public string Question
        {
            get;
            set;
        }

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