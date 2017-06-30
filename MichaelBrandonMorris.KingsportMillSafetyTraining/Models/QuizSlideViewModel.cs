using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Enum QuestionState
    /// </summary>
    /// TODO Edit XML Comment Template for QuestionState
    internal enum QuestionState
    {
        /// <summary>
        ///     The unanswered
        /// </summary>
        /// TODO Edit XML Comment Template for Unanswered
        Unanswered,

        /// <summary>
        ///     The incorrect
        /// </summary>
        /// TODO Edit XML Comment Template for Incorrect
        Incorrect,

        /// <summary>
        ///     The correct
        /// </summary>
        /// TODO Edit XML Comment Template for Correct
        Correct
    }

    /// <summary>
    ///     Class QuizSlideViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for QuizSlideViewModel
    public class QuizSlideViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="QuizSlideViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public QuizSlideViewModel()
        {
            QuestionState = QuestionState.Unanswered;
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="QuizSlideViewModel" /> class.
        /// </summary>
        /// <param name="slide">The slide.</param>
        /// TODO Edit XML Comment Template for #ctor
        public QuizSlideViewModel(Slide slide)
            : this()
        {
            Answers = slide.Answers;
            CorrectAnswerIndex = slide.CorrectAnswerIndex;
            Id = slide.Id;
            ImageDescription = slide.ImageDescription;
            Question = slide.Question;
            ShouldShowImageOnQuiz = slide.ShouldShowImageOnQuiz;
        }

        /// <summary>
        ///     Gets or sets the answers.
        /// </summary>
        /// <value>The answers.</value>
        /// TODO Edit XML Comment Template for Answers
        public IList<Answer> Answers
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the index of the correct answer.
        /// </summary>
        /// <value>The index of the correct answer.</value>
        /// TODO Edit XML Comment Template for CorrectAnswerIndex
        public int CorrectAnswerIndex
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
        ///     Gets or sets the image description.
        /// </summary>
        /// <value>The image description.</value>
        /// TODO Edit XML Comment Template for ImageDescription
        public string ImageDescription
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the question.
        /// </summary>
        /// <value>The question.</value>
        /// TODO Edit XML Comment Template for Question
        public string Question
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the index of the selected answer.
        /// </summary>
        /// <value>The index of the selected answer.</value>
        /// TODO Edit XML Comment Template for SelectedAnswerIndex
        [Required(ErrorMessage = "You must select an answer.")]
        public int SelectedAnswerIndex
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [should show
        ///     image on quiz].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [should show image on quiz];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for ShouldShowImageOnQuiz
        public bool ShouldShowImageOnQuiz
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the state of the question.
        /// </summary>
        /// <value>The state of the question.</value>
        /// TODO Edit XML Comment Template for QuestionState
        private QuestionState QuestionState
        {
            get;
            set;
        }

        /// <summary>
        ///     Determines whether this instance is correct.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is correct;
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for IsCorrect
        public bool IsCorrect()
        {
            return QuestionState == QuestionState.Correct;
        }

        /// <summary>
        ///     Determines whether this instance is incorrect.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is incorrect;
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for IsIncorrect
        public bool IsIncorrect()
        {
            return QuestionState == QuestionState.Incorrect;
        }

        /// <summary>
        ///     Determines whether this instance is unanswered.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is unanswered;
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for IsUnanswered
        public bool IsUnanswered()
        {
            return QuestionState == QuestionState.Unanswered;
        }

        /// <summary>
        ///     Answers the question.
        /// </summary>
        /// <param name="selectedAnswerIndex">
        ///     Index of the selected
        ///     answer.
        /// </param>
        /// TODO Edit XML Comment Template for AnswerQuestion
        internal void AnswerQuestion(int selectedAnswerIndex)
        {
            SelectedAnswerIndex = selectedAnswerIndex;

            QuestionState = CorrectAnswerIndex == SelectedAnswerIndex
                ? QuestionState.Correct
                : QuestionState.Incorrect;
        }
    }
}