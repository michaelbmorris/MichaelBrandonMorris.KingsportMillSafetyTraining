using System.Collections.Generic;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class UserTrainingResultsViewModel.
    /// </summary>
    /// TODO Edit XML Comment Template for UserTrainingResultsViewModel
    public class UserTrainingResultsViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="UserTrainingResultsViewModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public UserTrainingResultsViewModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="UserTrainingResultsViewModel" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="trainingResults">The training results.</param>
        /// TODO Edit XML Comment Template for #ctor
        public UserTrainingResultsViewModel(
            UserViewModel user,
            IList<TrainingResultViewModel> trainingResults)
        {
            User = user;
            TrainingResults = trainingResults;
        }

        /// <summary>
        ///     Gets or sets the training results.
        /// </summary>
        /// <value>The training results.</value>
        /// TODO Edit XML Comment Template for TrainingResults
        public IList<TrainingResultViewModel> TrainingResults
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        /// TODO Edit XML Comment Template for User
        public UserViewModel User
        {
            get;
            set;
        }
    }
}