﻿using System;
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
        /// <param name="role">The role.</param>
        /// <returns>GroupViewModel.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// TODO Edit XML Comment Template for AsViewModel
        public static GroupViewModel AsViewModel(this Group role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return new GroupViewModel(role);
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
                throw new ArgumentNullException(nameof(trainingResult));
            }

            return new TrainingResultViewModel(trainingResult);
        }

        /// <summary>
        ///     Ases the view model.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns>CompanyViewModel.</returns>
        /// <exception cref="ArgumentNullException">company</exception>
        /// TODO Edit XML Comment Template for AsViewModel
        public static CompanyViewModel AsViewModel(this Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            return new CompanyViewModel(company);
        }

        /// <summary>
        ///     Ases the view models.
        /// </summary>
        /// <param name="companies">The companies.</param>
        /// <returns>IList&lt;CompanyViewModel&gt;.</returns>
        /// TODO Edit XML Comment Template for AsViewModels
        public static IList<CompanyViewModel> AsViewModels(
            this IList<Company> companies)
        {
            return companies.Select(company => company.AsViewModel());
        }

        /// <summary>
        ///     Ases the view models.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <returns>IList&lt;GroupViewModel&gt;.</returns>
        /// TODO Edit XML Comment Template for AsViewModels
        public static IList<GroupViewModel> AsViewModels(
            this IList<Group> roles)
        {
            return roles.Select(x => new GroupViewModel(x));
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