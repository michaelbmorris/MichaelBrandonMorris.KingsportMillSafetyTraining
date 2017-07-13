using MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid;
using MichaelBrandonMorris.MvcGrid.Web;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(MvcGridConfig), "RegisterGrids")]

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    /// <summary>
    ///     Class MvcGridConfig.
    /// </summary>
    /// TODO Edit XML Comment Template for MvcGridConfig
    public static class MvcGridConfig
    {
        /// <summary>
        ///     The delete value template
        /// </summary>
        /// TODO Edit XML Comment Template for DeleteValueTemplate
        internal const string DeleteValueTemplate =
            "<a href='{Value}' class='btn btn-danger' role='button'>Delete</a>";

        /// <summary>
        ///     The details value template
        /// </summary>
        /// TODO Edit XML Comment Template for DetailsValueTemplate
        internal const string DetailsValueTemplate =
                "<a href='{Value}' class='btn btn-primary' role='button'>Details</a>"
            ;

        /// <summary>
        ///     The edit value template
        /// </summary>
        /// TODO Edit XML Comment Template for EditValueTemplate
        internal const string EditValueTemplate =
            "<a href='{Value}' class='btn btn-primary' role='button'>Edit</a>";

        /// <summary>
        ///     Registers the grids.
        /// </summary>
        /// TODO Edit XML Comment Template for RegisterGrids
        public static void RegisterGrids()
        {
            var categoriesGrid = CategoriesGrid.GetCategoriesGrid();

            MvcGridDefinitionTable.Add(
                categoriesGrid.Title,
                categoriesGrid.Grid);

            var companiesGrid = CompaniesGrid.GetCompaniesGrid();
            MvcGridDefinitionTable.Add(companiesGrid.Title, companiesGrid.Grid);

            var quizResultsGrid = QuizResultsGrid.GetQuizResultsGrid();

            MvcGridDefinitionTable.Add(
                quizResultsGrid.Title,
                quizResultsGrid.Grid);

            var rolesGrid = GroupsGrid.GetRolesGrid();
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