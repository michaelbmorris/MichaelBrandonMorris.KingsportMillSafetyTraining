using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.Result;
using MichaelBrandonMorris.MvcGrid.Models;
using Column =
    MichaelBrandonMorris.MvcGrid.Models.GridColumn<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.Identity.ViewModels.Result.
        TrainingResultViewModel>;
using Grid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.Identity.ViewModels.Result.
        TrainingResultViewModel>;
using RetrieveDataMethod =
    System.Func<MichaelBrandonMorris.MvcGrid.Models.GridContext,
        MichaelBrandonMorris.MvcGrid.Models.QueryResult<MichaelBrandonMorris.
            KingsportMillSafetyTraining.Models.Identity.ViewModels.Result.
            TrainingResultViewModel>>;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    internal static class TrainingResultsGrid
    {
        private static Column CompanyName => new Column
        {
            ColumnName = "CompanyName",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Company",
            ValueExpression = (x, y) => x.CompanyName
        };

        private static Column CompletionDateTime => new Column
        {
            ColumnName = "CompletionDateTime",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Completed On",
            ValueExpression = (x, y) => x.CompletionDateTime.ToString()
        };

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


        private static Column Email => new Column
        {
            ColumnName = "Email",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Email",
            ValueExpression = (x, y) => x.Email
        };

        private static Column FirstName => new Column
        {
            ColumnName = "FirstName",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "First Name",
            ValueExpression = (x, y) => x.FirstName
        };

        private static Column LastName => new Column
        {
            ColumnName = "LastName",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Last Name",
            ValueExpression = (x, y) => x.LastName
        };

        private static Column QuizAttemptsCount => new Column
        {
            ColumnName = "QuizAttemptsCount",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Number of Quiz Attempts",
            ValueExpression = (x, y) => x.QuizAttemptsCount.ToString()
        };

        private static RetrieveDataMethod RetrieveDataMethod => context =>
        {
            var sortColumnName = context.QueryOptions.SortColumnName;
            var sortDirection = context.QueryOptions.SortDirection;
            var id = context.QueryOptions.GetPageParameterString("id");
            var result = new QueryResult<TrainingResultViewModel>();

            using (var db = new ApplicationDbContext())
            {
                var query = db.GetTrainingResultsDescending(id).AsViewModels();

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

        private static Column RoleTitle => new Column
        {
            ColumnName = "RoleTitle",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Role",
            ValueExpression = (x, y) => x.RoleTitle
        };

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