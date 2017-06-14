using System;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity
{
    public class TrainingResult
    {
        public virtual ApplicationUser User
        {
            get;
            set;
        }

        public DateTime CompletionDateTime
        {
            get;
            set;
        } = DateTime.MaxValue;

        public int Id
        {
            get;
            set;
        }

        public int QuizAttemptsCount
        {
            get;
            set;
        }

        public virtual Role Role
        {
            get;
            set;
        }

        public TimeSpan TimeToComplete
        {
            get;
            set;
        }
    }
}