using System;
using System.Collections.Generic;
using System.Linq;
using Foolproof;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Validation
{
    /// <summary>
    ///     Class AnswersValidAttribute.
    /// </summary>
    /// <seealso cref="ModelAwareValidationAttribute" />
    /// TODO Edit XML Comment Template for AnswersValidAttribute
    public class AnswersValidAttribute : ModelAwareValidationAttribute
    {
        /// <summary>
        ///     Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="container">The container.</param>
        /// <returns>
        ///     <c>true</c> if the specified value is valid;
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Attribute must be applied within a slide view model.
        ///     or
        ///     Attribute must be applied to a list of answers.
        /// </exception>
        /// <exception cref="ArgumentNullException">value</exception>
        /// TODO Edit XML Comment Template for IsValid
        public override bool IsValid(object value, object container)
        {
            var model = container as SlideViewModel;

            if (model == null)
            {
                throw new ArgumentException(
                    "Attribute must be applied within a slide view model.");
            }

            if (!model.ShouldShowQuestionOnQuiz)
            {
                return true;
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!IsValidType(value.GetType()))
            {
                throw new ArgumentException(
                    "Attribute must be applied to a list of answers.");
            }

            var list = (IList<Answer>) value;

            if (model.Question.IsNullOrWhiteSpace())
            {
                ErrorMessage = "Question is required.";
                return false;
            }

            if (list.Count < 2)
            {
                ErrorMessage = "Two to five answers are required.";
                return false;
            }

            if (list.Any(answer => answer.Title.IsNullOrWhiteSpace()))
            {
                ErrorMessage = "No answer may be blank.";
                return false;
            }

            // ReSharper disable once InvertIf
            if (list.All(answer => answer.Index != model.CorrectAnswerIndex))
            {
                ErrorMessage = "An answer must be marked as correct.";
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Determines whether [is valid type] [the specified
        ///     type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     <c>true</c> if [is valid type] [the specified
        ///     type]; otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for IsValidType
        private static bool IsValidType(Type type)
        {
            return type == typeof(List<Answer>)
                   || type == typeof(IList<Answer>)
                   || type == typeof(Answer[]);
        }
    }
}