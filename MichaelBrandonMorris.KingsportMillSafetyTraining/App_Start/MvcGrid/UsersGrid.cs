using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.User;
using MichaelBrandonMorris.MvcGrid.Models;
using Column =
    MichaelBrandonMorris.MvcGrid.Models.GridColumn<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.Identity.ViewModels.User.
        UserViewModel>;
using Grid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.Identity.ViewModels.User.
        UserViewModel>;
using RetrieveDataMethod =
    System.Func<MichaelBrandonMorris.MvcGrid.Models.GridContext,
        MichaelBrandonMorris.MvcGrid.Models.QueryResult<MichaelBrandonMorris.
            KingsportMillSafetyTraining.Models.Identity.ViewModels.User.
            UserViewModel>>;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    internal static class UsersGrid
    {
        private static Column CompanyName => new Column
        {
            ColumnName = "CompanyName",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Company",
            ValueExpression = (x, y) => x.CompanyName
        };

        private static Column Delete => new Column
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
            ValueTemplate = MvcGridConfig.DeleteValueTemplate
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
                "Users",
                new
                {
                    id = x.Id
                }),
            ValueTemplate = MvcGridConfig.DetailsValueTemplate
        };

        private static Column Edit => new Column
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
            ValueTemplate = MvcGridConfig.EditValueTemplate
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

        private static Column LastTrainingDateTime => new Column
        {
            ColumnName = "LastTrainingResultDateTime",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Last Training Completed On",
            ValueExpression =
                (x, y) => x.LastTrainingResultDateTime?.ToString("M/d/yy")
                          ?? "Training not completed."
        };

        private static Column MiddleName => new Column
        {
            ColumnName = "MiddleName",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Middle Name",
            ValueExpression = (x, y) => x.MiddleName
        };

        private static Column PhoneNumber => new Column
        {
            ColumnName = "PhoneNumber",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Phone Number",
            ValueExpression = (x, y) => x.PhoneNumber
        };

        private static Column Results => new Column
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

        private static RetrieveDataMethod RetrieveDataMethod => context =>
        {
            var sortColumnName = context.QueryOptions.SortColumnName;
            var sortDirection = context.QueryOptions.SortDirection;
            var result = new QueryResult<UserViewModel>();

            using (var db = new ApplicationDbContext())
            {
                var query = db.Users.ToList().AsViewModels();

                if (!sortColumnName.IsNullOrWhiteSpace())
                {
                    result.Items = query.OrderBy(
                        x => x.GetPropertyValue(sortColumnName),
                        sortDirection);
                }
                else
                {
                    result.Items = query.ToList();
                }
            }

            return result;
        };

        internal static (string Title, Grid Grid) GetUsersGrid()
        {
            var grid = new Grid();
            grid.WithAuthorizationType(AuthorizationType.Authorized);
            grid.AddColumn(FirstName);
            grid.AddColumn(MiddleName);
            grid.AddColumn(LastName);
            grid.AddColumn(CompanyName);
            grid.AddColumn(Email);
            grid.AddColumn(PhoneNumber);
            grid.AddColumn(LastTrainingDateTime);
            grid.AddColumn(Edit);
            grid.AddColumn(Details);
            grid.AddColumn(Results);
            grid.AddColumn(Delete);
            grid.WithSorting(true, "LastName", SortDirection.Asc);
            grid.WithRetrieveDataMethod(RetrieveDataMethod);
            return ("UsersGrid", grid);
        }
    }
}