using MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid;
using MichaelBrandonMorris.MvcGrid.Web;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(MvcGridConfig), "RegisterGrids")]

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    /// <summary>
    ///     Configures the grids.
    /// </summary>
    public static class MvcGridConfig
    {
        internal const string DeleteValueTemplate =
            "<a href='{Value}' class='btn btn-danger' role='button'>Delete</a>";

        internal const string DetailsValueTemplate =
                "<a href='{Value}' class='btn btn-primary' role='button'>Details</a>"
            ;

        internal const string EditValueTemplate =
            "<a href='{Value}' class='btn btn-primary' role='button'>Edit</a>";

        /// <summary>
        ///     Adds the grids to the grid definition table.
        /// </summary>
        public static void RegisterGrids()
        {
            var categoriesGrid = CategoriesGrid.GetCategoriesGrid();

            MvcGridDefinitionTable.Add(
                categoriesGrid.Title,
                categoriesGrid.Grid);

            var quizResultsGrid = QuizResultsGrid.GetQuizResultsGrid();

            MvcGridDefinitionTable.Add(
                quizResultsGrid.Title,
                quizResultsGrid.Grid);

            var rolesGrid = RolesGrid.GetRolesGrid();
            MvcGridDefinitionTable.Add(rolesGrid.Title, rolesGrid.Grid);

            var slidesGrid = SlidesGrid.GetSlidesGrid();
            MvcGridDefinitionTable.Add(slidesGrid.Title, slidesGrid.Grid);

            var trainingResultsGrid =
                TrainingResultsGrid.GetTrainingResultsGrid();

            MvcGridDefinitionTable.Add(
                trainingResultsGrid.Title,
                trainingResultsGrid.Grid);

            var usersGrid = UsersGrid.GetUsersGrid();
            MvcGridDefinitionTable.Add(usersGrid.Title, usersGrid.Grid);

            var userTrainingResultsGrid =
                TrainingResultsGrid.GetUserTrainingResultsGrid();

            MvcGridDefinitionTable.Add(
                userTrainingResultsGrid.Title,
                userTrainingResultsGrid.Grid);
        }
    }
}