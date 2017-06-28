using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels;
using MichaelBrandonMorris.MvcGrid.Models;
using Column =
    MichaelBrandonMorris.MvcGrid.Models.GridColumn<MichaelBrandonMorris.KingsportMillSafetyTraining.Models.CategoryViewModel>;
using Grid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.KingsportMillSafetyTraining.Models.CategoryViewModel>;
using RetrieveDataMethod =
    System.Func<MichaelBrandonMorris.MvcGrid.Models.GridContext,
        MichaelBrandonMorris.MvcGrid.Models.QueryResult<MichaelBrandonMorris.KingsportMillSafetyTraining.Models.CategoryViewModel
        >>;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    internal static class CategoriesGrid
    {
        private static Column AssignRoles => new Column
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

        private static Column Delete => new Column
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
                "Categories",
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
                "Categories",
                new
                {
                    id = x.Id
                }),
            ValueTemplate = MvcGridConfig.EditValueTemplate
        };

        private static Column Index => new Column
        {
            ColumnName = "Index",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Index",
            ValueExpression = (x, y) => x.Index.ToString()
        };

        private static RetrieveDataMethod RetrieveDataMethod => context =>
        {
            var sortColumnName = context.QueryOptions.SortColumnName;
            var id = context.QueryOptions.GetPageParameterString("id");
            id.TryParse(out int roleId);
            var sortDirection = context.QueryOptions.SortDirection;
            var result = new QueryResult<CategoryViewModel>();

            using (var db = new ApplicationDbContext())
            {
                var role = db.GetRole(roleId);

                var query = role == null
                    ? db.GetCategories().AsViewModels()
                    : role.GetCategories().AsViewModels();

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

        private static Column RolesCount => new Column
        {
            ColumnName = "RolesCount",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Number of Roles",
            ValueExpression = (x, y) => x.RolesCount.ToString()
        };

        private static Column SlidesCount => new Column
        {
            ColumnName = "SlidesCount",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Number of Slides",
            ValueExpression = (x, y) => x.SlidesCount.ToString()
        };

        private static Column Title => new Column
        {
            ColumnName = "Title",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Title",
            ValueExpression = (x, y) => x.Title
        };

        internal static (string Title, Grid Grid) GetCategoriesGrid()
        {
            var grid = new Grid();
            grid.WithAuthorizationType(AuthorizationType.Authorized);
            grid.WithPageParameterNames("Id");
            grid.AddColumn(Index);
            grid.AddColumn(Title);
            grid.AddColumn(SlidesCount);
            grid.AddColumn(RolesCount);
            grid.AddColumn(Details);
            grid.AddColumn(Edit);
            grid.AddColumn(AssignRoles);
            grid.AddColumn(Delete);
            grid.WithSorting(true, "Index", SortDirection.Asc);
            grid.WithRetrieveDataMethod(RetrieveDataMethod);
            return ("CategoriesGrid", grid);
        }
    }
}