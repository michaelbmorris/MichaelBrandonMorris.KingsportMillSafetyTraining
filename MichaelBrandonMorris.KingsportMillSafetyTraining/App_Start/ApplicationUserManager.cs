using System;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public async Task<Role> GetRole(string userId)
        {
            var user = await FindByIdAsync(userId);

            if (user.Role == null)
            {
                throw new Exception();
            }

            return user.Role;
        }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(
                new UserStore<User>(
                    context.Get<ApplicationDbContext>()));

            manager.UserValidator =
                new UserValidator<User>(manager)
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
    }
}