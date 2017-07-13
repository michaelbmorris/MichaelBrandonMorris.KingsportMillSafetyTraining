using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.MvcGrid.Models;
using Column =
    MichaelBrandonMorris.MvcGrid.Models.GridColumn<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.UserViewModel>;
using Grid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Models.UserViewModel>;
using RetrieveDataMethod =
    System.Func<MichaelBrandonMorris.MvcGrid.Models.GridContext,
        MichaelBrandonMorris.MvcGrid.Models.QueryResult<MichaelBrandonMorris.
            KingsportMillSafetyTraining.Models.UserViewModel>>;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    /// <summary>
    ///     Class UsersGrid.
    /// </summary>
    /// TODO Edit XML Comment Template for UsersGrid
    internal static class UsersGrid
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
                "Users",
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
                "Users",
                new
                {
                    id = x.Id
                }),
            ValueTemplate = MvcGridConfig.EditValueTemplate
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
        ///     Gets the last training date time.
        /// </summary>
        /// <value>The last training date time.</value>
        /// TODO Edit XML Comment Template for LastTrainingDateTime
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

        /// <summary>
        ///     Gets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        /// TODO Edit XML Comment Template for MiddleName
        private static Column MiddleName => new Column
        {
            ColumnName = "MiddleName",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Middle Name",
            ValueExpression = (x, y) => x.MiddleName
        };

        /// <summary>
        ///     Gets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        /// TODO Edit XML Comment Template for PhoneNumber
        private static Column PhoneNumber => new Column
        {
            ColumnName = "PhoneNumber",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Phone Number",
            ValueExpression = (x, y) => x.PhoneNumber
        };

        /// <summary>
        ///     Gets the results.
        /// </summary>
        /// <value>The results.</value>
        /// TODO Edit XML Comment Template for Results
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

        /// <summary>
        ///     Gets the retrieve data method.
        /// </summary>
        /// <value>The retrieve data method.</value>
        /// TODO Edit XML Comment Template for RetrieveDataMethod
        private static RetrieveDataMethod RetrieveDataMethod => context =>
        {
            var sortColumnName = context.QueryOptions.SortColumnName;
            var sortDirection = context.QueryOptions.SortDirection;
            var result = new QueryResult<UserViewModel>();

            using (var db = new KingsportMillSafetyTrainingDbContext())
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

        /// <summary>
        ///     Gets the users grid.
        /// </summary>
        /// <returns>System.String.Grid.</returns>
        /// TODO Edit XML Comment Template for GetUsersGrid
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
            grid.WithSorting(true, "LastName", SortDirection.Asc);
            grid.WithRetrieveDataMethod(RetrieveDataMethod);
            return ("UsersGrid", grid);
        }
    }
}