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
using Microsoft.AspNet.Identity.EntityFramework;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }

        /// <summary>
        /// The answers table.
        /// </summary>
        public DbSet<Answer> Answers
        {
            get;
            set;
        }

        /// <summary>
        /// The categories table.
        /// </summary>
        public DbSet<Category> Categories
        {
            get;
            set;
        }

        /// <summary>
        /// The slides table.
        /// </summary>
        public DbSet<Slide> Slides
        {
            get;
            set;
        }

        /// <summary>
        /// The roles table.
        /// </summary>
        public DbSet<Role> TrainingRoles
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new <see cref="ApplicationDbContext" />.
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// Uses a transaction to add the specified <see cref="Category" /> to
        /// the database.
        /// </summary>
        /// <param name="category"></param>
        public void CreateCategory(Category category)
        {
            DoTransaction(() => _CreateCategory(category));
        }

        /// <summary>
        /// Uses a transaction to add the specified <see cref="Role" /> to the
        /// database.
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
        /// Uses a transaction to modify the specified <see cref="T" /> in the
        /// database.
        /// </summary>
        /// <typeparam name="T">
        /// The type of object to modify. Must be a class.
        /// Should be a type in the database.
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

        public void EditSlide(SlideViewModel slideViewModel)
        {
            var slide = Slides.Find(slideViewModel.Id);

            if (slide == null)
            {
                throw new Exception();
            }
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

        public IList<Category> GetCategories(
            Func<Category, object> orderBy = null,
            Func<Category, bool> where = null)
        {
            return DoTransaction(() => _GetCategories(orderBy, where));
        }

        public Category GetCategory(int id)
        {
            return DoTransaction(() => _GetCategory(id));
        }

        public CategoryViewModel GetCategoryViewModel(int id)
        {
            return DoTransaction(() => _GetCategoryViewModel(id));
        }

        public IList<CategoryViewModel> GetCategoryViewModels()
        {
            return DoTransaction(_GetCategoryViewModels);
        }

        /// <summary>
        /// Uses a transaction to get the
        /// <see cref="IList{QuizSlideViewModel}" /> for the specified role.
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

        public IList<Role> GetRoles(Func<Role, object> orderByPredicate = null)
        {
            return DoTransaction(() => _GetRoles(orderByPredicate));
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

        public IList<Slide> GetSlides(
            int? categoryId = null,
            Func<Slide, object> orderByPredicate = null)
        {
            return DoTransaction(
                () => _GetSlides(categoryId, orderByPredicate));
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

        public ApplicationUser GetUser(string id)
        {
            return DoTransaction(() => Users.Find(id));
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
            var quizViewModel = new List<QuizSlideViewModel>();

            foreach (var category in role.Categories)
            {
                foreach (var slide in category.Slides)
                {
                    if (slide.ShouldShowSlideInSlideshow
                        && slide.ShouldShowQuestionOnQuiz)
                    {
                        quizViewModel.Add(new QuizSlideViewModel(slide));
                    }
                }
            }

            return quizViewModel.Shuffle();
        }

        private static IList<SlideViewModel> _GetSlideshowViewModel(Role role)
        {
            var slideViewModels = new List<SlideViewModel>();

            foreach (var category in Enumerable.OrderBy(
                role.Categories,
                x => x.Index))
            {
                foreach (var slide in Enumerable.OrderBy(
                        category.Slides,
                        x => x.Index)
                    .Where(x => x.ShouldShowSlideInSlideshow))
                {
                    slideViewModels.Add(new SlideViewModel(slide));
                }
            }

            return slideViewModels;
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
            foreach (var slide in category.Slides.ToList())
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
            foreach (var answer in slide.Answers.ToList())
            {
                Answers.Attach(answer);
                Answers.Remove(answer);
            }

            Slides.Attach(slide);
            Slides.Remove(slide);
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
                var originalAnswer = slide.Answers
                    .SingleOrDefault(x => x.Id == answer.Id && x.Id != 0);

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

            foreach (var answer in Enumerable
                .Where(slide.Answers, x => x.Id != 0)
                .ToList())
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
            IList<Role> roles;

            if (id == null)
            {
                roles = TrainingRoles.ToList();
            }
            else
            {
                roles = new List<Role>
                {
                    _GetRole(id.Value)
                };
            }

            return new AssignCategoriesViewModel(
                roles,
                _GetCategoryViewModels());
        }

        private AssignRolesViewModel _GetAssignRolesViewModel(int? id = null)
        {
            IList<Category> categories;

            if (id == null)
            {
                categories = Categories.ToList();
            }
            else
            {
                categories = new List<Category>
                {
                    _GetCategory(id.Value)
                };
            }

            return new AssignRolesViewModel(categories, _GetRoleViewModels());
        }

        private IList<Category> _GetCategories(
            Func<Category, object> orderByPredicate = null,
            Func<Category, bool> wherePredicate = null)
        {
            IEnumerable<Category> categories = Categories;

            if (orderByPredicate != null)
            {
                categories = categories.OrderBy(orderByPredicate);
            }

            if (wherePredicate != null)
            {
                categories = categories.Where(wherePredicate);
            }

            return categories.ToList();
        }

        private Category _GetCategory(int id)
        {
            return Categories.Find(id);
        }

        private CategoryViewModel _GetCategoryViewModel(int id)
        {
            var category = Categories.Find(id);
            return new CategoryViewModel(category);
        }

        private IList<CategoryViewModel> _GetCategoryViewModels()
        {
            var categoryViewModels = new List<CategoryViewModel>();

            /* ReSharper disable once LoopCanBeConvertedToQuery
             * Replacing this loop with a LINQ expression will cause errors.
             */
            foreach (var category in Categories)
            {
                categoryViewModels.Add(
                    new CategoryViewModel(category));
            }

            return categoryViewModels;
        }

        private Role _GetRole(int id)
        {
            return TrainingRoles.Find(id);
        }

        private Role _GetRole(Func<Role, bool> predicate)
        {
            return TrainingRoles.Single(predicate);
        }

        private IList<Role> _GetRoles(
            Func<Role, object> orderByPredicate = null)
        {
            var roles = TrainingRoles;

            return orderByPredicate == null
                ? roles.ToList()
                : roles.OrderBy(orderByPredicate).ToList();
        }

        private RoleViewModel _GetRoleViewModel(int id)
        {
            var role = TrainingRoles.Find(id);
            return new RoleViewModel(role);
        }

        private IList<RoleViewModel> _GetRoleViewModels(
            Func<Role, object> orderByPredicate = null)
        {
            var roles = orderByPredicate == null
                ? TrainingRoles.AsEnumerable()
                : TrainingRoles.OrderBy(orderByPredicate);

            var roleViewModels = new List<RoleViewModel>();

            /* ReSharper disable once LoopCanBeConvertedToQuery
             * Replacing this loop with a LINQ expression will cause errors.
             */
            foreach (var role in roles)
            {
                roleViewModels.Add(new RoleViewModel(role));
            }

            return roleViewModels;
        }

        private Slide _GetSlide(int id)
        {
            return Slides.Find(id);
        }

        private IList<Slide> _GetSlides(
            int? categoryId = null,
            Func<Slide, object> orderByPredicate = null)
        {
            IList<Slide> slides;

            if (categoryId == null)
            {
                slides = Slides.ToList();
            }
            else
            {
                var category = Categories.Find(categoryId);

                if (category == null)
                {
                    throw new Exception();
                }

                slides = category.Slides;
            }

            return orderByPredicate == null
                ? slides
                : slides.OrderBy(orderByPredicate).ToList();
        }

        private SlideViewModel _GetSlideViewModel(int id)
        {
            var slide = Slides.Find(id);

            return slide == null
                ? null
                : new SlideViewModel(slide, _GetCategories());
        }

        private IList<SlideViewModel> _GetSlideViewModels(int? categoryId)
        {
            IList<Slide> slides = new List<Slide>();

            if (categoryId == null)
            {
                foreach (var category in Categories.OrderBy(x => x.Index))
                {
                    foreach (var slide in Enumerable.OrderBy(
                        category.Slides,
                        x => x.Index))
                    {
                        slides.Add(slide);
                    }
                }
            }
            else
            {
                var category = Categories.Find(categoryId);

                if (category == null)
                {
                    throw new Exception();
                }

                slides = Enumerable.OrderBy(category.Slides, x => x.Index)
                    .ToList();
            }

            return slides.ToList()
                .Select(slide => new SlideViewModel(slide))
                .ToList();
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

        private void _UnpairCategoriesAndRoles()
        {
            foreach (var category in Categories)
            {
                foreach (var role in category.Roles.ToList())
                {
                    category.Roles.Remove(role);
                }
            }

            foreach (var role in TrainingRoles)
            {
                foreach (var category in role.Categories.ToList())
                {
                    role.Categories.Remove(category);
                }
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
                    // The model failed to validate.
                    transaction.Rollback();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine(e);
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
    }
}