using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    internal static class Extensions
    {
        /// <summary>
        ///     Creates a new error, stores it in the session data, and then
        ///     returns the error as a <see cref="HttpStatusCodeResult" />.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        internal static ActionResult CreateError(
            this IController controller,
            HttpStatusCode code,
            string message)
        {
            HttpContext.Current.Session["Error"] = (code, message);
            return new HttpStatusCodeResult(code, message);
        }

        /// <summary>
        ///     Gets the stored error.
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        internal static (HttpStatusCode Code, string Message) GetError(
            this IController controller)
        {
            var error = HttpContext.Current.Session["Error"];

            if (error == null)
            {
                throw new NullReferenceException(
                    "Stored session error is null.");
            }

            HttpContext.Current.Session["Error"] = null;
            return ((HttpStatusCode, string)) error;
        }
    }
}