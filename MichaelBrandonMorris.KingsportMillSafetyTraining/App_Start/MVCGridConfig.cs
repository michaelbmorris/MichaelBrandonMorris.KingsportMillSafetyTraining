using System;
using System.Collections.Generic;
using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity;
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
        private static readonly GridColumn<CategoryViewModel>
            CategoryAssignRolesColumn = new GridColumn<CategoryViewModel>
            {
                ColumnName = "AssignRoles",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = string.Empty,
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "AssignRoles",
                    "Categories",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-primary' role='button'>Assign Roles</a>"
            };

        private static readonly GridColumn<CategoryViewModel>
            CategoryDeleteColumn = new GridColumn<CategoryViewModel>
            {
                ColumnName = "Delete",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = string.Empty,
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Delete",
                    "Categories",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-danger' role='button'>Delete</a>"
            };

        private static readonly GridColumn<CategoryViewModel>
            CategoryDetailsColumn = new GridColumn<CategoryViewModel>
            {
                ColumnName = "Details",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = string.Empty,
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Details",
                    "Categories",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-primary' role='button'>Details</a>"
            };

        private static readonly GridColumn<CategoryViewModel> CategoryEditColumn
            = new GridColumn<CategoryViewModel>
            {
                ColumnName = "Edit",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = string.Empty,
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Edit",
                    "Categories",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-primary' role='button'>Edit</a>"
            };

        private static readonly GridColumn<CategoryViewModel>
            CategoryIndexColumn = new GridColumn<CategoryViewModel>
            {
                ColumnName = "Index",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Index",
                ValueExpression = (x, y) => x.Index.ToString()
            };

        private static readonly GridColumn<CategoryViewModel>
            CategoryRolesCountColumn = new GridColumn<CategoryViewModel>
            {
                ColumnName = "RolesCount",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Number of Roles",
                ValueExpression = (x, y) => x.RolesCount.ToString()
            };

        private static readonly GridColumn<CategoryViewModel>
            CategorySlidesCountColumn = new GridColumn<CategoryViewModel>
            {
                ColumnName = "SlidesCount",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Number of Slides",
                ValueExpression = (x, y) => x.SlidesCount.ToString()
            };

        private static readonly GridColumn<CategoryViewModel>
            CategoryTitleColumn = new GridColumn<CategoryViewModel>
            {
                ColumnName = "Title",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Title",
                ValueExpression = (x, y) => x.Title
            };

        private static readonly GridColumn<QuizResult> QuizAttemptNumberColumn =
            new GridColumn<QuizResult>
            {
                ColumnName = "AttemptNumber",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Attempt Number",
                ValueExpression = (x, y) => x.AttemptNumber.ToString()
            };

        private static readonly GridColumn<QuizResult>
            QuizQuestionsCorrectColumn = new GridColumn<QuizResult>
            {
                ColumnName = "QuestionsCorrect",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Questions Correct",
                ValueExpression = (x, y) => x.QuestionsCorrect.ToString(),
                ValueTemplate = "{Value} / {Model.TotalQuestions}"
            };

        private static readonly GridColumn<QuizResult> QuizScoreColumn =
            new GridColumn<QuizResult>
            {
                ColumnName = "Score",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Score",
                ValueExpression = (x, y) => x.Score
            };

        private static readonly GridColumn<QuizResult> QuizTimeToCompleteColumn
            = new GridColumn<QuizResult>
            {
                ColumnName = "TimeToComplete",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Time to Complete",
                ValueExpression = (x, y) => x.TimeToCompleteString
            };

        private static readonly GridColumn<RoleViewModel>
            RoleAssignCategoriesColumn = new GridColumn<RoleViewModel>
            {
                ColumnName = "AssignCategories",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = "",
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "AssignCategories",
                    "Roles",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-primary' role='button'>Assign Categories</a>"
            };

        private static readonly GridColumn<RoleViewModel>
            RoleCategoriesCountColumn = new GridColumn<RoleViewModel>
            {
                ColumnName = "CategoriesCount",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Number of Categories",
                ValueExpression = (x, y) => x.CategoriesCount.ToString()
            };

        private static readonly GridColumn<RoleViewModel> RoleDeleteColumn =
            new GridColumn<RoleViewModel>
            {
                ColumnName = "Delete",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = "",
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Delete",
                    "Roles",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-danger' role='button'>Delete</a>"
            };

        private static readonly GridColumn<RoleViewModel> RoleDetailsColumn =
            new GridColumn<RoleViewModel>
            {
                ColumnName = "Details",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = "",
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Details",
                    "Roles",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-primary' role='button'>Details</a>"
            };

        private static readonly GridColumn<RoleViewModel> RoleEditColumn =
            new GridColumn<RoleViewModel>
            {
                ColumnName = "Edit",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = "",
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Edit",
                    "Roles",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-primary' role='button'>Edit</a>"
            };

        private static readonly GridColumn<RoleViewModel> RoleIndexColumn =
            new GridColumn<RoleViewModel>
            {
                ColumnName = "Index",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Index",
                ValueExpression = (x, y) => x.Index.ToString()
            };

        private static readonly GridColumn<RoleViewModel> RoleTitleColumn =
            new GridColumn<RoleViewModel>
            {
                ColumnName = "Title",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Title",
                ValueExpression = (x, y) => x.Title
            };

        private static readonly GridColumn<SlideViewModel> SlideDeleteColumn =
            new GridColumn<SlideViewModel>
            {
                ColumnName = "Delete",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = "",
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Delete",
                    "Slides",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-danger' role='button'>Delete</a>"
            };

        private static readonly GridColumn<SlideViewModel> SlideDetailsColumn =
            new GridColumn<SlideViewModel>
            {
                ColumnName = "Details",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = "",
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Details",
                    "Slides",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-primary' role='button'>Details</a>"
            };

        private static readonly GridColumn<SlideViewModel> SlideEditColumn =
            new GridColumn<SlideViewModel>
            {
                ColumnName = "Edit",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = "",
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "Edit",
                    "Slides",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<a href='{Value}' class='btn btn-primary' role='button'>Edit</a>"
            };

        private static readonly GridColumn<SlideViewModel> SlideImageColumn =
            new GridColumn<SlideViewModel>
            {
                ColumnName = "Image",
                EnableFiltering = false,
                EnableSorting = false,
                HeaderText = "Image",
                HtmlEncode = false,
                ValueExpression = (x, y) => y.UrlHelper.Action(
                    "RenderImage",
                    "Slides",
                    new
                    {
                        id = x.Id
                    }),
                ValueTemplate =
                    "<img src='{Value}' alt='{Model.ImageDescription}'>"
            };

        private static readonly GridColumn<SlideViewModel> SlideIndexColumn =
            new GridColumn<SlideViewModel>
            {
                ColumnName = "Index",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Index",
                ValueExpression = (x, y) => x.Index.ToString()
            };

        private static readonly GridColumn<SlideViewModel>
            SlideShouldShowImageOnQuizColumn = new GridColumn<SlideViewModel>
            {
                ColumnName = "ShouldShowImageOnQuiz",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Show Image on Quiz?",
                ValueExpression = (x, y) => x.ShouldShowImageOnQuiz.ToString()
            };

        private static readonly GridColumn<SlideViewModel>
            SlideShouldShowQuestionOnQuizColumn =
                new GridColumn<SlideViewModel>
                {
                    ColumnName = "ShouldShowQuestionOnQuiz",
                    EnableFiltering = true,
                    EnableSorting = true,
                    HeaderText = "Show Question on Quiz?",
                    ValueExpression =
                        (x, y) => x.ShouldShowQuestionOnQuiz.ToString()
                };

        private static readonly GridColumn<SlideViewModel>
            SlideShouldShowSlideInSlideshowColumn =
                new GridColumn<SlideViewModel>
                {
                    ColumnName = "ShouldShowSlideInSlideshow",
                    EnableFiltering = true,
                    EnableSorting = true,
                    HeaderText = "Show Slide in Slideshow?",
                    ValueExpression =
                        (x, y) => x.ShouldShowSlideInSlideshow.ToString()
                };

        private static readonly GridColumn<SlideViewModel> SlideTitleColumn =
            new GridColumn<SlideViewModel>
            {
                ColumnName = "Title",
                EnableFiltering = true,
                EnableSorting = true,
                HeaderText = "Title",
                ValueExpression = (x, y) => x.Title
            };

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
            var categoriesGrid = GetCategoriesGrid();
            var rolesGrid = GetRolesGrid();
            var slidesGrid = GetSlidesGrid();
            var quizResultsGrid = GetQuizResultsGrid();

            MvcGridDefinitionTable.Add(
                trainingResultsGrid.Title,
                trainingResultsGrid.Grid);

            MvcGridDefinitionTable.Add(
                userTrainingResultsGrid.Title,
                userTrainingResultsGrid.Grid);

            MvcGridDefinitionTable.Add(usersGrid.Title, usersGrid.Grid);

            MvcGridDefinitionTable.Add(
                categoriesGrid.Title,
                categoriesGrid.Grid);

            MvcGridDefinitionTable.Add(rolesGrid.Title, rolesGrid.Grid);
            MvcGridDefinitionTable.Add(slidesGrid.Title, slidesGrid.Grid);
            MvcGridDefinitionTable.Add(
                quizResultsGrid.Title,
                quizResultsGrid.Grid);
        }

        private static (string Title, MvcGridBuilder<CategoryViewModel> Grid)
            GetCategoriesGrid()
        {
            const string title = "CategoriesGrid";

            var grid = new MvcGridBuilder<CategoryViewModel>()
                .WithAuthorizationType(AuthorizationType.Authorized)
                .WithPageParameterNames("Id")
                .AddColumns(
                    columns =>
                    {
                        columns.Add(CategoryIndexColumn);
                        columns.Add(CategoryTitleColumn);
                        columns.Add(CategorySlidesCountColumn);
                        columns.Add(CategoryRolesCountColumn);
                        columns.Add(CategoryDetailsColumn);
                        columns.Add(CategoryEditColumn);
                        columns.Add(CategoryAssignRolesColumn);
                        columns.Add(CategoryDeleteColumn);
                    })
                .WithSorting(true, "Index", SortDirection.Asc)
                .WithRetrieveDataMethod(
                    context =>
                    {
                        var sortColumnName =
                            context.QueryOptions.SortColumnName;
                        var id =
                            context.QueryOptions.GetPageParameterString("id");

                        id.TryParse(out int roleId);
                        var sortDirection = context.QueryOptions.SortDirection;
                        var result = new QueryResult<CategoryViewModel>();

                        using (var db = new ApplicationDbContext())
                        {
                            var role = db.GetRole(roleId);

                            var query =
                                role == null
                                    ? db.GetCategoryViewModels()
                                    : role.GetCategories().AsViewModels();

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

        private static object GetPropertyValue(object o, string propertyName)
        {
            var propertyInfo = o.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new Exception();
            }

            return propertyInfo.GetValue(o, null);
        }

        private static (string Title, MvcGridBuilder<QuizResult> Grid)
            GetQuizResultsGrid()
        {
            const string title = "QuizResultsGrid";

            var grid = new MvcGridBuilder<QuizResult>()
                .WithAuthorizationType(AuthorizationType.Authorized)
                .WithPageParameterNames("Id")
                .AddColumns(
                    columns =>
                    {
                        columns.Add(QuizAttemptNumberColumn);
                        columns.Add(QuizQuestionsCorrectColumn);
                        columns.Add(QuizScoreColumn);
                        columns.Add(QuizTimeToCompleteColumn);
                    })
                .WithSorting(true, "AttemptNumber", SortDirection.Asc)
                .WithRetrieveDataMethod(
                    context =>
                    {
                        var sortColumnName =
                            context.QueryOptions.SortColumnName;

                        var id =
                            context.QueryOptions.GetPageParameterString("id");

                        id.TryParse(out int trainingResultId);
                        var sortDirection = context.QueryOptions.SortDirection;
                        var result = new QueryResult<QuizResult>();

                        using (var db = new ApplicationDbContext())
                        {
                            var trainingResult =
                                db.GetTrainingResult(trainingResultId);

                            var query = trainingResult.GetQuizResults();

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

        private static (string Title, MvcGridBuilder<RoleViewModel> Grid)
            GetRolesGrid()
        {
            const string title = "RolesGrid";

            var grid = new MvcGridBuilder<RoleViewModel>()
                .WithAuthorizationType(AuthorizationType.Authorized)
                .WithPageParameterNames("Id")
                .AddColumns(
                    columns =>
                    {
                        columns.Add(RoleIndexColumn);
                        columns.Add(RoleTitleColumn);
                        columns.Add(RoleCategoriesCountColumn);
                        columns.Add(RoleDetailsColumn);
                        columns.Add(RoleEditColumn);
                        columns.Add(RoleAssignCategoriesColumn);
                        columns.Add(RoleDeleteColumn);
                    })
                .WithSorting(true, "Index", SortDirection.Asc)
                .WithRetrieveDataMethod(
                    context =>
                    {
                        var sortColumnName =
                            context.QueryOptions.SortColumnName;

                        var id =
                            context.QueryOptions.GetPageParameterString("id");

                        id.TryParse(out int categoryId);
                        var sortDirection = context.QueryOptions.SortDirection;
                        var result = new QueryResult<RoleViewModel>();

                        using (var db = new ApplicationDbContext())
                        {
                            var category = db.GetCategory(categoryId);

                            var query =
                                category == null
                                    ? db.GetRoleViewModels()
                                    : category.GetRoles().AsViewModels();

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

        private static (string Title, MvcGridBuilder<SlideViewModel> Grid)
            GetSlidesGrid()
        {
            const string title = "SlidesGrid";
            var grid = new MvcGridBuilder<SlideViewModel>()
                .WithAuthorizationType(AuthorizationType.Authorized)
                .WithPageParameterNames("Id")
                .AddColumns(
                    columns =>
                    {
                        columns.Add(SlideIndexColumn);
                        columns.Add(SlideTitleColumn);
                        columns.Add(SlideImageColumn);
                        columns.Add(SlideShouldShowSlideInSlideshowColumn);
                        columns.Add(SlideShouldShowQuestionOnQuizColumn);
                        columns.Add(SlideShouldShowImageOnQuizColumn);
                        columns.Add(SlideDetailsColumn);
                        columns.Add(SlideEditColumn);
                        columns.Add(SlideDeleteColumn);
                    })
                .WithSorting(true, "Index", SortDirection.Asc)
                .WithRetrieveDataMethod(
                    context =>
                    {
                        var sortColumnName =
                            context.QueryOptions.SortColumnName;

                        var id =
                            context.QueryOptions.GetPageParameterString("id");

                        id.TryParse(out int categoryId);
                        var sortDirection = context.QueryOptions.SortDirection;
                        var result = new QueryResult<SlideViewModel>();

                        using (var db = new ApplicationDbContext())
                        {
                            var category = db.GetCategory(categoryId);
                            var query = category.GetSlides().AsViewModels();

                            if (!sortColumnName.IsNullOrWhiteSpace())
                            {
                                query = query.OrderBy(
                                    x => GetPropertyValue(x, sortColumnName),
                                    sortDirection);
                            }

                            result.Items = query;
                        }

                        return result;
                    });

            return (title, grid);
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