﻿using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.MvcGrid.Models;
using Column =
    MichaelBrandonMorris.MvcGrid.Models.GridColumn<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.TrainingResultViewModel>;
using Grid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.TrainingResultViewModel>;
using RetrieveDataMethod =
    System.Func<MichaelBrandonMorris.MvcGrid.Models.GridContext,
        MichaelBrandonMorris.MvcGrid.Models.QueryResult<MichaelBrandonMorris.
            KingsportMillSafetyTraining.Models.TrainingResultViewModel>>;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    /// <summary>
    ///     Class TrainingResultsGrid.
    /// </summary>
    internal static class TrainingResultsGrid
    {
        /// <summary>
        ///     Gets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        /// TODO Edit XML Comment Template for CompanyName
        private static Column CompanyName => new Column
        {
            ColumnName = "CompanyName",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Company",
            ValueExpression = (x, y) => x.CompanyName
        };

        /// <summary>
        ///     Gets the completion date time.
        /// </summary>
        /// <value>The completion date time.</value>
        /// TODO Edit XML Comment Template for CompletionDateTime
        private static Column CompletionDateTime => new Column
        {
            ColumnName = "CompletionDateTime",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Completed On",
            ValueExpression = (x, y) => x.CompletionDateTime.ToString()
        };

        /// <summary>
        ///     Gets the details.
        /// </summary>
        /// <value>The details.</value>
        /// TODO Edit XML Comment Template for Details
        private static Column Details => new Column
        {
            ColumnName = "Details",
            EnableFiltering = false,
            EnableSorting = false,
            HeaderText = string.Empty,
            HtmlEncode = false,
            ValueExpression = (x, y) => y.UrlHelper.Action(
                "Details",
                "Results",
                new
                {
                    id = x.Id
                }),
            ValueTemplate = MvcGridConfig.DetailsValueTemplate
        };


        /// <summary>
        ///     Gets the email.
        /// </summary>
        /// <value>The email.</value>
        /// TODO Edit XML Comment Template for Email
        private static Column Email => new Column
        {
            ColumnName = "Email",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Email",
            ValueExpression = (x, y) => x.Email
        };

        /// <summary>
        ///     Gets the first name.
        /// </summary>
        /// <value>The first name.</value>
        /// TODO Edit XML Comment Template for FirstName
        private static Column FirstName => new Column
        {
            ColumnName = "FirstName",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "First Name",
            ValueExpression = (x, y) => x.FirstName
        };

        /// <summary>
        ///     Gets the last name.
        /// </summary>
        /// <value>The last name.</value>
        /// TODO Edit XML Comment Template for LastName
        private static Column LastName => new Column
        {
            ColumnName = "LastName",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Last Name",
            ValueExpression = (x, y) => x.LastName
        };

        /// <summary>
        ///     Gets the quiz attempts count.
        /// </summary>
        /// <value>The quiz attempts count.</value>
        /// TODO Edit XML Comment Template for QuizAttemptsCount
        private static Column QuizAttemptsCount => new Column
        {
            ColumnName = "QuizAttemptsCount",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Number of Quiz Attempts",
            ValueExpression = (x, y) => x.QuizAttemptsCount.ToString()
        };

        /// <summary>
        ///     Gets the retrieve data method.
        /// </summary>
        /// <value>The retrieve data method.</value>
        /// TODO Edit XML Comment Template for RetrieveDataMethod
        private static RetrieveDataMethod RetrieveDataMethod => context =>
        {
            var sortColumnName = context.QueryOptions.SortColumnName;
            var sortDirection = context.QueryOptions.SortDirection;
            var id = context.QueryOptions.GetPageParameterString("id");
            var result = new QueryResult<TrainingResultViewModel>();

            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                var query = (id.IsNullOrWhiteSpace()
                    ? db.GetTrainingResultsDescending()
                    : db.GetTrainingResultsDescending(id)).AsViewModels();

                if (!sortColumnName.IsNullOrWhiteSpace())
                {
                    query = query.OrderBy(
                        x => x.GetPropertyValue(sortColumnName),
                        sortDirection);
                }

                result.Items = query.ToList();
            }

            return result;
        };

        /// <summary>
        ///     Gets the role title.
        /// </summary>
        /// <value>The role title.</value>
        /// TODO Edit XML Comment Template for RoleTitle
        private static Column RoleTitle => new Column
        {
            ColumnName = "RoleTitle",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Group",
            ValueExpression = (x, y) => x.RoleTitle
        };

        /// <summary>
        ///     Gets the time to complete.
        /// </summary>
        /// <value>The time to complete.</value>
        private static Column TimeToComplete => new Column
        {
            ColumnName = "TimeToComplete",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Time to Complete",
            ValueExpression =
                (x, y) => $"{x.TimeToComplete.TotalMinutes:#.##} Minutes"
        };

        internal static (string Title, Grid Grid) GetTrainingResultsGrid()
        {
            var grid = new Grid();
            grid.WithAuthorizationType(AuthorizationType.Authorized);
            grid.AddColumn(FirstName);
            grid.AddColumn(LastName);
            grid.AddColumn(CompanyName);
            grid.AddColumn(Email);
            grid.AddColumn(RoleTitle);
            grid.AddColumn(CompletionDateTime);
            grid.AddColumn(TimeToComplete);
            grid.AddColumn(Details);
            grid.WithSorting(true, "CompletionDateTime", SortDirection.Dsc);
            grid.WithRetrieveDataMethod(RetrieveDataMethod);
            return ("TrainingResultsGrid", grid);
        }

        internal static (string Title, Grid Grid) GetUserTrainingResultsGrid()
        {
            var grid = new Grid();
            grid.WithAuthorizationType(AuthorizationType.Authorized);
            grid.WithPageParameterNames("Id");
            grid.AddColumn(RoleTitle);
            grid.AddColumn(CompletionDateTime);
            grid.AddColumn(TimeToComplete);
            grid.AddColumn(QuizAttemptsCount);
            grid.AddColumn(Details);
            grid.WithSorting(true, "CompletionDateTime", SortDirection.Dsc);
            grid.WithRetrieveDataMethod(RetrieveDataMethod);
            return ("UserTrainingResultsGrid", grid);
        }
    }
}