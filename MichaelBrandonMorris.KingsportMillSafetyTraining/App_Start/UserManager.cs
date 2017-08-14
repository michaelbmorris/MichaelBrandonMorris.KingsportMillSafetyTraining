using System;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class UserManager.
    /// </summary>
    /// <seealso cref="UserManager{TUser}" />
    /// <seealso cref="User" />
    /// TODO Edit XML Comment Template for UserManager
    public class UserManager : UserManager<User, string>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public UserManager(UserStore store)
            : base(store)
        {
            Store = store;
        }

        private new UserStore Store
        {
            get;
        }

        /// <summary>
        ///     Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns>UserManager.</returns>
        /// TODO Edit XML Comment Template for Create
        public static UserManager Create(
            IdentityFactoryOptions<UserManager> options,
            IOwinContext context)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var manager = new UserManager(
                new UserStore(
                    context.Get<KingsportMillSafetyTrainingDbContext>()));

            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            manager.RegisterTwoFactorProvider(
                "Phone Code",
                new PhoneNumberTokenProvider<User>
                {
                    MessageFormat = "Your security code is {0}"
                });

            manager.RegisterTwoFactorProvider(
                "Email Code",
                new EmailTokenProvider<User>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is {0}"
                });

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User>(
                        dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }

        /// <summary>
        ///     Adds the training result.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for AddTrainingResult
        public Task AddTrainingResult(string id)
        {
            return Store.AddTrainingResult(id);
        }

        /// <summary>
        ///     Gets the role.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>Task&lt;Group&gt;.</returns>
        /// <exception cref="Exception"></exception>
        /// TODO Edit XML Comment Template for GetGroup
        public Task<Group> GetGroup(string id)
        {
            return Store.GetGroup(id);
        }

        /// <summary>
        ///     Sets the company.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <param name="company">The company.</param>
        /// TODO Edit XML Comment Template for SetCompany
        public Task SetCompany(string id, Company company)
        {
            return Store.SetCompany(id, company);
        }

        /// <summary>
        ///     Sets the group.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="group">The group.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for SetGroup
        public Task SetGroup(string userId, Group group)
        {
            return Store.SetGroup(userId, group);
        }

        /// <summary>
        ///     Sets the latest quiz start date time.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for SetLatestQuizStartDateTime
        public Task SetLatestQuizStartDateTime(string id)
        {
            return Store.SetLatestQuizStartDateTime(id);
        }

        /// <summary>
        ///     Sets the latest training start date time.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for SetLatestTrainingStartDateTime
        public Task SetLatestTrainingStartDateTime(string id)
        {
            return Store.SetLatestTrainingStartDateTime(id);
        }
    }
}