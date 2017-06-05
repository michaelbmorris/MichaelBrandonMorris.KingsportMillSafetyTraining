using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            Entry(t).State = EntityState.Modified;
        }

        public void EditSlide(SlideViewModel slideViewModel)
        {
            var slide = Slides.Find(slideViewModel.Id);

            if (slide == null)
            {
                throw new Exception();
            }
        }

        public AssignCategoriesViewModel GetAssignCategoriesViewModel()
        {
            return DoTransaction(_GetAssignCategoriesViewModel);
        }

        public IList<Category> GetCategories()
        {
            return DoTransaction(_GetCategories);
        }

        public Category GetCategory(int id)
        {
            return DoTransaction(() => _GetCategory(id));
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

        public IList<RoleViewModel> GetRoleViewModels()
        {
            return DoTransaction(_GetRoleViewModels);
        }

        public Slide GetSlide(int id)
        {
            return DoTransaction(() => _GetSlide(id));
        }

        public IList<Slide> GetSlides()
        {
            return DoTransaction(_GetSlides);
        }

        public IList<Slide> GetSlides(int categoryId)
        {
            return DoTransaction(() => _GetSlides(categoryId));
        }

        public SlideViewModel GetSlideViewModel(int id)
        {
            return DoTransaction(() => _GetSlideViewModel(id));
        }

        public IList<SlideViewModel> GetSlideViewModels()
        {
            return DoTransaction(_GetSlideViewModels);
        }

        private void _CreateCategory(Category category)
        {
            Categories.Add(category);
        }

        private void _CreateRole(Role role)
        {
            Roles.Add(role);
        }

        private void _CreateSlide(Slide slide)
        {
            Slides.Add(slide);
        }

        private void _DeleteCategory(Category category)
        {
            Categories.Remove(category);
        }

        private void _DeleteRole(Role role)
        {
            Roles.Remove(role);
        }

        private void _DeleteSlide(Slide slide)
        {
            Slides.Remove(slide);
        }

        private AssignCategoriesViewModel _GetAssignCategoriesViewModel()
        {
            return new AssignCategoriesViewModel(
                Roles.ToList(),
                _GetCategoryViewModels());
        }

        private IList<Category> _GetCategories()
        {
            return Categories.ToList();
        }

        private Category _GetCategory(int id)
        {
            return Categories.Find(id);
        }

        private IList<CategoryViewModel> _GetCategoryViewModels()
        {
            var categoryViewModels = new List<CategoryViewModel>();

            /* ReSharper disable once LoopCanBeConvertedToQuery
             * Replacing this loop with a LINQ expression will cause errors.
             */
            foreach (var category in Categories)
            {
                categoryViewModels.Add(new CategoryViewModel(category));
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

        private IList<Slide> _GetSlides(int categoryId)
        {
            var category = Categories.Find(categoryId);

            if (category == null)
            {
                throw new Exception();
            }

            return category.Slides;
        }

        private IList<Slide> _GetSlides()
        {
            return Slides.ToList();
        }

        private SlideViewModel _GetSlideViewModel(int id)
        {
            var slide = Slides.Find(id);

            return slide == null ? null : new SlideViewModel(slide);
        }

        private IList<SlideViewModel> _GetSlideViewModels()
        {
            var slideViewModels = new List<SlideViewModel>();

            /* ReSharper disable once LoopCanBeConvertedToQuery
             * Replacing this loop with a LINQ expression will cause errors.
             */
            foreach (var slide in Slides)
            {
                slideViewModels.Add(new SlideViewModel(slide));
            }

            return slideViewModels;
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