using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.MvcGrid.Models;
using Column =
    MichaelBrandonMorris.MvcGrid.Models.GridColumn<MichaelBrandonMorris.KingsportMillSafetyTraining.Models.SlideViewModel>;
using Grid =
    MichaelBrandonMorris.MvcGrid.Models.MvcGridBuilder<MichaelBrandonMorris.KingsportMillSafetyTraining.Models.SlideViewModel>;
using RetrieveDataMethod =
    System.Func<MichaelBrandonMorris.MvcGrid.Models.GridContext,
        MichaelBrandonMorris.MvcGrid.Models.QueryResult<MichaelBrandonMorris.KingsportMillSafetyTraining.Models.SlideViewModel>>;


namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    internal static class SlidesGrid
    {
        private static readonly Column Index = new Column
        {
            ColumnName = "Index",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Index",
            ValueExpression = (x, y) => x.Index.ToString()
        };

        private static Column Delete => new Column
        {
            ColumnName = "Delete",
            EnableFiltering = false,
            EnableSorting = false,
            HeaderText = "",
            HtmlEncode = false,
            ValueExpression = (x, y) => y.UrlHelper.Action(
                "Delete",
                "Slides",
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
            HeaderText = "",
            HtmlEncode = false,
            ValueExpression = (x, y) => y.UrlHelper.Action(
                "Details",
                "Slides",
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
            HeaderText = "",
            HtmlEncode = false,
            ValueExpression = (x, y) => y.UrlHelper.Action(
                "Edit",
                "Slides",
                new
                {
                    id = x.Id
                }),
            ValueTemplate = MvcGridConfig.EditValueTemplate
        };

        private static Column Image => new Column
        {
            ColumnName = "Image",
            EnableFiltering = false,
            EnableSorting = false,
            HeaderText = "Image",
            HtmlEncode = false,
            ValueExpression = (x, y) => y.UrlHelper.Action(
                "RenderImage",
                "Slides",
                new
                {
                    id = x.Id
                }),
            ValueTemplate = "<img src='{Value}' alt='{Model.ImageDescription}'>"
        };

        private static RetrieveDataMethod RetrieveDataMethod => context =>
        {
            var sortColumnName = context.QueryOptions.SortColumnName;
            var id = context.QueryOptions.GetPageParameterString("id");
            id.TryParse(out int categoryId);
            var sortDirection = context.QueryOptions.SortDirection;
            var result = new QueryResult<SlideViewModel>();

            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                var category = db.GetCategory(categoryId);
                var query = category.GetSlides().AsViewModels();

                if (!sortColumnName.IsNullOrWhiteSpace())
                {
                    query = query.OrderBy(
                        x => x.GetPropertyValue(sortColumnName),
                        sortDirection);
                }

                result.Items = query;
            }

            return result;
        };

        private static Column ShouldShowImageOnQuiz => new Column
        {
            ColumnName = "ShouldShowImageOnQuiz",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Show Image on Quiz?",
            ValueExpression = (x, y) => x.ShouldShowImageOnQuiz.ToString()
        };

        private static Column ShouldShowQuestionOnQuiz => new Column
        {
            ColumnName = "ShouldShowQuestionOnQuiz",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Show Question on Quiz?",
            ValueExpression = (x, y) => x.ShouldShowQuestionOnQuiz.ToString()
        };

        private static Column ShouldShowSlideInSlideshow => new Column
        {
            ColumnName = "ShouldShowSlideInSlideshow",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Show Slide in Slideshow?",
            ValueExpression = (x, y) => x.ShouldShowSlideInSlideshow.ToString()
        };

        private static Column Title => new Column
        {
            ColumnName = "Title",
            EnableFiltering = true,
            EnableSorting = true,
            HeaderText = "Title",
            ValueExpression = (x, y) => x.Title
        };

        internal static (string Title, Grid Grid) GetSlidesGrid()
        {
            const string title = "SlidesGrid";
            var grid = new Grid();
            grid.WithAuthorizationType(AuthorizationType.Authorized);
            grid.WithPageParameterNames("Id");
            grid.AddColumn(Index);
            grid.AddColumn(Title);
            grid.AddColumn(Image);
            grid.AddColumn(ShouldShowSlideInSlideshow);
            grid.AddColumn(ShouldShowQuestionOnQuiz);
            grid.AddColumn(ShouldShowImageOnQuiz);
            grid.AddColumn(Details);
            grid.AddColumn(Edit);
            grid.AddColumn(Delete);
            grid.WithSorting(true, "Index", SortDirection.Asc);
            grid.WithRetrieveDataMethod(RetrieveDataMethod);
            return (title, grid);
        }
    }
}