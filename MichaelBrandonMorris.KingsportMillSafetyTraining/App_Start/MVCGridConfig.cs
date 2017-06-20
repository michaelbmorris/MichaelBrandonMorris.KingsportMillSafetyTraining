using System;
using System.Collections.Generic;
using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.Result;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.User;
using MichaelBrandonMorris.MvcGrid.Models;
using MichaelBrandonMorris.MvcGrid.Web;
using WebActivatorEx;
using TrainingResultGrid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.Identity.ViewModels.Result.
        TrainingResultViewModel>;
using UserGrid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.Identity.ViewModels.User.
        UserViewModel>;

[assembly: PreApplicationStartMethod(typeof(MvcGridConfig), "RegisterGrids")]

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    public static class MvcGridConfig
    {
        private static readonly GridColumn<TrainingResultViewModel>
            TrainingResultCompanyNameColumn =
                new GridColumn<TrainingResultViewModel>
                {
                    ColumnName = "CompanyName",
                    EnableFiltering = true,
                    EnableSorting = true,
                    HeaderText = "Company",
                    ValueExpression = (x, y) => x.CompanyName
                };

        private static readonly GridColumn<TrainingResultViewModel>
            TrainingResultCompletionDateTimeColumn =
                new GridColumn<TrainingResultViewModel>
                {
                    ColumnName = "CompletionDateTime",
                    EnableFiltering = true,
                    EnableSorting = true,
                    HeaderText = "Completed On",
                    ValueExpression = (x, y) => x.CompletionDateTime.ToString()
                };

        private static readonly GridColumn<TrainingResultViewModel>
            TrainingResultDetailsColumn =
                new GridColumn<TrainingResultViewModel>
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
                    ValueTemplate =
                        "<a href='{Value}' class='btn btn-primary' role='button'>Details</a>"
                };


        private static readonly GridColumn<TrainingResultViewModel>
            TrainingResultEmailColumn =
                new GridColumn<TrainingResultViewModel>
                {
                    ColumnName = "Email",
                    EnableFiltering = true,
                    EnableSorting = true,
                    HeaderText = "Email",
                    ValueExpression = (x, y) => x.Email
                };

        private static readonly GridColumn<TrainingResultViewModel>
            TrainingResultFirstNameColumn =
                new GridColumn<TrainingResultViewModel>
                {
                    ColumnName = "FirstName",
                    EnableFiltering = true,
                    EnableSorting = true,
                    HeaderText = "First Name",
                    ValueExpression = (x, y) => x.FirstName
                };

        private static readonly GridColumn<TrainingResultViewModel>
            TrainingResultLastNameColumn =
                new GridColumn<TrainingResultViewModel>
                {
                    ColumnName = "LastName",
                    EnableFiltering = true,
                    EnableSorting = true,
                    HeaderText = "Last Name",
                    ValueExpression = (x, y) => x.LastName
                };

        private static readonly GridColumn<TrainingResultViewModel>
            TrainingResultQuizAttemptsCountColumn =
                new GridColumn<TrainingResultViewModel>
                {
                    ColumnName = "QuizAttemptsCount",
                    EnableFiltering = true,
                    EnableSorting = true,
                    HeaderText = "Number of Quiz Attempts",
                    ValueExpression = (x, y) => x.QuizAttemptsCount.ToString()
                };

        private static readonly GridColumn<TrainingResultViewModel>
            TrainingResultRoleTitleColumn =
                new GridColumn<TrainingResultViewModel>
                {
                    ColumnName = "RoleTitle",
                    EnableFiltering = true,
                    EnableSorting = true,
                    HeaderText = "Role",
                    ValueExpression = (x, y) => x.RoleTitle
                };

        private static readonly GridColumn<TrainingResultViewModel>
            TrainingResultTimeToCompleteColumn =
                new GridColumn<TrainingResultViewModel>
                {
                    ColumnName = "TimeToComplete",
                    EnableFiltering = true,
                    EnableSorting = true,
                    HeaderText = "Time to Complete",
                    ValueExpression =
                        (x, y) =>
                            $"{x.TimeToComplete.TotalMinutes:#.##} Minutes"
                };

        private static readonly GridColumn<UserViewModel> UserBirthDateColumn =
            new GridColumn<UserViewModel>
            {
                ColumnName = "BirthDate",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Birth Date",
                ValueExpression = (x, y) => x.BirthDate.ToString("M/d/yy")
            };

        private static readonly GridColumn<UserViewModel> UserCompanyNameColumn
            = new GridColumn<UserViewModel>
            {
                ColumnName = "CompanyName",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Company",
                ValueExpression = (x, y) => x.CompanyName
            };

        private static readonly GridColumn<UserViewModel> UserDeleteColumn =
            new GridColumn<UserViewModel>
            {
                ColumnName = "Delete",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = string.Empty,
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Delete",
                    "Users",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-danger' role='button'>Delete</a>"
            };

        private static readonly GridColumn<UserViewModel> UserDetailsColumn =
            new GridColumn<UserViewModel>
            {
                ColumnName = "Details",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = string.Empty,
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Details",
                    "Users",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-primary' role='button'>Details</a>"
            };

        private static readonly GridColumn<UserViewModel> UserEditColumn =
            new GridColumn<UserViewModel>
            {
                ColumnName = "Edit",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = string.Empty,
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Edit",
                    "Users",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-primary' role='button'>Edit</a>"
            };

        private static readonly GridColumn<UserViewModel> UserEmailColumn =
            new GridColumn<UserViewModel>
            {
                ColumnName = "Email",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Email",
                ValueExpression = (x, y) => x.Email
            };

        private static readonly GridColumn<UserViewModel> UserFirstNameColumn =
            new GridColumn<UserViewModel>
            {
                ColumnName = "FirstName",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "First Name",
                ValueExpression = (x, y) => x.FirstName
            };

        private static readonly GridColumn<UserViewModel> UserLastNameColumn =
            new GridColumn<UserViewModel>
            {
                ColumnName = "LastName",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Last Name",
                ValueExpression = (x, y) => x.LastName
            };

        private static readonly GridColumn<UserViewModel>
            UserLastTrainingDateTimeColumn = new GridColumn<UserViewModel>
            {
                ColumnName = "LastTrainingResultDateTime",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Last Training Completed On",
                ValueExpression =
                    (x, y) => x.LastTrainingResultDateTime?.ToString("M/d/yy")
                              ?? "Training not completed."
            };

        private static readonly GridColumn<UserViewModel> UserMiddleNameColumn =
            new GridColumn<UserViewModel>
            {
                ColumnName = "MiddleName",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Middle Name",
                ValueExpression = (x, y) => x.MiddleName
            };

        private static readonly GridColumn<UserViewModel> UserPhoneNumberColumn
            = new GridColumn<UserViewModel>
            {
                ColumnName = "PhoneNumber",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Phone Number",
                ValueExpression = (x, y) => x.PhoneNumber
            };

        private static readonly GridColumn<UserViewModel> UserResultsColumn =
            new GridColumn<UserViewModel>
            {
                ColumnName = "Results",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = string.Empty,
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "UserResults",
                    "Results",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-primary' role='button'>Results</a>"
            };


        public static void RegisterGrids()
        {
            var trainingResultsGrid = GetTrainingResultsGrid();
            var userTrainingResultsGrid = GetUserTrainingResultsGrid();
            var usersGrid = GetUsersGrid();

            MvcGridDefinitionTable.Add(
                trainingResultsGrid.Title,
                trainingResultsGrid.Grid);

            MvcGridDefinitionTable.Add(
                userTrainingResultsGrid.Title,
                userTrainingResultsGrid.Grid);

            MvcGridDefinitionTable.Add(usersGrid.Title, usersGrid.Grid);
        }

        private static object GetPropertyValue(object o, string propertyName)
        {
            var propertyInfo = o.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new Exception();
            }

            return propertyInfo.GetValue(o, null);
        }

        private static (string Title, MvcGridBuilder<TrainingResultViewModel>
            Grid) GetTrainingResultsGrid()
        {
            const string title = "TrainingResultsGrid";

            var columnDefaults = new ColumnDefaults
            {
                EnableSorting = true
            };

            var grid =
                new MvcGridBuilder<TrainingResultViewModel>(columnDefaults);

            grid.WithAuthorizationType(AuthorizationType.Authorized);
            grid.AddColumn(TrainingResultFirstNameColumn);
            grid.AddColumn(TrainingResultLastNameColumn);
            grid.AddColumn(TrainingResultCompanyNameColumn);
            grid.AddColumn(TrainingResultEmailColumn);
            grid.AddColumn(TrainingResultRoleTitleColumn);
            grid.AddColumn(TrainingResultCompletionDateTimeColumn);
            grid.AddColumn(TrainingResultTimeToCompleteColumn);

            grid.AddColumns(
                    columns =>
                    {
                        columns.Add("Details")
                            .WithSorting(false)
                            .WithHtmlEncoding(false)
                            .WithValueExpression(
                                (i, c) => c.UrlHelper.Action(
                                    "Details",
                                    "Results",
                                    new
                                    {
                                        id = i.Id
                                    }))
                            .WithValueTemplate(
                                "<a href='{Value}' class='btn btn-primary' role='button'>Details</a>");
                    })
                .WithSorting(true, "CompletionDateTime", SortDirection.Dsc);

            grid.WithRetrieveDataMethod(
                context =>
                {
                    var sortColumnName = context.QueryOptions.SortColumnName;
                    var sortDirection = context.QueryOptions.SortDirection;
                    var result = new QueryResult<TrainingResultViewModel>();

                    using (var db = new ApplicationDbContext())
                    {
                        var query = db.GetTrainingResultViewModels();

                        if (!sortColumnName.IsNullOrWhiteSpace())
                        {
                            query = query.OrderBy(
                                x => GetPropertyValue(x, sortColumnName),
                                sortDirection);
                        }

                        result.Items = query.ToList();
                    }

                    return result;
                });

            return (title, grid);
        }

        private static (string Title, UserGrid Grid) GetUsersGrid()
        {
            const string title = "UsersGrid";
            var grid = new UserGrid(
                    new ColumnDefaults
                    {
                        EnableFiltering = true,
                        EnableSorting = true
                    }).WithAuthorizationType(AuthorizationType.Authorized)
                .AddColumns(
                    columns =>
                    {
                        columns.Add(UserFirstNameColumn);
                        columns.Add(UserMiddleNameColumn);
                        columns.Add(UserLastNameColumn);
                        columns.Add(UserCompanyNameColumn);
                        columns.Add(UserEmailColumn);
                        columns.Add(UserPhoneNumberColumn);
                        columns.Add(UserBirthDateColumn);
                        columns.Add(UserLastTrainingDateTimeColumn);
                        columns.Add(UserEditColumn);
                        columns.Add(UserDetailsColumn);
                        columns.Add(UserResultsColumn);
                        columns.Add(UserDeleteColumn);
                    })
                .WithSorting(true, "LastName", SortDirection.Asc)
                .WithRetrieveDataMethod(
                    context =>
                    {
                        var sortColumnName =
                            context.QueryOptions.SortColumnName;
                        var sortDirection = context.QueryOptions.SortDirection;
                        var result = new QueryResult<UserViewModel>();

                        using (var db = new ApplicationDbContext())
                        {
                            var query = db.Users.ToList().AsViewModels();

                            if (!sortColumnName.IsNullOrWhiteSpace())
                            {
                                result.Items = query.OrderBy(
                                    x => GetPropertyValue(x, sortColumnName),
                                    sortDirection);
                            }
                            else
                            {
                                result.Items = query.ToList();
                            }
                        }

                        return result;
                    });

            return (title, grid);
        }

        private static (string Title, TrainingResultGrid Grid)
            GetUserTrainingResultsGrid()
        {
            const string title = "UserTrainingResultsGrid";

            var grid = new TrainingResultGrid(
                    new ColumnDefaults
                    {
                        EnableFiltering = true,
                        EnableSorting = true
                    }).WithAuthorizationType(AuthorizationType.Authorized)
                    .WithPageParameterNames("Id")
                .AddColumns(
                    columns =>
                    {
                        columns.Add(TrainingResultRoleTitleColumn);
                        columns.Add(TrainingResultCompletionDateTimeColumn);
                        columns.Add(TrainingResultTimeToCompleteColumn);
                        columns.Add(TrainingResultQuizAttemptsCountColumn);
                        columns.Add(TrainingResultDetailsColumn);
                    })
                .WithSorting(true, "CompletionDateTime", SortDirection.Dsc)
                .WithRetrieveDataMethod(
                    context =>
                    {
                        var sortColumnName =
                            context.QueryOptions.SortColumnName;
                        var sortDirection = context.QueryOptions.SortDirection;

                        var id =
                            context.QueryOptions.GetPageParameterString("id");

                        var result = new QueryResult<TrainingResultViewModel>();

                        using (var db = new ApplicationDbContext())
                        {
                            var query = db.GetTrainingResultViewModels(id);

                            if (!sortColumnName.IsNullOrWhiteSpace())
                            {
                                query = query.OrderBy(
                                    x => GetPropertyValue(x, sortColumnName),
                                    sortDirection);
                            }

                            result.Items = query.ToList();
                        }

                        return result;
                    });

            return (title, grid);
        }

        private static IList<T> OrderBy<T>(
            this IEnumerable<T> enumerable,
            Func<T, object> sortBy,
            SortDirection sortDirection)
        {
            switch (sortDirection)
            {
                case SortDirection.Unspecified: return enumerable.ToList();
                case SortDirection.Asc:
                    return enumerable.OrderBy(sortBy).ToList();
                case SortDirection.Dsc:
                    return enumerable.OrderByDescending(sortBy).ToList();
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(sortDirection),
                        sortDirection,
                        null);
            }
        }
    }
}