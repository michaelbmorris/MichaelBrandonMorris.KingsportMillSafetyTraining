using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class ListQuantityAttribute.
    /// </summary>
    /// <seealso
    ///     cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// TODO Edit XML Comment Template for ListQuantityAttribute
    public class ListQuantityAttribute : ValidationAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ListQuantityAttribute" /> class.
        /// </summary>
        /// <param name="minElements">The minimum elements.</param>
        /// <param name="maxElements">The maximum elements.</param>
        /// TODO Edit XML Comment Template for #ctor
        public ListQuantityAttribute(int minElements = 0, int maxElements = 0)
        {
            MinElements = minElements;
            MaxElements = maxElements;
        }

        /// <summary>
        ///     Gets the maximum elements.
        /// </summary>
        /// <value>The maximum elements.</value>
        /// TODO Edit XML Comment Template for MaxElements
        private int MaxElements
        {
            get;
        }

        /// <summary>
        ///     Gets the minimum elements.
        /// </summary>
        /// <value>The minimum elements.</value>
        /// TODO Edit XML Comment Template for MinElements
        private int MinElements
        {
            get;
        }

        /// <summary>
        ///     Determines whether the specified value of the object is
        ///     valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        ///     true if the specified value is valid; otherwise,
        ///     false.
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        /// TODO Edit XML Comment Template for IsValid
        public override bool IsValid(object value)
        {
            var list = value as IList;

            if (list == null)
            {
                throw new ArgumentException();
            }

            if (MinElements == 0
                && MaxElements == 0)
            {
                return true;
            }

            if (MinElements == 0)
            {
                return list.Count <= MaxElements;
            }

            if (MaxElements == 0)
            {
                return list.Count >= MinElements;
            }

            return list.Count <= MaxElements && list.Count >= MinElements;
        }
    }
}