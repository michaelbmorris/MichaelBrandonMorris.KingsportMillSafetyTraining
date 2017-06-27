using System.Security.Claims;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     The sign-in manager for the application.
    /// </summary>
    public class ApplicationSignInManager
        : SignInManager<User, string>
    {
        /// <summary>
        ///     Creates a new instance of the sign-in manager with the 
        ///     specified <see cref="ApplicationUserManager"/> and 
        ///     <see cref="IAuthenticationManager"/>.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="authenticationManager"></param>
        public ApplicationSignInManager(
            ApplicationUserManager userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        /// <summary>
        ///     Invokes the <see cref="ApplicationSignInManager"/> constructor 
        ///     with the current <see cref="IOwinContext"/>'s 
        ///     <see cref="ApplicationUserManager"/> and 
        ///     <see cref="IAuthenticationManager"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ApplicationSignInManager Create(
            IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            return new ApplicationSignInManager(
                context.GetUserManager<ApplicationUserManager>(),
                context.Authentication);
        }

        /// <summary>
        ///     Creates a <see cref="ClaimsIdentity"/> asynchronously. 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(
            User user)
        {
            return user.GenerateUserIdentityAsync(
                (ApplicationUserManager) UserManager);
        }
    }
}