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

        public IList<Category> GetCategories()
        {
            return DoTransaction(_GetCategories);
        }

        public Category GetCategory(int id)
        {
            return DoTransaction(() => _GetCategory(id));
        }

        public Role GetRole(int id)
        {
            return DoTransaction(() => _GetRole(id));
        }

        public IList<Role> GetRoles()
        {
            return DoTransaction(_GetRoles);
        }

        public Slide GetSlide(int id)
        {
            return DoTransaction(() => _GetSlide(id));
        }

        public IList<Slide> GetSlides()
        {
            return DoTransaction(_GetSlides);
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

        private IList<Category> _GetCategories()
        {
            return Categories.ToList();
        }

        private Category _GetCategory(int id)
        {
            return Categories.Find(id);
        }

        private Role _GetRole(int id)
        {
            return Roles.Find(id);
        }

        private IList<Role> _GetRoles()
        {
            return Roles.ToList();
        }

        private Slide _GetSlide(int id)
        {
            return Slides.Find(id);
        }

        private IList<Slide> _GetSlides()
        {
            return Slides.ToList();
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