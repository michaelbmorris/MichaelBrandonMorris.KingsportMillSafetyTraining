using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers.Helpers
{
    /// <summary>
    ///     Class ChallengeResult.
    /// </summary>
    /// <seealso cref="HttpUnauthorizedResult" />
    /// TODO Edit XML Comment Template for ChallengeResult
    internal class ChallengeResult : HttpUnauthorizedResult
    {
        /// <summary>
        ///     The XSRF key
        /// </summary>
        /// TODO Edit XML Comment Template for XsrfKey
        private const string XsrfKey = "XsrfId";

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ChallengeResult" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="redirectUri">The redirect URI.</param>
        /// TODO Edit XML Comment Template for #ctor
        public ChallengeResult(string provider, string redirectUri)
            : this(provider, redirectUri, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ChallengeResult" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <param name="userId">The user identifier.</param>
        /// TODO Edit XML Comment Template for #ctor
        public ChallengeResult(
            string provider,
            string redirectUri,
            string userId)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
            UserId = userId;
        }

        /// <summary>
        ///     Gets or sets the login provider.
        /// </summary>
        /// <value>The login provider.</value>
        /// TODO Edit XML Comment Template for LoginProvider
        public string LoginProvider
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the redirect URI.
        /// </summary>
        /// <value>The redirect URI.</value>
        /// TODO Edit XML Comment Template for RedirectUri
        public string RedirectUri
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        /// TODO Edit XML Comment Template for UserId
        public string UserId
        {
            get;
            set;
        }

        /// <summary>
        ///     Enables processing of the result of an action method by
        ///     a custom type that inherits from the
        ///     <see cref="T:System.Web.Mvc.ActionResult" /> class.
        /// </summary>
        /// <param name="context">
        ///     The context in which the result is executed. The
        ///     context information includes the controller, HTTP
        ///     content, request context, and route data.
        /// </param>
        /// TODO Edit XML Comment Template for ExecuteResult
        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = RedirectUri
            };
            if (UserId != null)
            {
                properties.Dictionary[XsrfKey] = UserId;
            }
            context.HttpContext.GetOwinContext()
                .Authentication.Challenge(properties, LoginProvider);
        }
    }
}