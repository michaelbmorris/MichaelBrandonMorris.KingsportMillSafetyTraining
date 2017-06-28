using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data
{
    /// <summary>
    ///     Categories have a list of slides and are assigned to roles.
    /// </summary>
    public class Category
    {
        /// <summary>
        ///     Tracks the current index being used by categories.
        /// </summary>
        public static int CurrentIndex;

        /// <summary>
        ///     Creates a new <see cref="Category" /> with the next
        ///     <see cref="Index" />.
        /// </summary>
        public Category()
        {
            Index = ++CurrentIndex;
        }

        /// <summary>
        ///     The category's description
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        ///     The category's ID, generated automatically by the database
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Determines the ordering of this category.
        /// </summary>
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        ///     The category's roles
        /// </summary>
        public virtual IList<Role> Roles
        {
            get;
            set;
        } = new List<Role>();

        /// <summary>
        ///     The category's slides
        /// </summary>
        public virtual IList<Slide> Slides
        {
            get;
            set;
        } = new List<Slide>();

        /// <summary>
        ///     The category's title
        /// </summary>
        public string Title
        {
            get;
            set;
        }
    }
}