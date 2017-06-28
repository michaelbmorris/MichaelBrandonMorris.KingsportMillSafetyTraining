using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class UserTrainingResultsViewModel
    {
        public UserTrainingResultsViewModel()
        {
        }

        public UserTrainingResultsViewModel(
            UserViewModel user,
            IList<TrainingResultViewModel> trainingResults)
        {
            User = user;
            TrainingResults = trainingResults;
        }

        public IList<TrainingResultViewModel> TrainingResults
        {
            get;
            set;
        }

        public UserViewModel User
        {
            get;
            set;
        }
    }
}