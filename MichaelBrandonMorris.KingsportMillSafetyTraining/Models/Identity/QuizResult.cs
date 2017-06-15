using System;
using System.ComponentModel;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity
{
    public class QuizResult
    {
        public string Score => TotalQuestions == 0
            ? 0.ToString("P")
            : (QuestionsCorrect / (float) TotalQuestions).ToString("P");

        public int Id { get; set; }

        [DisplayName("Questions Correct")]
        public int QuestionsCorrect { get; set; }

        [DisplayName("Time to Complete")]
        public TimeSpan TimeToComplete { get; set; }

        public string TimeToCompleteString => TimeToComplete.ToString(@"mm");

        [DisplayName("Total Questions")]
        public int TotalQuestions { get; set; }

        public virtual TrainingResult TrainingResult { get; set; }
    }
}