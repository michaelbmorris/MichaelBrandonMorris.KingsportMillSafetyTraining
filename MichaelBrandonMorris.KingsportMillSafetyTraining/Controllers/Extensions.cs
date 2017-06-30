using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class Extensions.
    /// </summary>
    /// TODO Edit XML Comment Template for Extensions
    internal static class Extensions
    {
        /// <summary>
        ///     Creates the error.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for CreateError
        internal static ActionResult CreateError(
            this IController controller,
            HttpStatusCode code,
            string message)
        {
            HttpContext.Current.Session["Error"] = (code, message);
            return new HttpStatusCodeResult(code, message);
        }

        /// <summary>
        ///     Gets the error.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.NullReferenceException">
        ///     Stored
        ///     session error is null.
        /// </exception>
        /// TODO Edit XML Comment Template for GetError
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