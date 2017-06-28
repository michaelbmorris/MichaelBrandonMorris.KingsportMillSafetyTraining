using System;
using System.Collections.Generic;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    ///     Extensions for models.
    /// </summary>
    public static class Extensions
    {
        /// <summary>Gets the slides as quiz view models.</summary>
        /// <param name="slides">The slides.</param>
        /// <returns>The slides as quiz view models.</returns>
        public static IList<QuizSlideViewModel> AsQuizSlideViewModels(
            this IList<Slide> slides)
        {
            return slides.Select(x => new QuizSlideViewModel(x));
        }

        /// <summary>Gets the slide as a view model, with an optional list of categories for category selection.</summary>
        /// <param name="slide">The slide.</param>
        /// <param name="categories">The categories.</param>
        /// <returns>The slide as a view model.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the slide is null.</exception>
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

        /// <summary>Gets the user as a view model..</summary>
        /// <param name="user">The user.</param>
        /// <returns>The user as a view model.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the user is null.</exception>
        public static UserViewModel AsViewModel(this User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return new UserViewModel(user);
        }

        /// <summary>Gets the role as a view model.</summary>
        /// <param name="role">The role.</param>
        /// <returns>The role as a view model.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the role is null.</exception>
        public static RoleViewModel AsViewModel(this Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException();
            }

            return new RoleViewModel(role);
        }

        /// <summary>Gets the category as a view model.</summary>
        /// <param name="category">The category.</param>
        /// <returns>The category as a view model.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the category is null.</exception>
        public static CategoryViewModel AsViewModel(this Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }

            return new CategoryViewModel(category);
        }

        /// <summary>Gets the training result as a view model.</summary>
        /// <param name="trainingResult">The training result.</param>
        /// <returns>The training result as a view model.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the training result is null.</exception>
        public static TrainingResultViewModel AsViewModel(
            this TrainingResult trainingResult)
        {
            if (trainingResult == null)
            {
                throw new ArgumentNullException();
            }

            return new TrainingResultViewModel(trainingResult);
        }

        /// <summary>Gets the users as view models.</summary>
        /// <param name="users">The users.</param>
        /// <returns>The users as view models.</returns>
        public static IList<UserViewModel> AsViewModels(this IList<User> users)
        {
            return users.Select(x => new UserViewModel(x));
        }

        /// <summary>Gets the slides as view models.</summary>
        /// <param name="slides">The slides.</param>
        /// <returns>The slides as view models.</returns>
        public static IList<SlideViewModel> AsViewModels(
            this IList<Slide> slides)
        {
            return slides.Select(x => new SlideViewModel(x));
        }

        /// <summary>Gets the roles as view models.</summary>
        /// <param name="roles">The roles.</param>
        /// <returns>The roles as view models.</returns>
        public static IList<RoleViewModel> AsViewModels(this IList<Role> roles)
        {
            return roles.Select(x => new RoleViewModel(x));
        }

        /// <summary>Gets the categories as view models.</summary>
        /// <param name="categories">The categories.</param>
        /// <returns>The categories as view models.</returns>
        public static IList<CategoryViewModel> AsViewModels(
            this IList<Category> categories)
        {
            return categories.Select(x => new CategoryViewModel(x));
        }

        /// <summary>Gets the training results as view models.</summary>
        /// <param name="trainingResults">The training results.</param>
        /// <returns>The training results as view models.</returns>
        public static IList<TrainingResultViewModel> AsViewModels(
            this IList<TrainingResult> trainingResults)
        {
            return trainingResults.Select(x => new TrainingResultViewModel(x));
        }
    }
}