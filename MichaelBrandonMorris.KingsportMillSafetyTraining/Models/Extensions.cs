using System;
using System.Collections.Generic;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.Result;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.User;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public static class Extensions
    {
        public static IList<QuizSlideViewModel> AsQuizSlideViewModels(
            this IList<Slide> slides)
        {
            return slides.Select(x => new QuizSlideViewModel(x));
        }

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

        public static UserViewModel AsViewModel(this User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return new UserViewModel(user);
        }

        public static RoleViewModel AsViewModel(this Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException();
            }

            return new RoleViewModel(role);
        }

        public static CategoryViewModel AsViewModel(this Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }

            return new CategoryViewModel(category);
        }

        public static TrainingResultViewModel AsViewModel(
            this TrainingResult trainingResult)
        {
            if (trainingResult == null)
            {
                throw new ArgumentNullException();
            }

            return new TrainingResultViewModel(trainingResult);
        }

        public static IList<UserViewModel> AsViewModels(this IList<User> users)
        {
            return users.Select(x => new UserViewModel(x));
        }

        public static IList<SlideViewModel> AsViewModels(
            this IList<Slide> slides)
        {
            return slides.Select(x => new SlideViewModel(x));
        }

        public static IList<RoleViewModel> AsViewModels(this IList<Role> roles)
        {
            return roles.Select(x => new RoleViewModel(x));
        }

        public static IList<CategoryViewModel> AsViewModels(
            this IList<Category> categories)
        {
            return categories.Select(x => new CategoryViewModel(x));
        }

        public static IList<TrainingResultViewModel> AsViewModels(
            this IList<TrainingResult> trainingResults)
        {
            return trainingResults.Select(x => new TrainingResultViewModel(x));
        }
    }
}