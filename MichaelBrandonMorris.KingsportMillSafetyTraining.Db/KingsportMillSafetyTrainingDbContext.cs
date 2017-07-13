using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
            Companies = Set<Company>();
            Slides = Set<Slide>();
            TrainingResults = Set<TrainingResult>();
            Groups = Set<Group>();
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

        internal DbSet<Company> Companies
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the training groups.
        /// </summary>
        /// <value>The training groups.</value>
        /// TODO Edit XML Comment Template for Groups
        internal DbSet<Group> Groups
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
                            Group = user.Group
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

        public void CreateCompany(Company company)
        {
            DoTransaction(
                () =>
                {
                    Companies.Add(company);
                });
        }

        /// <summary>
        ///     Creates the role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// TODO Edit XML Comment Template for CreateGroup
        public void CreateGroup(Group role)
        {
            DoTransaction(
                () =>
                {
                    Groups.Add(role);
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
            Debug.WriteLine("creating slide");
            DoTransaction(
                () =>
                {
                    var category = Categories.Find(categoryId);

                    slide.Category =
                        category
                        ?? throw new KeyNotFoundException(
                            $"Category with id '{categoryId}' not found.");

                    slide.Index = category.Slides.Count;
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

            UpdateCurrentCategoryIndex();
        }

        public void DeleteCompany(int? id)
        {
            DoTransaction(
                () =>
                {
                    if (id == null)
                    {
                        throw new ArgumentNullException(nameof(id));
                    }

                    var company = Companies.Find(id);

                    if (company == null)
                    {
                        throw new KeyNotFoundException(
                            "Company with id '{id}' not found.");
                    }

                    Companies.Attach(company);
                    Companies.Remove(company);
                });
        }

        /// <summary>
        ///     Deletes the role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// TODO Edit XML Comment Template for DeleteGroup
        public void DeleteGroup(int? id)
        {
            DoTransaction(
                () =>
                {
                    if (id == null)
                    {
                        throw new ArgumentNullException(nameof(id));
                    }

                    var role = Groups.Find(id);

                    if (role == null)
                    {
                        throw new KeyNotFoundException(
                            $"Group with id '{id}' not found.");
                    }

                    Groups.Attach(role);
                    Groups.Remove(role);
                });

            UpdateCurrentGroupIndex();
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

        public void Edit(User user, int companyId)
        {
            DoTransaction(
                () =>
                {
                    user.Company = Companies.Find(companyId)
                                   ?? throw new KeyNotFoundException(
                                       $"Company with id '{companyId}' not found.");
                    Edit(user);
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// TODO Edit XML Comment Template for GetCategory
        public Category GetCategory(int? id)
        {
            return DoTransaction(
                () =>
                {
                    if (id == null)
                    {
                        throw new ArgumentNullException(nameof(id));
                    }

                    var category = Categories.Find(id);

                    if (category == null)
                    {
                        throw new KeyNotFoundException(
                            $"Category with id '{id}' not found.");
                    }

                    return category;
                });
        }

        public IList<Company> GetCompanies(
            Func<Company, object> orderBy = null,
            Func<Company, bool> where = null)
        {
            return DoTransaction(() => Companies.OrderByWhere(orderBy, where));
        }

        public Company GetCompany(int? id)
        {
            return DoTransaction(
                () =>
                {
                    if (id == null)
                    {
                        throw new ArgumentNullException(nameof(id));
                    }

                    var company = Companies.Find(id);

                    if (company == null)
                    {
                        throw new KeyNotFoundException(
                            $"Company with id '{id}' not found.");
                    }

                    return company;
                });
        }

        /// <summary>
        ///     Gets the role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Group.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// TODO Edit XML Comment Template for GetGroup
        public Group GetGroup(int? id)
        {
            return DoTransaction(
                () =>
                {
                    if (id == null)
                    {
                        throw new ArgumentNullException(nameof(id));
                    }

                    var group = Groups.Find(id);

                    if (group == null)
                    {
                        throw new KeyNotFoundException(
                            $"Group with id '{id}' not found.");
                    }

                    return group;
                });
        }

        /// <summary>
        ///     Gets the role.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Group.</returns>
        /// TODO Edit XML Comment Template for GetGroup
        public Group GetGroup(Func<Group, bool> predicate)
        {
            return DoTransaction(() => Groups.Single(predicate));
        }

        /// <summary>
        ///     Gets the groups.
        /// </summary>
        /// <param name="orderByPredicate">The order by predicate.</param>
        /// <param name="wherePredicate">The where predicate.</param>
        /// <returns>IList&lt;Group&gt;.</returns>
        /// TODO Edit XML Comment Template for GetGroups
        public IList<Group> GetGroups(
            Func<Group, object> orderByPredicate = null,
            Func<Group, bool> wherePredicate = null)
        {
            return Groups.OrderByWhere(orderByPredicate, wherePredicate);
        }

        /// <summary>
        ///     Gets the slide.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Slide.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// TODO Edit XML Comment Template for GetSlide
        public Slide GetSlide(int? id)
        {
            return DoTransaction(
                () =>
                {
                    if (id == null)
                    {
                        throw new ArgumentNullException(nameof(id));
                    }

                    var slide = Slides.Find(id);

                    if (slide == null)
                    {
                        throw new KeyNotFoundException(
                            $"Slide with id '{id}' not found.");
                    }

                    return slide;
                });
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
            return DoTransaction(() => Slides.OrderByWhere(orderBy, where));
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
                    if (userId == null)
                    {
                        throw new ArgumentNullException(nameof(userId));
                    }

                    var user = Users.Find(userId);

                    if (user == null)
                    {
                        throw new KeyNotFoundException(
                            $"User with id '{userId}' not found.");
                    }

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
        ///     Pairs the category and role.
        /// </summary>
        /// <param name="categoryAndRoleIds">The category and role ids.</param>
        /// TODO Edit XML Comment Template for PairCategoryAndGroup
        public void PairCategoryAndGroup(
            (int CategoryId, int GroupId) categoryAndGroupIds)
        {
            PairCategoryAndGroup(
                categoryAndGroupIds.CategoryId,
                categoryAndGroupIds.GroupId);
        }

        /// <summary>
        ///     Pairs the category and role.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="groupId">The role identifier.</param>
        /// TODO Edit XML Comment Template for PairCategoryAndGroup
        public void PairCategoryAndGroup(int categoryId, int groupId)
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

                    var role = Groups.Find(groupId);

                    if (role == null)
                    {
                        throw new KeyNotFoundException(
                            $"Group with id '{groupId}' not found.");
                    }

                    category.Groups.Add(role);
                    role.Categories.Add(category);
                });
        }

        public void PairGroupAndCategory(
            (int GroupId, int CategoryId) groupAndCategoryIds)
        {
            PairGroupAndCategory(
                groupAndCategoryIds.GroupId,
                groupAndCategoryIds.CategoryId);
        }

        public void PairGroupAndCategory(int groupId, int categoryId)
        {
            DoTransaction(
                () =>
                {
                    var role = Groups.Find(groupId);

                    if (role == null)
                    {
                        throw new KeyNotFoundException(
                            $"Group with id '{groupId}' not found.");
                    }

                    var category = Categories.Find(categoryId);

                    if (category == null)
                    {
                        throw new KeyNotFoundException(
                            $"Category with id '{categoryId}' not found.");
                    }

                    role.Categories.Add(category);
                    category.Groups.Add(role);
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

            UpdateCurrentCategoryIndex();
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
        ///     Reorders the specified groups.
        /// </summary>
        /// <param name="groups">The groups.</param>
        /// TODO Edit XML Comment Template for Reorder
        public void Reorder(IList<Group> groups)
        {
            DoTransaction(
                () =>
                {
                    foreach (var group in groups)
                    {
                        var roleToEdit = Groups.Find(group.Id);

                        if (roleToEdit == null)
                        {
                            throw new KeyNotFoundException(
                                GetNotFoundMessage<Group>(group.Id));
                        }

                        roleToEdit.Index = group.Index;
                    }
                });
        }

        public void SetUserCompany(string userId, int companyId)
        {
            DoTransaction(
                () =>
                {
                    var user = Users.Find(userId);
                    user.Company = Companies.Find(companyId)
                                   ?? throw new KeyNotFoundException(
                                       $"Company with id '{companyId}' not found.");
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

                    user.Group = roleId == null
                        ? Groups.MaxBy(x => x.Index)
                        : Groups.Find(roleId);
                });
        }

        /// <summary>
        ///     Unpairs the categories and groups.
        /// </summary>
        /// TODO Edit XML Comment Template for UnpairCategoriesAndGroups
        public void UnpairCategoriesAndGroups()
        {
            DoTransaction(
                () =>
                {
                    foreach (var category in GetCategories())
                    foreach (var group in category.GetGroups())
                    {
                        category.Groups.Remove(group);
                    }

                    foreach (var group in GetGroups())
                    foreach (var category in group.GetCategories())
                    {
                        group.Categories.Remove(category);
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
                    Answer.CurrentIndex = !Answers.Any()
                        ? 0
                        : Answers.Max(x => x.Index);
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
        ///     Updates the index of the current role.
        /// </summary>
        /// TODO Edit XML Comment Template for UpdateCurrentGroupIndex
        public void UpdateCurrentGroupIndex()
        {
            DoTransaction(
                () =>
                {
                    Group.CurrentIndex =
                        !Groups.Any() ? 0 : Groups.Max(x => x.Index);
                });
        }

        /// <summary>
        ///     Does the transaction.
        /// </summary>
        /// <param name="action">The action.</param>
        /// TODO Edit XML Comment Template for DoTransaction
        internal void DoTransaction(Action action)
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
                    throw;
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

            using (var transaction = Database.BeginTransaction())
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