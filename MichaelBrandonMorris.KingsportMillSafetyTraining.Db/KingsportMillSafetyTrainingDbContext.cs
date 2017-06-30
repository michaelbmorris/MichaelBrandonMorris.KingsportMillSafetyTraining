using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using MoreLinq;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Db
{
    /// <summary>
    ///     Class KingsportMillSafetyTrainingDbContext. This class
    ///     cannot be inherited.
    /// </summary>
    /// <seealso cref="IdentityDbContext{TUser}" />
    /// <seealso cref="User" />
    /// TODO Edit XML Comment Template for KingsportMillSafetyTrainingDbContext
    public sealed class KingsportMillSafetyTrainingDbContext
        : IdentityDbContext<User>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="KingsportMillSafetyTrainingDbContext" />
        ///     class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public KingsportMillSafetyTrainingDbContext()
            : base("DefaultConnection", false)
        {
            Answers = Set<Answer>();
            Categories = Set<Category>();
            Slides = Set<Slide>();
            TrainingResults = Set<TrainingResult>();
            TrainingRoles = Set<Role>();
        }

        /// <summary>
        ///     Gets or sets the answers.
        /// </summary>
        /// <value>The answers.</value>
        /// TODO Edit XML Comment Template for Answers
        internal DbSet<Answer> Answers
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        /// TODO Edit XML Comment Template for Categories
        internal DbSet<Category> Categories
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the slides.
        /// </summary>
        /// <value>The slides.</value>
        /// TODO Edit XML Comment Template for Slides
        internal DbSet<Slide> Slides
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the training results.
        /// </summary>
        /// <value>The training results.</value>
        /// TODO Edit XML Comment Template for TrainingResults
        internal DbSet<TrainingResult> TrainingResults
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the training roles.
        /// </summary>
        /// <value>The training roles.</value>
        /// TODO Edit XML Comment Template for TrainingRoles
        internal DbSet<Role> TrainingRoles
        {
            get;
            set;
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>KingsportMillSafetyTrainingDbContext.</returns>
        /// TODO Edit XML Comment Template for Create
        public static KingsportMillSafetyTrainingDbContext Create()
        {
            return new KingsportMillSafetyTrainingDbContext();
        }


        /// <summary>
        ///     Adds the quiz result.
        /// </summary>
        /// <param name="trainingResultId">
        ///     The training result
        ///     identifier.
        /// </param>
        /// <param name="questionsCorrect">The questions correct.</param>
        /// <param name="totalQuestions">The total questions.</param>
        /// TODO Edit XML Comment Template for AddQuizResult
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
        ///     Adds the training result.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// TODO Edit XML Comment Template for AddTrainingResult
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
        ///     Creates the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// TODO Edit XML Comment Template for CreateCategory
        public void CreateCategory(Category category)
        {
            DoTransaction(
                () =>
                {
                    Categories.Add(category);
                });
        }

        /// <summary>
        ///     Creates the role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// TODO Edit XML Comment Template for CreateRole
        public void CreateRole(Role role)
        {
            DoTransaction(
                () =>
                {
                    TrainingRoles.Add(role);
                });
        }

        /// <summary>
        ///     Creates the slide.
        /// </summary>
        /// <param name="slide">The slide.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// TODO Edit XML Comment Template for CreateSlide
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
        ///     Deletes the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// TODO Edit XML Comment Template for DeleteCategory
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
        ///     Deletes the role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// TODO Edit XML Comment Template for DeleteRole
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
        ///     Deletes the slide.
        /// </summary>
        /// <param name="slide">The slide.</param>
        /// TODO Edit XML Comment Template for DeleteSlide
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
        ///     Edits the specified t.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// TODO Edit XML Comment Template for Edit`1
        public void Edit<T>(T t)
            where T : class
        {
            DoTransaction(
                () =>
                {
                    Entry(t).State = EntityState.Modified;
                });
        }

        /// <summary>
        ///     Edits the specified slide.
        /// </summary>
        /// <param name="slide">The slide.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// TODO Edit XML Comment Template for Edit
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
        /// <param name="trainingResult">The training result.</param>
        /// TODO Edit XML Comment Template for Edit
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

        /// <summary>
        ///     Gets the categories.
        /// </summary>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;Category&gt;.</returns>
        /// TODO Edit XML Comment Template for GetCategories
        public IList<Category> GetCategories(
            Func<Category, object> orderByPredicate = null,
            Func<Category, bool> wherePredicate = null)
        {
            return Categories.OrderByWhere(orderByPredicate, wherePredicate);
        }

        /// <summary>
        ///     Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category.</returns>
        /// TODO Edit XML Comment Template for GetCategory
        public Category GetCategory(int id)
        {
            return DoTransaction(() => Categories.Find(id));
        }

        /// <summary>
        ///     Gets the role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Role.</returns>
        /// TODO Edit XML Comment Template for GetRole
        public Role GetRole(int id)
        {
            return DoTransaction(() => TrainingRoles.Find(id));
        }

        /// <summary>
        ///     Gets the role.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Role.</returns>
        /// TODO Edit XML Comment Template for GetRole
        public Role GetRole(Func<Role, bool> predicate)
        {
            return DoTransaction(() => TrainingRoles.Single(predicate));
        }

        /// <summary>
        ///     Gets the roles.
        /// </summary>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;Role&gt;.</returns>
        /// TODO Edit XML Comment Template for GetRoles
        public IList<Role> GetRoles(
            Func<Role, object> orderByPredicate = null,
            Func<Role, bool> wherePredicate = null)
        {
            return TrainingRoles.OrderByWhere(orderByPredicate, wherePredicate);
        }

        /// <summary>
        ///     Gets the slide.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Slide.</returns>
        /// TODO Edit XML Comment Template for GetSlide
        public Slide GetSlide(int id)
        {
            return DoTransaction(() => Slides.Find(id));
        }

        /// <summary>
        ///     Gets the slides.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="where">The where.</param>
        /// <returns>IList&lt;Slide&gt;.</returns>
        /// TODO Edit XML Comment Template for GetSlides
        public IList<Slide> GetSlides(
            Func<Slide, object> orderBy = null,
            Func<Slide, bool> where = null)
        {
            return Slides.OrderByWhere(orderBy, where);
        }

        /// <summary>
        ///     Gets the slides.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="where">The where.</param>
        /// <returns>IList&lt;Slide&gt;.</returns>
        /// TODO Edit XML Comment Template for GetSlides
        public IList<Slide> GetSlides(
            int categoryId,
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

        /// <summary>
        ///     Gets the training result.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TrainingResult.</returns>
        /// TODO Edit XML Comment Template for GetTrainingResult
        public TrainingResult GetTrainingResult(int id)
        {
            return DoTransaction(() => TrainingResults.Find(id));
        }

        /// <summary>
        ///     Gets the training results descending.
        /// </summary>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;TrainingResult&gt;.</returns>
        /// TODO Edit XML Comment Template for GetTrainingResultsDescending
        public IList<TrainingResult> GetTrainingResultsDescending(
            Func<TrainingResult, object> orderByPredicate = null,
            Func<TrainingResult, bool> wherePredicate = null)
        {
            return TrainingResults.OrderByDescendingWhere(
                orderByPredicate,
                wherePredicate);
        }

        /// <summary>
        ///     Gets the training results descending.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;TrainingResult&gt;.</returns>
        /// TODO Edit XML Comment Template for GetTrainingResultsDescending
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

        /// <summary>
        ///     Gets the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>User.</returns>
        /// TODO Edit XML Comment Template for GetUser
        public User GetUser(string id)
        {
            return DoTransaction(() => Users.Find(id));
        }

        /// <summary>
        ///     Gets the users.
        /// </summary>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;User&gt;.</returns>
        /// TODO Edit XML Comment Template for GetUsers
        public IList<User> GetUsers(
            Func<User, object> orderByPredicate = null,
            Func<User, bool> wherePredicate = null)
        {
            return Users.OrderByWhere(orderByPredicate, wherePredicate);
        }

        /// <summary>
        ///     Determines whether [is user training result] [the
        ///     specified user
        ///     identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="trainingResultId">
        ///     The training result
        ///     identifier.
        /// </param>
        /// <returns>
        ///     <c>true</c> if [is user training result] [the specified
        ///     user identifier];
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for IsUserTrainingResult
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


        /// <summary>
        /// Pairs the category and role.
        /// </summary>
        /// <param name="categoryAndRoleIds">The category and role ids.</param>
        /// TODO Edit XML Comment Template for PairCategoryAndRole
        public void PairCategoryAndRole(
            (int categoryId, int roleId) categoryAndRoleIds)
        {
            PairCategoryAndRole(
                categoryAndRoleIds.categoryId,
                categoryAndRoleIds.roleId);
        }

        /// <summary>
        ///     Pairs the category and role.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// TODO Edit XML Comment Template for PairCategoryAndRole
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

        /// <summary>
        ///     Reorders the specified categories.
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// TODO Edit XML Comment Template for Reorder
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

        /// <summary>
        ///     Reorders the specified slides.
        /// </summary>
        /// <param name="slides">The slides.</param>
        /// TODO Edit XML Comment Template for Reorder
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
        ///     Reorders the specified roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// TODO Edit XML Comment Template for Reorder
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
        ///     Sets the user latest quiz start date time.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// TODO Edit XML Comment Template for SetUserLatestQuizStartDateTime
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
        ///     Sets the user latest training start date time.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// TODO Edit XML Comment Template for SetUserLatestTrainingStartDateTime
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
        ///     Sets the user role.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// TODO Edit XML Comment Template for SetUserRole
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
        ///     Unpairs the categories and roles.
        /// </summary>
        /// TODO Edit XML Comment Template for UnpairCategoriesAndRoles
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
        ///     Updates the index of the current answer.
        /// </summary>
        /// TODO Edit XML Comment Template for UpdateCurrentAnswerIndex
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
        ///     Updates the index of the current category.
        /// </summary>
        /// TODO Edit XML Comment Template for UpdateCurrentCategoryIndex
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
        ///     Updates the index of the current slide.
        /// </summary>
        /// TODO Edit XML Comment Template for UpdateCurrentSlideIndex
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

        /// <summary>
        ///     Does the transaction.
        /// </summary>
        /// <param name="action">The action.</param>
        /// TODO Edit XML Comment Template for DoTransaction
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

        /// <summary>
        ///     Does the transaction.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">The function.</param>
        /// <returns>T.</returns>
        /// TODO Edit XML Comment Template for DoTransaction`1
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

        /// <summary>
        ///     Gets the not found message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GetNotFoundMessage`1
        private static string GetNotFoundMessage<T>(int id)
        {
            return $"{typeof(T)} with id '{id}' not found.";
        }
    }
}