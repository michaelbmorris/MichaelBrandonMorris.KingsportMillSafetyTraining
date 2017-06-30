using System;
using System.Collections.Generic;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Class Extensions.
    /// </summary>
    /// TODO Edit XML Comment Template for Extensions
    public static class Extensions
    {
        /// <summary>
        ///     Ases the quiz slide view models.
        /// </summary>
        /// <param name="slides">The slides.</param>
        /// <returns>IList&lt;QuizSlideViewModel&gt;.</returns>
        /// TODO Edit XML Comment Template for AsQuizSlideViewModels
        public static IList<QuizSlideViewModel> AsQuizSlideViewModels(
            this IList<Slide> slides)
        {
            return slides.Select(x => new QuizSlideViewModel(x));
        }

        /// <summary>
        ///     Ases the view model.
        /// </summary>
        /// <param name="slide">The slide.</param>
        /// <param name="categories">The categories.</param>
        /// <returns>SlideViewModel.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// TODO Edit XML Comment Template for AsViewModel
        public static SlideViewModel AsViewModel(
            this Slide slide,
            IList<Category> categories = null)
        {
            if (slide == null)
            {
                throw new ArgumentNullException();
            }

            return new SlideViewModel(slide, categories);
        }

        /// <summary>
        ///     Ases the view model.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>UserViewModel.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// TODO Edit XML Comment Template for AsViewModel
        public static UserViewModel AsViewModel(this User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return new UserViewModel(user);
        }

        /// <summary>
        ///     Ases the view model.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>RoleViewModel.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// TODO Edit XML Comment Template for AsViewModel
        public static RoleViewModel AsViewModel(this Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException();
            }

            return new RoleViewModel(role);
        }

        /// <summary>
        ///     Ases the view model.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>CategoryViewModel.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// TODO Edit XML Comment Template for AsViewModel
        public static CategoryViewModel AsViewModel(this Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }

            return new CategoryViewModel(category);
        }

        /// <summary>
        ///     Ases the view model.
        /// </summary>
        /// <param name="trainingResult">The training result.</param>
        /// <returns>TrainingResultViewModel.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// TODO Edit XML Comment Template for AsViewModel
        public static TrainingResultViewModel AsViewModel(
            this TrainingResult trainingResult)
        {
            if (trainingResult == null)
            {
                throw new ArgumentNullException();
            }

            return new TrainingResultViewModel(trainingResult);
        }

        /// <summary>
        ///     Ases the view models.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <returns>IList&lt;UserViewModel&gt;.</returns>
        /// TODO Edit XML Comment Template for AsViewModels
        public static IList<UserViewModel> AsViewModels(this IList<User> users)
        {
            return users.Select(x => new UserViewModel(x));
        }

        /// <summary>
        ///     Ases the view models.
        /// </summary>
        /// <param name="slides">The slides.</param>
        /// <returns>IList&lt;SlideViewModel&gt;.</returns>
        /// TODO Edit XML Comment Template for AsViewModels
        public static IList<SlideViewModel> AsViewModels(
            this IList<Slide> slides)
        {
            return slides.Select(x => new SlideViewModel(x));
        }

        /// <summary>
        ///     Ases the view models.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <returns>IList&lt;RoleViewModel&gt;.</returns>
        /// TODO Edit XML Comment Template for AsViewModels
        public static IList<RoleViewModel> AsViewModels(this IList<Role> roles)
        {
            return roles.Select(x => new RoleViewModel(x));
        }

        /// <summary>
        ///     Ases the view models.
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// <returns>IList&lt;CategoryViewModel&gt;.</returns>
        /// TODO Edit XML Comment Template for AsViewModels
        public static IList<CategoryViewModel> AsViewModels(
            this IList<Category> categories)
        {
            return categories.Select(x => new CategoryViewModel(x));
        }

        /// <summary>
        ///     Ases the view models.
        /// </summary>
        /// <param name="trainingResults">The training results.</param>
        /// <returns>IList&lt;TrainingResultViewModel&gt;.</returns>
        /// TODO Edit XML Comment Template for AsViewModels
        public static IList<TrainingResultViewModel> AsViewModels(
            this IList<TrainingResult> trainingResults)
        {
            return trainingResults.Select(x => new TrainingResultViewModel(x));
        }
    }
}