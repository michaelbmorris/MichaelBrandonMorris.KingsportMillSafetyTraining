using System;
using System.Collections.Generic;
using System.Linq;
using Foolproof;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Validation
{
    public class AnswersValidAttribute : ModelAwareValidationAttribute
    {
        public override bool IsValid(object value, object container)
        {
            var list = value as IList<Answer>;

            if (list == null)
            {
                throw new ArgumentException(
                    "Attribute must be applied to a list of answers.");
            }

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

            if(list.Any(answer => answer.Title.IsNullOrWhiteSpace()))
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
    }
}