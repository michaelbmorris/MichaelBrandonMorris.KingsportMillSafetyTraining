using System;
using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity
{
    public class TrainingResult
    {
        public DateTime? CompletionDateTime { get; set; }

        public int Id { get; set; }

        public virtual IList<QuizResult> QuizResults { get; set; } = new List<QuizResult>();

        public virtual Role Role { get; set; }

        public TimeSpan TimeToComplete { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}