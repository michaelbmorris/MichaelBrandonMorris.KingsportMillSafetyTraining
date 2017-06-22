using System.Collections.Generic;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.User;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.Result
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