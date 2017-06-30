﻿using System;
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
    public class ApplicationUserManager : UserManager<User>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="store">The store.</param>
        /// TODO Edit XML Comment Template for #ctor
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(
                new UserStore<User>(
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

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
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
        /// <returns>Task&lt;Role&gt;.</returns>
        /// <exception cref="Exception"></exception>
        /// TODO Edit XML Comment Template for GetRole
        public async Task<Role> GetRole(string userId)
        {
            var user = await FindByIdAsync(userId);

            if (user.Role == null)
            {
                throw new Exception();
            }

            return user.Role;
        }
    }
}