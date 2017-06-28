using System.Linq;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.MvcGrid.Models;
using Column =
    MichaelBrandonMorris.MvcGrid.Models.GridColumn<MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models.QuizResult>;
using Grid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models.QuizResult>;
using RetrieveDataMethod =
    System.Func<MichaelBrandonMorris.MvcGrid.Models.GridContext,
        MichaelBrandonMorris.MvcGrid.Models.QueryResult<MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models.QuizResult>>;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    internal static class QuizResultsGrid
    {
        private static Column AttemptNumber => new Column
        {
            ColumnName = "AttemptNumber",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Attempt Number",
            ValueExpression = (x, y) => x.AttemptNumber.ToString()
        };

        private static Column QuestionsCorrect => new Column
        {
            ColumnName = "QuestionsCorrect",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Questions Correct",
            ValueExpression = (x, y) => x.QuestionsCorrect.ToString(),
            ValueTemplate = "{Value} / {Model.TotalQuestions}"
        };

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

        private static Column Score => new Column
        {
            ColumnName = "Score",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Score",
            ValueExpression = (x, y) => x.Score
        };

        private static Column TimeToComplete => new Column
        {
            ColumnName = "TimeToComplete",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Time to Complete",
            ValueExpression = (x, y) => x.TimeToCompleteString
        };

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