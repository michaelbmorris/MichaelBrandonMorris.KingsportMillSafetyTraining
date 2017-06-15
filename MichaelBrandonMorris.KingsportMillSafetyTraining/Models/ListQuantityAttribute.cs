using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class ListQuantityAttribute : ValidationAttribute
    {
        public ListQuantityAttribute(int minElements = 0, int maxElements = 0)
        {
            MinElements = minElements;
            MaxElements = maxElements;
        }

        private int MaxElements { get; }

        private int MinElements { get; }

        public override bool IsValid(object value)
        {
            var list = value as IList;

            if (list == null)
                throw new ArgumentException();

            if (MinElements == 0 && MaxElements == 0)
                return true;

            if (MinElements == 0)
                return list.Count <= MaxElements;

            if (MaxElements == 0)
                return list.Count >= MinElements;

            return list.Count <= MaxElements && list.Count >= MinElements;
        }
    }
}