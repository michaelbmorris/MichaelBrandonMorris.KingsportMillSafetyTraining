using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class Category
    {
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
        ///     The category's roles
        /// </summary>
        public IList<Role> Roles
        {
            get;
            set;
        }

        /// <summary>
        ///     The category's slides
        /// </summary>
        public IList<Slide> Slides
        {
            get;
            set;
        }

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