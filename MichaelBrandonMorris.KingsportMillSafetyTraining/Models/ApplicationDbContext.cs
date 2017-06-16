using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.Extensions.OtherExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.Result;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.
    ViewModels.User;
using Microsoft.AspNet.Identity.EntityFramework;
using MoreLinq;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }

        /// <summary>
        ///     The answers table.
        /// </summary>
        public DbSet<Answer> Answers
        {
            get;
            set;
        }

        /// <summary>
        ///     The categories table.
        /// </summary>
        public DbSet<Category> Categories
        {
            get;
            set;
        }

        /// <summary>
        ///     The slides table.
        /// </summary>
        public DbSet<Slide> Slides
        {
            get;
            set;
        }

        /// <summary>
        ///     The training results table.
        /// </summary>
        public DbSet<TrainingResult> TrainingResults
        {
            get;
            set;
        }

        /// <summary>
        ///     The roles table.
        /// </summary>
        public DbSet<Role> TrainingRoles
        {
            get;
            set;
        }

        private static Func<Category, object> OrderByCategoryIndex => x => x
            .Index;

        private static Func<Slide, object> OrderBySlideIndex => x => x.Index;

        private static Func<Slide, bool> ShouldShowSlideInSlideshow => x => x
            .ShouldShowSlideInSlideshow;

        private static Func<Slide, bool> ShouldShowSlideOnQuiz => x =>
            x.ShouldShowSlideInSlideshow && x.ShouldShowQuestionOnQuiz;

        /// <summary>
        ///     Creates a new <see cref="ApplicationDbContext" />.
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public void AddQuizResult(
            int trainingResultId,
            int questionsCorrect,
            int totalQuestions)
        {
            DoTransaction(
                () => _AddQuizResult(
                    trainingResultId,
                    questionsCorrect,
                    totalQuestions));
        }

        public void AddTrainingResult(string userId)
        {
            DoTransaction(() => _AddTrainingResult(userId));
        }

        /// <summary>
        ///     Uses a transaction to add the specified <see cref="Category" />
        ///     to the database.
        /// </summary>
        /// <param name="category"></param>
        public void CreateCategory(Category category)
        {
            DoTransaction(() => _CreateCategory(category));
        }

        /// <summary>
        ///     Uses a transaction to add the specified <see cref="Role" /> to
        ///     the database.
        /// </summary>
        /// <param name="role"></param>
        public void CreateRole(Role role)
        {
            DoTransaction(() => _CreateRole(role));
        }

        public void CreateSlide(Slide slide)
        {
            DoTransaction(() => _CreateSlide(slide));
        }

        public void CreateSlide(SlideViewModel slideViewModel)
        {
            DoTransaction(() => _CreateSlide(slideViewModel));
        }

        public void DeleteCategory(Category category)
        {
            DoTransaction(() => _DeleteCategory(category));
        }

        public void DeleteRole(Role role)
        {
            DoTransaction(() => _DeleteRole(role));
        }

        public void DeleteSlide(Slide slide)
        {
            DoTransaction(() => _DeleteSlide(slide));
        }

        /// <summary>
        ///     Uses a transaction to modify the specified <see cref="T" /> in
        ///     the database.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of object to modify. Must be a class.
        ///     Should be a type in the database.
        /// </typeparam>
        /// <param name="t">The object to modify.</param>
        public void Edit<T>(T t)
            where T : class
        {
            DoTransaction(() => _Edit(t));
        }

        public void Edit(SlideViewModel slideViewModel)
        {
            DoTransaction(() => _Edit(slideViewModel));
        }

        public void Edit(TrainingResult trainingResult)
        {
            DoTransaction(() => _Edit(trainingResult));
        }

        public AssignCategoriesViewModel GetAssignCategoriesViewModel(
            int? id = null)
        {
            return DoTransaction(() => _GetAssignCategoriesViewModel(id));
        }

        public AssignRolesViewModel GetAssignRolesViewModel(int? id = null)
        {
            return DoTransaction(() => _GetAssignRolesViewModel(id));
        }

        public Category GetCategory(int id)
        {
            return DoTransaction(() => _GetCategory(id));
        }

        public CategoryViewModel GetCategoryViewModel(int id)
        {
            return DoTransaction(() => _GetCategoryViewModel(id));
        }

        public IList<CategoryViewModel> GetCategoryViewModels(
            Func<Category, object> orderByPredicate = null,
            Func<Category, bool> wherePredicate = null)
        {
            return DoTransaction(
                () => GetCategories(orderByPredicate, wherePredicate)
                    .AsViewModels());
        }

        /// <summary>
        ///     Uses a transaction to get the
        ///     <see cref="IList{QuizSlideViewModel}" /> for the specified role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<QuizSlideViewModel> GetQuizViewModel(Role role)
        {
            return DoTransaction(() => _GetQuizViewModel(role));
        }

        public Role GetRole(int id)
        {
            return DoTransaction(() => _GetRole(id));
        }

        public Role GetRole(Func<Role, bool> predicate)
        {
            return DoTransaction(() => _GetRole(predicate));
        }

        public RoleViewModel GetRoleViewModel(int id)
        {
            return DoTransaction(() => _GetRoleViewModel(id));
        }

        public IList<RoleViewModel> GetRoleViewModels(
            Func<Role, object> orderByPredicate = null)
        {
            return DoTransaction(() => _GetRoleViewModels(orderByPredicate));
        }

        public Slide GetSlide(int id)
        {
            return DoTransaction(() => _GetSlide(id));
        }

        public SlideViewModel GetNewSlideViewModel()
        {
            return DoTransaction(
                () => new SlideViewModel(null, GetCategories()));
        }

        public void SetUserRole(string userId, int? roleId)
        {
            DoTransaction(() => _SetUserRole(userId, roleId));      
        }

        private void _SetUserRole(string userId, int? roleId)
        {
            var user = Users.Find(userId);

            user.Role = roleId == null
                ? TrainingRoles.MaxBy(x => x.Index)
                : TrainingRoles.Find(roleId);
        }

        public IList<SlideViewModel> GetSlideshowViewModel(Role role)
        {
            return DoTransaction(() => _GetSlideshowViewModel(role));
        }

        public SlideViewModel GetSlideViewModel(int id)
        {
            return DoTransaction(() => _GetSlideViewModel(id));
        }

        public IList<SlideViewModel> GetSlideViewModels(int? categoryId = null)
        {
            return DoTransaction(() => _GetSlideViewModels(categoryId));
        }

        public TrainingResultViewModel GetTrainingResultViewModel(int id)
        {
            return DoTransaction(() => _GetTrainingResultViewModel(id));
        }

        public IList<TrainingResultViewModel> GetTrainingResultViewModels(
            string id)
        {
            return DoTransaction(() => _GetTrainingResultViewModels(id));
        }

        public ApplicationUser GetUser(string id)
        {
            return DoTransaction(() => Users.Find(id));
        }

        public IList<UserViewModel> GetUserViewModels()
        {
            return DoTransaction(_GetUserViewModels);
        }

        public bool IsUserTrainingResult(string userId, int trainingResultId)
        {
            return DoTransaction(
                () => _IsUserTrainingResult(userId, trainingResultId));
        }

        public void PairCategoryAndRole(int categoryId, int roleId)
        {
            DoTransaction(() => _PairCategoryAndRole(categoryId, roleId));
        }

        public void Reorder(IList<Category> categories)
        {
            DoTransaction(() => _Reorder(categories));
        }

        public void Reorder(IList<Slide> slides)
        {
            DoTransaction(() => _Reorder(slides));
        }

        public void Reorder(IList<Role> roles)
        {
            DoTransaction(() => _Reorder(roles));
        }

        public void SetUserLatestQuizStartDateTime(string userId)
        {
            DoTransaction(() => _SetUserLatestQuizStartDateTime(userId));
        }

        public void SetUserLatestTrainingStartDateTime(string userId)
        {
            DoTransaction(() => _SetUserLatestTrainingStartDateTime(userId));
        }

        public void UnpairCategoriesAndRoles()
        {
            DoTransaction(_UnpairCategoriesAndRoles);
        }

        public void UpdateCurrentAnswerIndex()
        {
            DoTransaction(_UpdateCurrentAnswerIndex);
        }

        public void UpdateCurrentCategoryIndex()
        {
            DoTransaction(_UpdateCurrentCategoryIndex);
        }

        public void UpdateCurrentSlideIndex()
        {
            DoTransaction(_UpdateCurrentSlideIndex);
        }

        private static IList<QuizSlideViewModel> _GetQuizViewModel(Role role)
        {
            var quizViewModel = from x in role.GetCategories()
                from y in x.GetSlides(wherePredicate: ShouldShowSlideOnQuiz)
                    .AsQuizSlideViewModels()
                select y;

            return quizViewModel.ShuffleToList();
        }

        private static IList<SlideViewModel> _GetSlideshowViewModel(Role role)
        {
            return (from x in role.GetCategories(OrderByCategoryIndex)
                from y in x.GetSlides(
                        OrderBySlideIndex,
                        ShouldShowSlideInSlideshow)
                    .AsViewModels()
                select y).ToList();
        }

        private void _AddQuizResult(
            int trainingResultId,
            int questionsCorrect,
            int totalQuestions)
        {
            var trainingResult = TrainingResults.Find(trainingResultId);

            if (trainingResult == null)
            {
                throw new Exception();
            }

            if (trainingResult.User.LatestQuizStartDateTime == null)
            {
                throw new Exception();
            }

            var timeToComplete = DateTime.Now
                                 - trainingResult.User.LatestQuizStartDateTime
                                     .Value;

            trainingResult.QuizResults.Add(
                new QuizResult
                {
                    QuestionsCorrect = questionsCorrect,
                    TimeToComplete = timeToComplete,
                    TotalQuestions = totalQuestions
                });
        }

        private void _AddTrainingResult(string userId)
        {
            var user = Users.Find(userId);

            user.TrainingResults.Add(
                new TrainingResult
                {
                    Role = user.Role
                });
        }

        private void _CreateCategory(Category category)
        {
            Categories.Add(category);
        }

        private void _CreateRole(Role role)
        {
            TrainingRoles.Add(role);
        }

        private void _CreateSlide(SlideViewModel slideViewModel)
        {
            Slides.Add(
                new Slide(slideViewModel)
                {
                    Answers = slideViewModel.Answers,
                    Category = _GetCategory(slideViewModel.CategoryId)
                });
        }

        private void _CreateSlide(Slide slide)
        {
            Slides.Add(slide);
        }

        private void _DeleteCategory(Category category)
        {
            foreach (var slide in category.Slides)
            {
                slide.Category = null;
            }

            Categories.Attach(category);
            Categories.Remove(category);
        }

        private void _DeleteRole(Role role)
        {
            TrainingRoles.Attach(role);
            TrainingRoles.Remove(role);
        }

        private void _DeleteSlide(Slide slide)
        {
            foreach (var answer in slide.Answers())
            {
                Answers.Attach(answer);
                Answers.Remove(answer);
            }

            Slides.Attach(slide);
            Slides.Remove(slide);
        }

        private void _Edit(TrainingResult trainingResult)
        {
            var originalTrainingResult =
                TrainingResults.Find(trainingResult.Id);

            if (originalTrainingResult == null)
            {
                throw new Exception();
            }

            var trainingResultEntry = Entry(trainingResult);
            trainingResultEntry.CurrentValues.SetValues(trainingResult);
        }

        private void _Edit(SlideViewModel model)
        {
            var slide = Slides.Find(model.Id);

            if (slide == null)
            {
                throw new Exception();
            }

            slide.Category = _GetCategory(model.CategoryId);
            slide.Title = model.Title;
            slide.Content = model.Content;

            if (model.Image != null)
            {
                slide.ImageBytes = model.Image.ToBytes();
            }

            slide.ImageDescription = model.ImageDescription;
            slide.ShouldShowSlideInSlideshow = model.ShouldShowSlideInSlideshow;
            slide.ShouldShowQuestionOnQuiz = model.ShouldShowQuestionOnQuiz;
            slide.ShouldShowImageOnQuiz = model.ShouldShowImageOnQuiz;
            slide.Question = model.Question;
            slide.CorrectAnswerIndex = model.CorrectAnswerIndex;

            foreach (var answer in model.Answers)
            {
                var originalAnswer =
                    slide.Answers.SingleOrDefault(
                        x => x.Id == answer.Id && x.Id != 0);

                if (originalAnswer != null)
                {
                    var answerEntry = Entry(originalAnswer);
                    answerEntry.CurrentValues.SetValues(answer);
                }
                else
                {
                    answer.Id = 0;
                    slide.Answers.Add(answer);
                }
            }

            foreach (var answer in slide.Answers(x => x.Id != 0))
            {
                if (model.Answers.All(x => x.Id != answer.Id))
                {
                    Answers.Remove(answer);
                }
            }
        }

        private void _Edit<T>(T t)
            where T : class
        {
            Entry(t).State = EntityState.Modified;
        }

        private AssignCategoriesViewModel _GetAssignCategoriesViewModel(
            int? id = null)
        {
            var roles = id == null
                ? GetRoles()
                : new List<Role>
                {
                    _GetRole(id.Value)
                };

            return new AssignCategoriesViewModel(
                roles,
                GetCategories().AsViewModels());
        }

        private AssignRolesViewModel _GetAssignRolesViewModel(int? id = null)
        {
            var categories = id == null
                ? GetCategories()
                : new List<Category>
                {
                    _GetCategory(id.Value)
                };

            return new AssignRolesViewModel(
                categories,
                GetRoles().AsViewModels());
        }

        private Category _GetCategory(int id)
        {
            return Categories.Find(id);
        }

        private CategoryViewModel _GetCategoryViewModel(int id)
        {
            return Categories.Find(id).AsViewModel();
        }

        private Role _GetRole(int id)
        {
            return TrainingRoles.Find(id);
        }

        private Role _GetRole(Func<Role, bool> predicate)
        {
            return TrainingRoles.Single(predicate);
        }

        private RoleViewModel _GetRoleViewModel(int id)
        {
            var role = TrainingRoles.Find(id);
            return new RoleViewModel(role);
        }

        private IList<RoleViewModel> _GetRoleViewModels(
            Func<Role, object> orderByPredicate = null)
        {
            IList<Role> roles = orderByPredicate == null
                ? TrainingRoles.ToList()
                : TrainingRoles.OrderBy(orderByPredicate).ToList();

            return roles.Select(role => new RoleViewModel(role)).ToList();
        }

        private Slide _GetSlide(int id)
        {
            return Slides.Find(id);
        }

        private SlideViewModel _GetSlideViewModel(int id)
        {
            var slide = Slides.Find(id);
            return slide.AsViewModel(GetCategories());
        }

        private IList<SlideViewModel> _GetSlideViewModels(int? categoryId)
        {
            IList<Slide> slides;

            if (categoryId == null)
            {
                slides = (from x in GetCategories(x => x.Index)
                    from y in x.GetSlides(y => y.Index)
                    select y).ToList();
            }
            else
            {
                var category = Categories.Find(categoryId);

                slides = category?.GetSlides(x => x.Index)
                         ?? throw new Exception();
            }

            return slides.AsViewModels();
        }

        private TrainingResultViewModel _GetTrainingResultViewModel(int id)
        {
            return TrainingResults.Find(id).AsViewModel();
        }

        private IList<TrainingResultViewModel> _GetTrainingResultViewModels(
            string id)
        {
            var user = Users.Find(id);
            return user.GetTrainingResultsDescending(x => x.CompletionDateTime).AsViewModels();
        }

        private IList<UserViewModel> _GetUserViewModels()
        {
            return GetUsers().AsViewModels();
        }

        private bool _IsUserTrainingResult(string userId, int trainingResultId)
        {
            var user = Users.Find(userId);
            return user.TrainingResults.Any(x => x.Id == trainingResultId);
        }

        private void _PairCategoryAndRole(int categoryId, int roleId)
        {
            var category = Categories.Find(categoryId);

            if (category == null)
            {
                throw new Exception();
            }

            var role = TrainingRoles.Find(roleId);

            if (role == null)
            {
                throw new Exception();
            }

            role.Categories.Add(category);
            category.Roles.Add(role);
        }

        private void _Reorder(IEnumerable<Category> categories)
        {
            foreach (var category in categories)
            {
                var categoryToEdit = _GetCategory(category.Id);
                categoryToEdit.Index = category.Index;
            }
        }

        private void _Reorder(IEnumerable<Slide> slides)
        {
            foreach (var slide in slides)
            {
                var slideToEdit = _GetSlide(slide.Id);
                slideToEdit.Index = slide.Index;
            }
        }

        private void _Reorder(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                var roleToEdit = _GetRole(role.Id);
                roleToEdit.Index = role.Index;
            }
        }

        private void _SetUserLatestQuizStartDateTime(string userId)
        {
            var user = Users.Find(userId);
            user.LatestQuizStartDateTime = DateTime.Now;
        }

        private void _SetUserLatestTrainingStartDateTime(string userId)
        {
            var user = Users.Find(userId);
            user.LatestTrainingStartDateTime = DateTime.Now;
        }

        private void _UnpairCategoriesAndRoles()
        {
            foreach (var category in GetCategories())
            foreach (var role in category.GetRoles())
            {
                category.Roles.Remove(role);
            }

            foreach (var role in GetRoles())
            foreach (var category in role.GetCategories())
            {
                role.Categories.Remove(category);
            }
        }

        private void _UpdateCurrentAnswerIndex()
        {
            Answer.CurrentIndex = !Categories.Any()
                ? 0
                : Categories.Max(x => x.Index);
        }

        private void _UpdateCurrentCategoryIndex()
        {
            Category.CurrentIndex = !Categories.Any()
                ? 0
                : Categories.Max(x => x.Index);
        }

        private void _UpdateCurrentSlideIndex()
        {
            Slide.CurrentIndex = !Categories.Any()
                ? 0
                : Categories.Max(x => x.Index);
        }

        private void DoTransaction(Action action)
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    action();
                    SaveChanges();
                    transaction.Commit();
                }
                catch (DbEntityValidationException)
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private T DoTransaction<T>(Func<T> func)
        {
            T t;

            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    t = func();
                    SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine(e);
                    throw;
                }
            }

            return t;
        }

        private IList<Category> GetCategories(
            Func<Category, object> orderByPredicate = null,
            Func<Category, bool> wherePredicate = null)
        {
            return Categories.OrderByWhere(orderByPredicate, wherePredicate);
        }

        private IList<Role> GetRoles(
            Func<Role, object> orderByPredicate = null,
            Func<Role, bool> wherePredicate = null)
        {
            return TrainingRoles.OrderByWhere(orderByPredicate, wherePredicate);
        }

        private IList<ApplicationUser> GetUsers(
            Func<ApplicationUser, object> orderByPredicate = null,
            Func<ApplicationUser, bool> wherePredicate = null)
        {
            return Users.OrderByWhere(orderByPredicate, wherePredicate);
        }
    }

    public static class Extensions
    {
        public static IList<Answer> Answers(
            this Slide slide,
            Func<Answer, object> orderByPredicate = null,
            Func<Answer, bool> wherePredicate = null)
        {
            return slide.Answers.OrderByWhere(orderByPredicate, wherePredicate);
        }

        public static IList<QuizSlideViewModel> AsQuizSlideViewModels(
            this IList<Slide> slides)
        {
            return slides.Select(x => new QuizSlideViewModel(x));
        }

        public static SlideViewModel AsViewModel(
            this Slide slide,
            IList<Category> categories = null)
        {
            if (slide == null)
            {
                throw new ArgumentNullException();
            }

            return new SlideViewModel(slide, categories);
        }

        public static RoleViewModel AsViewModel(this Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException();
            }

            return new RoleViewModel(role);
        }

        public static CategoryViewModel AsViewModel(this Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }

            return new CategoryViewModel(category);
        }

        public static TrainingResultViewModel AsViewModel(
            this TrainingResult trainingResult)
        {
            if (trainingResult == null)
            {
                throw new ArgumentNullException();
            }

            return new TrainingResultViewModel(trainingResult);
        }

        public static IList<UserViewModel> AsViewModels(
            this IList<ApplicationUser> users)
        {
            return users.Select(x => new UserViewModel(x));
        }

        public static IList<SlideViewModel> AsViewModels(
            this IList<Slide> slides)
        {
            return slides.Select(x => new SlideViewModel(x));
        }

        public static IList<RoleViewModel> AsViewModels(this IList<Role> roles)
        {
            return roles.Select(x => new RoleViewModel(x));
        }

        public static IList<CategoryViewModel> AsViewModels(
            this IList<Category> categories)
        {
            return categories.Select(x => new CategoryViewModel(x));
        }

        public static IList<TrainingResultViewModel> AsViewModels(
            this IList<TrainingResult> trainingResults)
        {
            return trainingResults.Select(x => new TrainingResultViewModel(x));
        }

        public static IList<Category> GetCategories(
            this Role role,
            Func<Category, object> orderByPredicate = null,
            Func<Category, bool> wherePredicate = null)
        {
            return role.Categories.OrderByWhere(
                orderByPredicate,
                wherePredicate);
        }

        public static IList<Role> GetRoles(this Category category)
        {
            return category.Roles;
        }

        public static IList<Slide> GetSlides(
            this Category category,
            Func<Slide, object> orderByPredicate = null,
            Func<Slide, bool> wherePredicate = null)
        {
            return category.Slides.OrderByWhere(
                orderByPredicate,
                wherePredicate);
        }

        public static IList<TrainingResult> GetTrainingResults(
            this ApplicationUser user,
            Func<TrainingResult, object> orderByPredicate = null,
            Func<TrainingResult, bool> wherePredicate = null)
        {
            return user.TrainingResults.OrderByWhere(
                orderByPredicate,
                wherePredicate);
        }

        public static IList<TrainingResult> GetTrainingResultsDescending(
            this ApplicationUser user,
            Func<TrainingResult, object> orderByPredicate = null,
            Func<TrainingResult, bool> wherePredicate = null)
        {
            return user.TrainingResults.OrderByDescendingWhere(
                orderByPredicate,
                wherePredicate);
        }
    }
}