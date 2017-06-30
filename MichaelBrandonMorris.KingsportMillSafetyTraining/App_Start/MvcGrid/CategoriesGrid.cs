using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.MvcGrid.Models;
using Column =
    MichaelBrandonMorris.MvcGrid.Models.GridColumn<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.CategoryViewModel>;
using Grid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.CategoryViewModel>;
using RetrieveDataMethod =
    System.Func<MichaelBrandonMorris.MvcGrid.Models.GridContext,
        MichaelBrandonMorris.MvcGrid.Models.QueryResult<MichaelBrandonMorris.
            KingsportMillSafetyTraining.Models.CategoryViewModel>>;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    /// <summary>
    ///     Class CategoriesGrid.
    /// </summary>
    /// TODO Edit XML Comment Template for CategoriesGrid
    internal static class CategoriesGrid
    {
        /// <summary>
        ///     Gets the assign roles.
        /// </summary>
        /// <value>The assign roles.</value>
        /// TODO Edit XML Comment Template for AssignRoles
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

        /// <summary>
        ///     Gets the delete.
        /// </summary>
        /// <value>The delete.</value>
        /// TODO Edit XML Comment Template for Delete
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
                "Categories",
                new
                {
                    id = x.Id
                }),
            ValueTemplate = MvcGridConfig.DetailsValueTemplate
        };

        /// <summary>
        ///     Gets the edit.
        /// </summary>
        /// <value>The edit.</value>
        /// TODO Edit XML Comment Template for Edit
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

        /// <summary>
        ///     Gets the index.
        /// </summary>
        /// <value>The index.</value>
        /// TODO Edit XML Comment Template for Index
        private static Column Index => new Column
        {
            ColumnName = "Index",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Index",
            ValueExpression = (x, y) => x.Index.ToString()
        };

        /// <summary>
        ///     Gets the retrieve data method.
        /// </summary>
        /// <value>The retrieve data method.</value>
        /// TODO Edit XML Comment Template for RetrieveDataMethod
        private static RetrieveDataMethod RetrieveDataMethod => context =>
        {
            var sortColumnName = context.QueryOptions.SortColumnName;
            var id = context.QueryOptions.GetPageParameterString("id");
            id.TryParse(out int roleId);
            var sortDirection = context.QueryOptions.SortDirection;
            var result = new QueryResult<CategoryViewModel>();

            using (var db = new KingsportMillSafetyTrainingDbContext())
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

        /// <summary>
        ///     Gets the roles count.
        /// </summary>
        /// <value>The roles count.</value>
        /// TODO Edit XML Comment Template for RolesCount
        private static Column RolesCount => new Column
        {
            ColumnName = "RolesCount",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Number of Roles",
            ValueExpression = (x, y) => x.RolesCount.ToString()
        };

        /// <summary>
        ///     Gets the slides count.
        /// </summary>
        /// <value>The slides count.</value>
        /// TODO Edit XML Comment Template for SlidesCount
        private static Column SlidesCount => new Column
        {
            ColumnName = "SlidesCount",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Number of Slides",
            ValueExpression = (x, y) => x.SlidesCount.ToString()
        };

        /// <summary>
        ///     Gets the title.
        /// </summary>
        /// <value>The title.</value>
        /// TODO Edit XML Comment Template for Title
        private static Column Title => new Column
        {
            ColumnName = "Title",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Title",
            ValueExpression = (x, y) => x.Title
        };

        /// <summary>
        ///     Gets the categories grid.
        /// </summary>
        /// <returns>System.String.Grid.</returns>
        /// TODO Edit XML Comment Template for GetCategoriesGrid
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