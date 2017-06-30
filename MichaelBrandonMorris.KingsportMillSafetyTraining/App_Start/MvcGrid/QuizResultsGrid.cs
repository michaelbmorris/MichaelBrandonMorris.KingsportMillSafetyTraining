using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.MvcGrid.Models;
using Column =
    MichaelBrandonMorris.MvcGrid.Models.GridColumn<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Db.Models.QuizResult>;
using Grid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.
        KingsportMillSafetyTraining.Db.Models.QuizResult>;
using RetrieveDataMethod =
    System.Func<MichaelBrandonMorris.MvcGrid.Models.GridContext,
        MichaelBrandonMorris.MvcGrid.Models.QueryResult<MichaelBrandonMorris.
            KingsportMillSafetyTraining.Db.Models.QuizResult>>;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    /// <summary>
    ///     Class QuizResultsGrid.
    /// </summary>
    /// TODO Edit XML Comment Template for QuizResultsGrid
    internal static class QuizResultsGrid
    {
        /// <summary>
        ///     Gets the attempt number.
        /// </summary>
        /// <value>The attempt number.</value>
        /// TODO Edit XML Comment Template for AttemptNumber
        private static Column AttemptNumber => new Column
        {
            ColumnName = "AttemptNumber",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Attempt Number",
            ValueExpression = (x, y) => x.AttemptNumber.ToString()
        };

        /// <summary>
        ///     Gets the questions correct.
        /// </summary>
        /// <value>The questions correct.</value>
        /// TODO Edit XML Comment Template for QuestionsCorrect
        private static Column QuestionsCorrect => new Column
        {
            ColumnName = "QuestionsCorrect",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Questions Correct",
            ValueExpression = (x, y) => x.QuestionsCorrect.ToString(),
            ValueTemplate = "{Value} / {Model.TotalQuestions}"
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
            id.TryParse(out int trainingResultId);
            var sortDirection = context.QueryOptions.SortDirection;
            var result = new QueryResult<QuizResult>();

            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                var trainingResult = db.GetTrainingResult(trainingResultId);
                var query = trainingResult.GetQuizResults();

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
        ///     Gets the score.
        /// </summary>
        /// <value>The score.</value>
        /// TODO Edit XML Comment Template for Score
        private static Column Score => new Column
        {
            ColumnName = "Score",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Score",
            ValueExpression = (x, y) => x.Score
        };

        /// <summary>
        ///     Gets the time to complete.
        /// </summary>
        /// <value>The time to complete.</value>
        /// TODO Edit XML Comment Template for TimeToComplete
        private static Column TimeToComplete => new Column
        {
            ColumnName = "TimeToComplete",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Time to Complete",
            ValueExpression = (x, y) => x.TimeToCompleteString
        };

        /// <summary>
        ///     Gets the quiz results grid.
        /// </summary>
        /// <returns>System.String.Grid.</returns>
        /// TODO Edit XML Comment Template for GetQuizResultsGrid
        internal static (string Title, Grid Grid) GetQuizResultsGrid()
        {
            var grid = new Grid();
            grid.WithAuthorizationType(AuthorizationType.Authorized);
            grid.WithPageParameterNames("Id");
            grid.AddColumn(AttemptNumber);
            grid.AddColumn(QuestionsCorrect);
            grid.AddColumn(Score);
            grid.AddColumn(TimeToComplete);
            grid.WithSorting(true, "AttemptNumber", SortDirection.Asc);
            grid.WithRetrieveDataMethod(RetrieveDataMethod);
            return ("QuizResultsGrid", grid);
        }
    }
}