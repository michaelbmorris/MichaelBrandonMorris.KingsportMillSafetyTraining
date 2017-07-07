using System;
using System.Collections.Generic;
using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.MvcGrid.Models;
using Column =
    MichaelBrandonMorris.MvcGrid.Models.GridColumn<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.RoleViewModel>;
using Grid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.RoleViewModel>;
using RetrieveDataMethod =
    System.Func<MichaelBrandonMorris.MvcGrid.Models.GridContext,
        MichaelBrandonMorris.MvcGrid.Models.QueryResult<MichaelBrandonMorris.
            KingsportMillSafetyTraining.Models.RoleViewModel>>;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    /// <summary>
    ///     Class RolesGrid.
    /// </summary>
    /// TODO Edit XML Comment Template for RolesGrid
    internal static class RolesGrid
    {
        /// <summary>
        ///     Gets the assign categories.
        /// </summary>
        /// <value>The assign categories.</value>
        /// TODO Edit XML Comment Template for AssignCategories
        private static Column AssignCategories => new Column
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

        /// <summary>
        ///     Gets the categories count.
        /// </summary>
        /// <value>The categories count.</value>
        /// TODO Edit XML Comment Template for CategoriesCount
        private static Column CategoriesCount => new Column
        {
            ColumnName = "CategoriesCount",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Number of Categories",
            ValueExpression = (x, y) => x.CategoriesCount.ToString()
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
            HeaderText = "",
            HtmlEncode = false,
            ValueExpression = (x, y) => y.UrlHelper.Action(
                "Delete",
                "Roles",
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
            HeaderText = "",
            HtmlEncode = false,
            ValueExpression = (x, y) => y.UrlHelper.Action(
                "Details",
                "Roles",
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
            HeaderText = "",
            HtmlEncode = false,
            ValueExpression = (x, y) => y.UrlHelper.Action(
                "Edit",
                "Roles",
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
            id.TryParse(out int categoryId);
            var sortDirection = context.QueryOptions.SortDirection;
            var result = new QueryResult<RoleViewModel>();

            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                Category category = null;

                try
                {
                    category = db.GetCategory(categoryId);
                }
                catch (ArgumentNullException)
                {
                }
                catch (KeyNotFoundException)
                {
                }

                var query = category == null
                    ? db.GetRoles().AsViewModels()
                    : category.GetRoles().AsViewModels();

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
        ///     Gets the roles grid.
        /// </summary>
        /// <returns>System.String.Grid.</returns>
        /// TODO Edit XML Comment Template for GetRolesGrid
        internal static (string Title, Grid Grid) GetRolesGrid()
        {
            var grid = new MvcGridBuilder<RoleViewModel>();
            grid.WithAuthorizationType(AuthorizationType.Authorized);
            grid.WithPageParameterNames("Id");
            grid.AddColumn(Index);
            grid.AddColumn(Title);
            grid.AddColumn(CategoriesCount);
            grid.AddColumn(Details);
            grid.AddColumn(Edit);
            grid.AddColumn(AssignCategories);
            grid.AddColumn(Delete);
            grid.WithSorting(true, "Index", SortDirection.Asc);
            grid.WithRetrieveDataMethod(RetrieveDataMethod);
            return ("RolesGrid", grid);
        }
    }
}