using System;
using System.Net;
using System.Web.Mvc;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class AuthorizeAttribute.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.AuthorizeAttribute" />
    /// TODO Edit XML Comment Template for AuthorizeAttribute
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Method,
        AllowMultiple = true)]
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        /// <summary>
        ///     Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">
        ///     Encapsulates the information for using
        ///     <see cref="T:System.Web.Mvc.AuthorizeAttribute" />. The
        ///     <paramref name="filterContext" /> object contains the
        ///     controller, HTTP context, request context, action
        ///     result, and route data.
        /// </param>
        /// TODO Edit XML Comment Template for HandleUnauthorizedRequest
        protected override void HandleUnauthorizedRequest(
            AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result =
                    new HttpStatusCodeResult((int) HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}