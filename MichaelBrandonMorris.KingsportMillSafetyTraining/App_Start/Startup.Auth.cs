using System;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class Startup.
    /// </summary>
    /// TODO Edit XML Comment Template for Startup
    public partial class Startup
    {
        /// <summary>
        ///     Configures the authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        /// TODO Edit XML Comment Template for ConfigureAuth
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(
                KingsportMillSafetyTrainingDbContext.Create);

            app.CreatePerOwinContext<ApplicationUserManager>(
                ApplicationUserManager.Create);

            app.CreatePerOwinContext<ApplicationSignInManager>(
                ApplicationSignInManager.Create);

            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType =
                        DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/Account/Login"),
                    Provider = new CookieAuthenticationProvider
                    {
                        OnValidateIdentity =
                            SecurityStampValidator
                                .OnValidateIdentity<ApplicationUserManager, User
                                >(
                                    TimeSpan.FromMinutes(30),
                                    (manager, user) => user
                                        .GenerateUserIdentityAsync(manager))
                    }
                });

            app.UseExternalSignInCookie(
                DefaultAuthenticationTypes.ExternalCookie);

            app.UseTwoFactorSignInCookie(
                DefaultAuthenticationTypes.TwoFactorCookie,
                TimeSpan.FromMinutes(5));

            app.UseTwoFactorRememberBrowserCookie(
                DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }
    }
}