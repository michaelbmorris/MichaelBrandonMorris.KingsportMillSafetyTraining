using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using MichaelBrandonMorris.Extensions.OtherExtensions;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Models
{
    /// <summary>
    /// The database context for the program. Stores references to all the
    /// database tables, as well as methods to perform operations on all the
    /// data in transactions.
    /// </summary>
    public class KingsportMillSafetyTrainingDbContext : DbContext
    {
        public KingsportMillSafetyTrainingDbContext()
            : base("KingsportMillSafetyTrainingDbContext")
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
        /// The roles table.
        /// </summary>
        public DbSet<Role> Roles
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

        public void CreateCategory(Category category)
        {
            DoTransaction(() => _CreateCategory(category));
        }

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

        public Role GetRole(int id)
        {
            return DoTransaction(() => _GetRole(id));
        }

        public IList<Role> GetRoles()
        {
            return DoTransaction(_GetRoles);
        }

        public RoleViewModel GetRoleViewModel(int id)
        {
            return DoTransaction(() => _GetRoleViewModel(id));
        }

        public IList<RoleViewModel> GetRoleViewModels()
        {
            return DoTransaction(_GetRoleViewModels);
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

        public SlideViewModel GetSlideViewModel(int id)
        {
            return DoTransaction(() => _GetSlideViewModel(id));
        }

        public IList<SlideViewModel> GetSlideViewModels(int? categoryId = null)
        {
            return DoTransaction(() => _GetSlideViewModels(categoryId));
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

        private void _CreateCategory(Category category)
        {
            Categories.Add(category);
        }

        private void _CreateRole(Role role)
        {
            Roles.Add(role);
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
            Roles.Attach(role);
            Roles.Remove(role);
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

            foreach (var answer in slide.Answers.Where(x => x.Id != 0)
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
                roles = Roles.ToList();
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
            return Roles.Find(id);
        }

        private IList<Role> _GetRoles()
        {
            return Roles.ToList();
        }

        private RoleViewModel _GetRoleViewModel(int id)
        {
            var role = Roles.Find(id);
            return new RoleViewModel(role);
        }

        private IList<RoleViewModel> _GetRoleViewModels()
        {
            var roleViewModels = new List<RoleViewModel>();

            /* ReSharper disable once LoopCanBeConvertedToQuery
             * Replacing this loop with a LINQ expression will cause errors.
             */
            foreach (var role in Roles)
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
                    foreach (var slide in category.Slides.OrderBy(x => x.Index))
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

                slides = category.Slides.OrderBy(x => x.Index).ToList();
            }

            return slides.ToList().Select(slide => new SlideViewModel(slide)).ToList();       
        }

        private void _PairCategoryAndRole(int categoryId, int roleId)
        {
            var category = Categories.Find(categoryId);

            if (category == null)
            {
                throw new Exception();
            }

            var role = Roles.Find(roleId);

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

        private void _UnpairCategoriesAndRoles()
        {
            Debug.WriteLine("unpairing");
            foreach (var category in Categories)
            {
                foreach (var role in category.Roles.ToList())
                {
                    category.Roles.Remove(role);
                }
            }

            foreach (var role in Roles)
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