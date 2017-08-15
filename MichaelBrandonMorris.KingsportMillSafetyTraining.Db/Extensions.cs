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
        public static IList<Answer> GetAnswers(
            this Slide slide,
            Func<Answer, object> orderByPredicate = null,
            Func<Answer, bool> wherePredicate = null)
        {
            return slide.Answers.OrderByWhere(orderByPredicate, wherePredicate);
        }

        /// <summary>
        ///     Gets the categories.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;Category&gt;.</returns>
        /// TODO Edit XML Comment Template for GetCategories
        public static IList<Category> GetCategories(
            this Group group,
            Func<Category, object> orderByPredicate = null,
            Func<Category, bool> wherePredicate = null)
        {
            return group.Categories.OrderByWhere(
                orderByPredicate,
                wherePredicate);
        }

        /// <summary>
        ///     Gets the roles.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>IList&lt;Group&gt;.</returns>
        /// TODO Edit XML Comment Template for GetGroups
        public static IList<Group> GetGroups(this Category category)
        {
            return category.Groups.ToList();
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
        ///     Gets the training results.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <returns>IList&lt;TrainingResult&gt;.</returns>
        /// TODO Edit XML Comment Template for GetTrainingResults
        public static IList<TrainingResult> GetTrainingResults(
            this IList<User> users)
        {
            return users.SelectMany(user => user.GetTrainingResults()).ToList();
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