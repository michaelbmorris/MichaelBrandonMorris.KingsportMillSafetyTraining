using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MoreLinq;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db
{
    /// <summary>
    ///     The database interface for the application.
    /// </summary>
    public sealed class ApplicationDbContext : IdentityDbContext<User>
    {
        /// <summary>
        ///     Creates a new <see cref="ApplicationDbContext" /> with a
        ///     specified name.
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
            Answers = Set<Answer>();
            Categories = Set<Category>();
            Slides = Set<Slide>();
            TrainingResults = Set<TrainingResult>();
            TrainingRoles = Set<Role>();
        }

        /// <summary>
        ///     The answers table.
        /// </summary>
        internal DbSet<Answer> Answers
        {
            get;
            set;
        }

        /// <summary>
        ///     The categories table.
        /// </summary>
        internal DbSet<Category> Categories
        {
            get;
            set;
        }

        /// <summary>
        ///     The slides table.
        /// </summary>
        internal DbSet<Slide> Slides
        {
            get;
            set;
        }

        /// <summary>
        ///     The training results table.
        /// </summary>
        internal DbSet<TrainingResult> TrainingResults
        {
            get;
            set;
        }

        /// <summary>
        ///     The (training) roles table.
        /// </summary>
        internal DbSet<Role> TrainingRoles
        {
            get;
            set;
        }

        private static Func<Category, object> OrderByCategoryIndex => x => x
            .Index;

        private static Func<TrainingResult, object> OrderByCompletionDateTime =>
            x => x.CompletionDateTime;

        private static Func<Role, object> OrderByRoleName => x => x.Title;

        private static Func<Slide, object> OrderBySlideIndex => x => x.Index;

        private static Func<Slide, bool> WhereShouldShowSlideInSlideshow => x =>
            x.ShouldShowSlideInSlideshow;

        private static Func<Slide, bool> WhereShouldShowSlideOnQuiz => x =>
            x.ShouldShowSlideInSlideshow && x.ShouldShowQuestionOnQuiz;

        /// <summary>
        ///     Creates a new <see cref="ApplicationDbContext" />.
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        /// <summary>
        ///     Adds a new <see cref="QuizResult" /> to the specified
        ///     <see cref="TrainingResult" />.
        /// </summary>
        /// <param name="trainingResultId">The training result identifier.</param>
        /// <param name="questionsCorrect">The number of questions correct.</param>
        /// <param name="totalQuestions">The total number of questions.</param>
        /// <exception cref="KeyNotFoundException">
        ///     Thrown when the training
        ///     result is not found in the database.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the user
        ///     does not have a latest quiz start time.
        /// </exception>
        public void AddQuizResult(
            int trainingResultId,
            int questionsCorrect,
            int totalQuestions)
        {
            DoTransaction(
                () =>
                {
                    var trainingResult = TrainingResults.Find(trainingResultId);

                    if (trainingResult == null)
                    {
                        throw new KeyNotFoundException(
                            $"Training result with id '{trainingResultId}' not found.");
                    }

                    if (trainingResult.User.LatestQuizStartDateTime == null)
                    {
                        throw new InvalidOperationException(
                            "User quiz start time not recorded.");
                    }

                    var timeToComplete =
                        DateTime.Now
                        - trainingResult.User.LatestQuizStartDateTime.Value;

                    trainingResult.QuizResults.Add(
                        new QuizResult
                        {
                            AttemptNumber =
                                trainingResult.QuizResults.Count + 1,
                            QuestionsCorrect = questionsCorrect,
                            TimeToComplete = timeToComplete,
                            TotalQuestions = totalQuestions
                        });
                });
        }

        /// <summary>
        ///     Adds a new <see cref="TrainingResult" /> to the specified
        ///     <see cref="User" />. Returns if the user's most recent training
        ///     result is not complete.
        /// </summary>
        /// <param name="userId"></param>
        public void AddTrainingResult(string userId)
        {
            DoTransaction(
                () =>
                {
                    var user = Users.Find(userId);

                    if (user.TrainingResults.LastOrDefault() != null
                        && user.TrainingResults.Last().CompletionDateTime
                        == null)
                    {
                        return;
                    }

                    user.TrainingResults.Add(
                        new TrainingResult
                        {
                            Role = user.Role
                        });
                });
        }

        /// <summary>
        ///     Creates the category in the database.
        /// </summary>
        /// <param name="category">The category to create.</param>
        public void CreateCategory(Category category)
        {
            DoTransaction(
                () =>
                {
                    Categories.Add(category);
                });
        }

        /// <summary>
        ///     Uses a transaction to add the specified <see cref="Role" /> to
        ///     the database.
        /// </summary>
        /// <param name="role"></param>
        public void CreateRole(Role role)
        {
            DoTransaction(
                () =>
                {
                    TrainingRoles.Add(role);
                });
        }

        public void CreateSlide(Slide slide, int categoryId)
        {
            DoTransaction(
                () =>
                {
                    slide.Category = Categories.Find(categoryId);
                    Slides.Add(slide);
                });
        }

        /// <summary>
        ///     Removes the specified <see cref="Category" /> from
        ///     <see cref="Categories" />.
        /// </summary>
        /// <param name="category"></param>
        public void DeleteCategory(Category category)
        {
            DoTransaction(
                () =>
                {
                    foreach (var slide in category.Slides)
                    {
                        slide.Category = null;
                    }

                    Categories.Attach(category);
                    Categories.Remove(category);
                });
        }

        /// <summary>
        ///     Removes the specified <see cref="Role" /> from
        ///     <see cref="TrainingRoles" />.
        /// </summary>
        /// <param name="role"></param>
        public void DeleteRole(Role role)
        {
            DoTransaction(
                () =>
                {
                    TrainingRoles.Attach(role);
                    TrainingRoles.Remove(role);
                });
        }

        /// <summary>
        ///     Removes the specified <see cref="Slide" /> from
        ///     <see cref="Slides" />.
        /// </summary>
        /// <param name="slide"></param>
        public void DeleteSlide(Slide slide)
        {
            DoTransaction(
                () =>
                {
                    foreach (var answer in slide.Answers())
                    {
                        Answers.Attach(answer);
                        Answers.Remove(answer);
                    }

                    Slides.Attach(slide);
                    Slides.Remove(slide);
                });
        }

        /// <summary>
        ///     Uses a transaction to modify the specified item in the database.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of object to modify. Must be a class.
        ///     Should be a type in the database.
        /// </typeparam>
        /// <param name="t">The object to modify.</param>
        public void Edit<T>(T t)
            where T : class
        {
            DoTransaction(
                () =>
                {
                    Entry(t).State = EntityState.Modified;
                });
        }

        public void Edit(Slide slide, int categoryId)
        {
            DoTransaction(
                () =>
                {
                    var entry = Entry(slide);
                    entry.State = EntityState.Modified;

                    foreach (var answer in slide.Answers)
                    {
                        var originalAnswer =
                            entry.Entity.Answers.SingleOrDefault(
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

                    foreach (var answer in entry.Entity.Answers(x => x.Id != 0))
                    {
                        if (slide.Answers.All(x => x.Id != answer.Id))
                        {
                            Answers.Remove(answer);
                        }
                    }
                });
        }


        /// <summary>
        ///     Edits the specified training result.
        /// </summary>
        /// <param name="trainingResult">The training result to edit.</param>
        /// <exception cref="KeyNotFoundException">
        ///     Thrown when the training
        ///     result is not found in the database.
        /// </exception>
        public void Edit(TrainingResult trainingResult)
        {
            DoTransaction(
                () =>
                {
                    var originalTrainingResult =
                        TrainingResults.Find(trainingResult.Id);

                    if (originalTrainingResult == null)
                    {
                        throw new KeyNotFoundException(
                            $"Training result with id '{trainingResult.Id}' not found");
                    }

                    var trainingResultEntry = Entry(trainingResult);
                    trainingResultEntry.CurrentValues.SetValues(trainingResult);
                });
        }

        public IList<Category> GetCategories(
            Func<Category, object> orderByPredicate = null,
            Func<Category, bool> wherePredicate = null)
        {
            return Categories.OrderByWhere(orderByPredicate, wherePredicate);
        }

        /// <summary>
        ///     Gets the specified <see cref="Category" />.
        /// </summary>
        /// <param name="id">The <see cref="Category" /> identifier.</param>
        /// <returns>The specified <see cref="Category" />.</returns>
        public Category GetCategory(int id)
        {
            return DoTransaction(() => Categories.Find(id));
        }

        /// <summary>
        ///     Gets the specified role.
        /// </summary>
        /// <param name="id">The role identifier.</param>
        /// <returns></returns>
        public Role GetRole(int id)
        {
            return DoTransaction(() => TrainingRoles.Find(id));
        }

        /// <summary>
        ///     Returns the only role that satisfies a specified condition, and throws an exception if more than one such role
        ///     exists.
        /// </summary>
        /// <param name="predicate">A function to test a role for a condition.</param>
        /// <returns>The single role that satisfies a condition.</returns>
        public Role GetRole(Func<Role, bool> predicate)
        {
            return DoTransaction(() => TrainingRoles.Single(predicate));
        }

        public IList<Role> GetRoles(
            Func<Role, object> orderByPredicate = null,
            Func<Role, bool> wherePredicate = null)
        {
            return TrainingRoles.OrderByWhere(orderByPredicate, wherePredicate);
        }

        /// <summary>
        ///     Gets the specified <see cref="Slide" />.
        /// </summary>
        /// <param name="id">The <see cref="Slide" /> identifier.</param>
        /// <returns>The specified <see cref="Slide" />.</returns>
        public Slide GetSlide(int id)
        {
            return DoTransaction(() => Slides.Find(id));
        }

        public IList<Slide> GetSlides(
            Func<Slide, object> orderBy = null,
            Func<Slide, bool> where = null)
        {
            return Slides.OrderByWhere(orderBy, where);
        }

        public IList<Slide> GetSlides(int categoryId,
            Func<Slide, object> orderBy = null,
            Func<Slide, bool> where = null)
        {
            return DoTransaction(
                () =>
                {
                    var category = Categories.Find(categoryId);

                    if (category == null)
                    {
                        throw new KeyNotFoundException(
                            GetNotFoundMessage<Category>(categoryId));
                    }

                    return category.Slides.OrderByWhere(orderBy, where);
                });
        }

        public TrainingResult GetTrainingResult(int id)
        {
            return DoTransaction(() => TrainingResults.Find(id));
        }

        public IList<TrainingResult> GetTrainingResultsDescending(
            Func<TrainingResult, object> orderByPredicate = null,
            Func<TrainingResult, bool> wherePredicate = null)
        {
            return TrainingResults.OrderByDescendingWhere(
                orderByPredicate,
                wherePredicate);
        }

        public IList<TrainingResult> GetTrainingResultsDescending(
            string userId,
            Func<TrainingResult, object> orderByPredicate = null,
            Func<TrainingResult, bool> wherePredicate = null)
        {
            return DoTransaction(
                () =>
                {
                    var user = Users.Find(userId);
                    return user.GetTrainingResultsDescending(
                        orderByPredicate,
                        wherePredicate);
                });
        }

        public User GetUser(string id)
        {
            return DoTransaction(() => Users.Find(id));
        }

        public IList<User> GetUsers(
            Func<User, object> orderByPredicate = null,
            Func<User, bool> wherePredicate = null)
        {
            return Users.OrderByWhere(orderByPredicate, wherePredicate);
        }

        public bool IsUserTrainingResult(string userId, int trainingResultId)
        {
            return DoTransaction(
                () =>
                {
                    var user = Users.Find(userId);
                    return user.TrainingResults.Any(
                        x => x.Id == trainingResultId);
                });
        }

        public void PairCategoryAndRole(int categoryId, int roleId)
        {
            DoTransaction(
                () =>
                {
                    var category = Categories.Find(categoryId);

                    if (category == null)
                    {
                        throw new KeyNotFoundException(
                            $"Category with id '{categoryId}' not found.");
                    }

                    var role = TrainingRoles.Find(roleId);

                    if (role == null)
                    {
                        throw new KeyNotFoundException(
                            $"Role with id '{roleId}' not found.");
                    }

                    role.Categories.Add(category);
                    category.Roles.Add(role);
                });
        }

        public void Reorder(IList<Category> categories)
        {
            DoTransaction(
                () =>
                {
                    foreach (var category in categories)
                    {
                        var categoryToEdit = Categories.Find(category.Id);

                        if (categoryToEdit == null)
                        {
                            throw new KeyNotFoundException(
                                $"Category with id '{category.Id}' not found.");
                        }

                        categoryToEdit.Index = category.Index;
                    }
                });
        }

        public void Reorder(IList<Slide> slides)
        {
            DoTransaction(
                () =>
                {
                    foreach (var slide in slides)
                    {
                        var slideToEdit = Slides.Find(slide.Id);

                        if (slideToEdit == null)
                        {
                            throw new KeyNotFoundException(
                                GetNotFoundMessage<Slide>(slide.Id));
                        }

                        slideToEdit.Index = slide.Index;
                    }
                });
        }

        /// <summary>
        ///     Changes the indices of the roles in the database to match the specified roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        public void Reorder(IList<Role> roles)
        {
            DoTransaction(
                () =>
                {
                    foreach (var role in roles)
                    {
                        var roleToEdit = TrainingRoles.Find(role.Id);

                        if (roleToEdit == null)
                        {
                            throw new KeyNotFoundException(
                                GetNotFoundMessage<Role>(role.Id));
                        }

                        roleToEdit.Index = role.Index;
                    }
                });
        }

        /// <summary>
        ///     Sets the specified <see cref="User" />'s latest quiz start datetime to the current datetime.
        /// </summary>
        /// <param name="userId">The <see cref="User" /> identifier.</param>
        public void SetUserLatestQuizStartDateTime(string userId)
        {
            DoTransaction(
                () =>
                {
                    var user = Users.Find(userId);
                    user.LatestQuizStartDateTime = DateTime.Now;
                });
        }

        /// <summary>
        ///     Sets the specified <see cref="User" />'s latest training start datetime to the current datetime.
        /// </summary>
        /// <param name="userId">The <see cref="User" /> identifier.</param>
        public void SetUserLatestTrainingStartDateTime(string userId)
        {
            DoTransaction(
                () =>
                {
                    var user = Users.Find(userId);
                    user.LatestTrainingStartDateTime = DateTime.Now;
                });
        }

        /// <summary>
        ///     Sets the <see cref="Role" /> of the specified <see cref="User" /> to the specified role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        public void SetUserRole(string userId, int? roleId)
        {
            DoTransaction(
                () =>
                {
                    var user = Users.Find(userId);

                    user.Role = roleId == null
                        ? TrainingRoles.MaxBy(x => x.Index)
                        : TrainingRoles.Find(roleId);
                });
        }

        /// <summary>
        ///     Unpairs all categories and roles.
        /// </summary>
        public void UnpairCategoriesAndRoles()
        {
            DoTransaction(
                () =>
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
                });
        }

        /// <summary>
        ///     Updates the current index property in the answer class to be
        ///     the highest index of any slide in the database.
        /// </summary>
        public void UpdateCurrentAnswerIndex()
        {
            DoTransaction(
                () =>
                {
                    Answer.CurrentIndex = !Categories.Any()
                        ? 0
                        : Categories.Max(x => x.Index);
                });
        }

        /// <summary>
        ///     Updates the current index property in the category class to be
        ///     the highest index of any slide in the database.
        /// </summary>
        public void UpdateCurrentCategoryIndex()
        {
            DoTransaction(
                () =>
                {
                    Category.CurrentIndex = !Categories.Any()
                        ? 0
                        : Categories.Max(x => x.Index);
                });
        }

        /// <summary>
        ///     Updates the current index property in the slide class to be the
        ///     highest index of any slide in the database.
        /// </summary>
        public void UpdateCurrentSlideIndex()
        {
            DoTransaction(
                () =>
                {
                    Slide.CurrentIndex = !Categories.Any()
                        ? 0
                        : Categories.Max(x => x.Index);
                });
        }

        internal void DoTransaction(Action action)
        {
            using (var transaction =
                Database.CurrentTransaction ?? Database.BeginTransaction())
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

        internal T DoTransaction<T>(Func<T> func)
        {
            T t;

            using (var transaction =
                Database.CurrentTransaction ?? Database.BeginTransaction())
            {
                try
                {
                    t = func();
                    SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return t;
        }

        private static string GetNotFoundMessage<T>(int id)
        {
            return $"{typeof(T)} with id '{id}' not found.";
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

        public static IList<Category> GetCategories(
            this Role role,
            Func<Category, object> orderByPredicate = null,
            Func<Category, bool> wherePredicate = null)
        {
            return role.Categories.OrderByWhere(
                orderByPredicate,
                wherePredicate);
        }

        public static IList<QuizResult> GetQuizResults(
            this TrainingResult trainingResult)
        {
            return trainingResult.QuizResults.ToList();
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

        public static IList<Slide> GetSlides(
            this IList<Category> categories,
            Func<Slide, object> orderByPredicate = null,
            Func<Slide, bool> wherePredicate = null)
        {
            return categories.SelectMany(
                category => category.GetSlides(
                    orderByPredicate,
                    wherePredicate));
        }

        public static IList<Slide> GetSlides(
            this Role role,
            Func<Category, object> orderCategoriesBy = null,
            Func<Category, bool> categoriesWhere = null,
            Func<Slide, object> orderSlidesBy = null,
            Func<Slide, bool> slidesWhere = null)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.DoTransaction(
                    () => role.GetCategories(orderCategoriesBy, categoriesWhere)
                        .GetSlides(orderSlidesBy, slidesWhere));
            }
        }

        public static IList<TrainingResult> GetTrainingResults(
            this User user,
            Func<TrainingResult, object> orderByPredicate = null,
            Func<TrainingResult, bool> wherePredicate = null)
        {
            return user.TrainingResults.OrderByWhere(
                orderByPredicate,
                wherePredicate);
        }

        public static IList<TrainingResult> GetTrainingResultsDescending(
            this User user,
            Func<TrainingResult, object> orderByPredicate = null,
            Func<TrainingResult, bool> wherePredicate = null)
        {
            return user.TrainingResults.OrderByDescendingWhere(
                orderByPredicate,
                wherePredicate);
        }
    }
}