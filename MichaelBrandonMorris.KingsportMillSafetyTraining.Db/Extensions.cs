using System;
using System.Collections.Generic;
using System.Linq;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db
{
    /// <summary>
    ///     Class Extensions.
    /// </summary>
    /// TODO Edit XML Comment Template for Extensions
    public static class Extensions
    {
        /// <summary>
        ///     Answerses the specified order by predicate.
        /// </summary>
        /// <param name="slide">The slide.</param>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;Answer&gt;.</returns>
        /// TODO Edit XML Comment Template for Answers
        public static IList<Answer> Answers(
            this Slide slide,
            Func<Answer, object> orderByPredicate = null,
            Func<Answer, bool> wherePredicate = null)
        {
            return slide.Answers.OrderByWhere(orderByPredicate, wherePredicate);
        }

        /// <summary>
        ///     Gets the categories.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;Category&gt;.</returns>
        /// TODO Edit XML Comment Template for GetCategories
        public static IList<Category> GetCategories(
            this Role role,
            Func<Category, object> orderByPredicate = null,
            Func<Category, bool> wherePredicate = null)
        {
            return role.Categories.OrderByWhere(
                orderByPredicate,
                wherePredicate);
        }

        /// <summary>
        ///     Gets the quiz results.
        /// </summary>
        /// <param name="trainingResult">The training result.</param>
        /// <returns>IList&lt;QuizResult&gt;.</returns>
        /// TODO Edit XML Comment Template for GetQuizResults
        public static IList<QuizResult> GetQuizResults(
            this TrainingResult trainingResult)
        {
            return trainingResult.QuizResults.ToList();
        }

        /// <summary>
        ///     Gets the roles.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>IList&lt;Role&gt;.</returns>
        /// TODO Edit XML Comment Template for GetRoles
        public static IList<Role> GetRoles(this Category category)
        {
            return category.Roles;
        }

        /// <summary>
        ///     Gets the slides.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;Slide&gt;.</returns>
        /// TODO Edit XML Comment Template for GetSlides
        public static IList<Slide> GetSlides(
            this Category category,
            Func<Slide, object> orderByPredicate = null,
            Func<Slide, bool> wherePredicate = null)
        {
            return category.Slides.OrderByWhere(
                orderByPredicate,
                wherePredicate);
        }

        /// <summary>
        ///     Gets the slides.
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;Slide&gt;.</returns>
        /// TODO Edit XML Comment Template for GetSlides
        public static IList<Slide> GetSlides(
            this IList<Category> categories,
            Func<Slide, object> orderByPredicate = null,
            Func<Slide, bool> wherePredicate = null)
        {
            return categories.SelectMany(
                category => category.GetSlides(
                    orderByPredicate,
                    wherePredicate));
        }

        /// <summary>
        ///     Gets the slides.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="orderCategoriesBy">The order categories by.</param>
        /// <param name="categoriesWhere">The categories where.</param>
        /// <param name="orderSlidesBy">The order slides by.</param>
        /// <param name="slidesWhere">The slides where.</param>
        /// <returns>IList&lt;Slide&gt;.</returns>
        /// TODO Edit XML Comment Template for GetSlides
        public static IList<Slide> GetSlides(
            this Role role,
            Func<Category, object> orderCategoriesBy = null,
            Func<Category, bool> categoriesWhere = null,
            Func<Slide, object> orderSlidesBy = null,
            Func<Slide, bool> slidesWhere = null)
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                return db.DoTransaction(
                    () => role.GetCategories(orderCategoriesBy, categoriesWhere)
                        .GetSlides(orderSlidesBy, slidesWhere));
            }
        }

        /// <summary>
        ///     Gets the training results.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;TrainingResult&gt;.</returns>
        /// TODO Edit XML Comment Template for GetTrainingResults
        public static IList<TrainingResult> GetTrainingResults(
            this User user,
            Func<TrainingResult, object> orderByPredicate = null,
            Func<TrainingResult, bool> wherePredicate = null)
        {
            return user.TrainingResults.OrderByWhere(
                orderByPredicate,
                wherePredicate);
        }

        /// <summary>
        ///     Gets the training results descending.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;TrainingResult&gt;.</returns>
        /// TODO Edit XML Comment Template for GetTrainingResultsDescending
        public static IList<TrainingResult> GetTrainingResultsDescending(
            this User user,
            Func<TrainingResult, object> orderByPredicate = null,
            Func<TrainingResult, bool> wherePredicate = null)
        {
            return user.TrainingResults.OrderByDescendingWhere(
                orderByPredicate,
                wherePredicate);
        }
    }
}