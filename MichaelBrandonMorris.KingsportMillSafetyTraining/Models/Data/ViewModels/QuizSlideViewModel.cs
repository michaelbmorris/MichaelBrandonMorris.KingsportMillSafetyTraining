using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.
    ViewModels
{
    internal enum QuestionState
    {
        Unanswered,
        Incorrect,
        Correct
    }

    public class QuizSlideViewModel
    {
        public QuizSlideViewModel()
        {
            QuestionState = QuestionState.Unanswered;
        }

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

        public IList<Answer> Answers { get; set; }

        public int CorrectAnswerIndex { get; set; }

        public int Id { get; set; }

        public string ImageDescription { get; set; }

        public string Question { get; set; }

        [Required(ErrorMessage = "Please select an answer.")]
        public int SelectedAnswerIndex { get; set; }

        public bool ShouldShowImageOnQuiz { get; set; }

        private QuestionState QuestionState { get; set; }

        internal void AnswerQuestion(int selectedAnswerIndex)
        {
            SelectedAnswerIndex = selectedAnswerIndex;

            QuestionState = CorrectAnswerIndex == SelectedAnswerIndex
                ? QuestionState.Correct
                : QuestionState.Incorrect;
        }

        public bool IsCorrect()
        {
            return QuestionState == QuestionState.Correct;
        }

        public bool IsIncorrect()
        {
            return QuestionState == QuestionState.Incorrect;
        }

        public bool IsUnanswered()
        {
            return QuestionState == QuestionState.Unanswered;
        }
    }
}