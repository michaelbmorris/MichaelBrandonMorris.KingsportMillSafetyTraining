using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class SignInManager.
    /// </summary>
    /// <seealso cref="SignInManager{TUser,TKey}" />
    /// <seealso cref="User" />
    /// TODO Edit XML Comment Template for SignInManager
    public class SignInManager : SignInManager<User, string>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="SignInManager" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="authenticationManager">
        ///     The authentication
        ///     manager.
        /// </param>
        /// TODO Edit XML Comment Template for #ctor
        public SignInManager(
            UserManager userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        /// <summary>
        /// Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns>SignInManager.</returns>
        /// <exception cref="ArgumentNullException">context</exception>
        /// TODO Edit XML Comment Template for Create
        public static SignInManager Create(
            IdentityFactoryOptions<SignInManager> options,
            IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return new SignInManager(
                context.GetUserManager<UserManager>(),
                context.Authentication);
        }

        /// <summary>
        ///     Called to generate the ClaimsIdentity for the user,
        ///     override to add additional claims before SignIn
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;ClaimsIdentity&gt;.</returns>
        /// TODO Edit XML Comment Template for CreateUserIdentityAsync
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.GenerateUserIdentityAsync(
                (UserManager) UserManager);
        }
    }
}