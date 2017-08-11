using System;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class ApplicationUserManager.
    /// </summary>
    /// <seealso cref="UserManager{TUser}" />
    /// <seealso cref="User" />
    /// TODO Edit XML Comment Template for ApplicationUserManager
    public class ApplicationUserManager : UserManager<User, string>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public ApplicationUserManager(IUserStore<User, string> store)
            : base(store)
        {
        }

        /// <summary>
        ///     Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns>ApplicationUserManager.</returns>
        /// TODO Edit XML Comment Template for Create
        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var manager = new ApplicationUserManager(
                new UserStore<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>(
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
        ///     Gets the role.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;Group&gt;.</returns>
        /// <exception cref="Exception"></exception>
        /// TODO Edit XML Comment Template for GetGroup
        public async Task<Group> GetGroup(string userId)
        {
            var user = await FindByIdAsync(userId);

            if (user.Group == null)
            {
                throw new Exception();
            }

            return user.Group;
        }

        /// <summary>
        /// Sets the company.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="company">The company.</param>
        /// TODO Edit XML Comment Template for SetCompany
        public async void SetCompany(string userId, Company company)
        {
            var user = await FindByIdAsync(userId);
            user.Company = company;
        }
    }
}