using System;
using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.MvcGrid.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            ValueExpression =
                (trainingResultViewModel, gridContext) =>
                    trainingResultViewModel.CompanyName
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
            ValueExpression =
                (trainingResultViewModel, gridContext) =>
                    trainingResultViewModel.CompletionDateTime == null
                        ? "Training not completed."
                        : trainingResultViewModel.CompletionDateTime.ToString()
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
            PlainTextValueExpression = (trainingResultViewModel, gridContext) =>
            {
                var urlBuilder =
                    new UriBuilder(
                        gridContext.CurrentHttpContext.Request.Url
                            .AbsoluteUri)
                    {
                        Path = gridContext.UrlHelper.Action(
                            "Details",
                            "Results",
                            new
                            {
                                id = trainingResultViewModel.Id
                            }),
                        Query = null
                    };

                return urlBuilder.Uri.ToString();
            },
            ValueExpression =
                (trainingResultViewModel, gridContext) => gridContext.UrlHelper
                    .Action(
                        "Details",
                        "Results",
                        new
                        {
                            id = trainingResultViewModel.Id
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
            ValueExpression =
                (trainingResultViewModel, gridContext) =>
                    trainingResultViewModel.Email
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
            ValueExpression =
                (trainingResultViewModel, gridContext) =>
                    trainingResultViewModel.FirstName
        };

        /// <summary>
        ///     Gets the role title.
        /// </summary>
        /// <value>The role title.</value>
        /// TODO Edit XML Comment Template for GroupTitle
        private static Column GroupTitle => new Column
        {
            ColumnName = "GroupTitle",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Group",
            ValueExpression =
                (trainingResultViewModel, gridContext) =>
                    trainingResultViewModel.GroupTitle
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
            ValueExpression =
                (trainingResultViewModel, gridContext) =>
                    trainingResultViewModel.LastName
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
            ValueExpression =
                (trainingResultViewModel, gridContext) =>
                    trainingResultViewModel.QuizAttemptsCount.ToString()
        };

        private static RetrieveDataMethod RetrieveDataMethod => context =>
        {
            var options = context.QueryOptions;
            var sortColumnName = options.SortColumnName;
            var sortDirection = options.SortDirection;
            var id = options.GetPageParameterString("id");
            var result = new QueryResult<TrainingResultViewModel>();

            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                var currentUser = db.GetUser(id);

                var userManager =
                    new ApplicationUserManager(new UserStore<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>(db));

                var query = userManager.IsInRole(id, "Supervisor")
                    ? currentUser.Company.GetEmployees().GetTrainingResults().AsViewModels()
                    : db.GetTrainingResults().AsViewModels();

                if (!sortColumnName.IsNullOrWhiteSpace())
                {
                    query = query.OrderBy(
                        x => x.GetPropertyValue(sortColumnName),
                        sortDirection);
                }

                result.Items = query.ToList();

                var firstName = options.GetFilterString("FirstName");
                var lastName = options.GetFilterString("LastName");
                var companyName = options.GetFilterString("CompanyName");
                var groupTitle = options.GetFilterString("GroupTitle");
                var email = options.GetFilterString("Email");

                result.Items = result.Items.Where(
                    user => user.FirstName.ContainsFilter(firstName)
                            && user.LastName.ContainsFilter(lastName)
                            && user.CompanyName.ContainsFilter(companyName)
                            && user.GroupTitle.ContainsFilter(groupTitle)
                            && user.Email.ContainsFilter(email));
            }

            return result;
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
                (trainingResultViewModel, gridContext) =>
                    trainingResultViewModel.TimeToComplete == default(TimeSpan)
                        ? "Training not completed."
                        : $"{trainingResultViewModel.TimeToComplete.TotalMinutes:#.##} Minutes"
        };

        /// <summary>
        ///     Gets the retrieve data method.
        /// </summary>
        /// <value>The retrieve data method.</value>
        /// TODO Edit XML Comment Template for RetrieveDataMethod
        private static RetrieveDataMethod UserRetrieveDataMethod => context =>
        {
            var options = context.QueryOptions;
            var sortColumnName = options.SortColumnName;
            var sortDirection = options.SortDirection;
            var id = options.GetPageParameterString("id");
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

                var firstName = options.GetFilterString("FirstName");
                var lastName = options.GetFilterString("LastName");
                var companyName = options.GetFilterString("CompanyName");
                var groupTitle = options.GetFilterString("GroupTitle");
                var email = options.GetFilterString("Email");

                result.Items = result.Items.Where(
                    user => user.FirstName.ContainsFilter(firstName)
                            && user.LastName.ContainsFilter(lastName)
                            && user.CompanyName.ContainsFilter(companyName)
                            && user.GroupTitle.ContainsFilter(groupTitle)
                            && user.Email.ContainsFilter(email));
            }

            return result;
        };

        internal static (string Title, Grid Grid) GetTrainingResultsGrid()
        {
            var grid = new Grid();
            grid.WithAuthorizationType(AuthorizationType.Authorized);
            grid.WithPageParameterNames("Id");
            grid.AddColumn(FirstName);
            grid.AddColumn(LastName);
            grid.AddColumn(CompanyName);
            grid.AddColumn(Email);
            grid.AddColumn(GroupTitle);
            grid.AddColumn(CompletionDateTime);
            grid.AddColumn(TimeToComplete);
            grid.AddColumn(QuizAttemptsCount);
            grid.AddColumn(Details);
            grid.WithFiltering(true);
            grid.WithSorting(true, "CompletionDateTime", SortDirection.Dsc);
            grid.WithRetrieveDataMethod(RetrieveDataMethod);
            return ("TrainingResultsGrid", grid);
        }

        internal static (string Title, Grid Grid) GetUserTrainingResultsGrid()
        {
            var grid = new Grid();
            grid.WithAuthorizationType(AuthorizationType.Authorized);
            grid.WithPageParameterNames("Id");
            grid.AddColumn(GroupTitle);
            grid.AddColumn(CompletionDateTime);
            grid.AddColumn(TimeToComplete);
            grid.AddColumn(QuizAttemptsCount);
            grid.AddColumn(Details);
            grid.WithSorting(true, "CompletionDateTime", SortDirection.Dsc);
            grid.WithRetrieveDataMethod(UserRetrieveDataMethod);
            return ("UserTrainingResultsGrid", grid);
        }
    }
}