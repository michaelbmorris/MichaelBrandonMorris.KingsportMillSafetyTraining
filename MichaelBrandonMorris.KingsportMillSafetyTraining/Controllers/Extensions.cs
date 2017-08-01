using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
#if DEBUG
using System.Diagnostics;

#endif

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class Extensions.
    /// </summary>
    /// TODO Edit XML Comment Template for Extensions
    internal static class Extensions
    {
#if DEBUG
        /// <summary>
        ///     Creates the error.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="code">The code.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for CreateError
        internal static ActionResult CreateError(
            this IController controller,
            HttpStatusCode code,
            Exception exception)
        {
            Debug.WriteLine(exception.Message);
            Debug.WriteLine(exception.StackTrace);

            var dbEntityValidationException =
                exception as DbEntityValidationException;

            ExceptionDispatchInfo exceptionDispatchInfo;

            if (dbEntityValidationException == null)
            {
                exceptionDispatchInfo =
                    ExceptionDispatchInfo.Capture(exception);

                exceptionDispatchInfo.Throw();
            }

            foreach (var entityValidationError in dbEntityValidationException
                .EntityValidationErrors)
            {
                foreach (var validationError in entityValidationError
                    .ValidationErrors)
                {
                    Debug.WriteLine(validationError.ErrorMessage);
                }
            }

            exceptionDispatchInfo =
                ExceptionDispatchInfo.Capture(dbEntityValidationException);

            exceptionDispatchInfo.Throw();
            throw new Exception("Error handling failed.");
        }
#else /// <summary>
///     Creates the error.
/// </summary>
/// <param name="controller">The controller.</param>
/// <param name="code">The code.</param>
/// <param name="exception">The exception.</param>
/// <returns>ActionResult.</returns>
/// TODO Edit XML Comment Template for CreateError
        internal static ActionResult CreateError(
            this IController controller,
            HttpStatusCode code,
            Exception exception)
        {
            HttpContext.Current.Session["Error"] = (code, exception.Message);
            return new HttpStatusCodeResult(code);
        }
#endif


        /// <summary>
        ///     Gets the error.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="NullReferenceException">
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

        internal static bool IsEmployeeTrainingResult(
            this IPrincipal user,
            int id)
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                return db.GetUser(user.GetId())
                    .Company.Employees
                    .SelectMany(employee => employee.TrainingResults)
                    .Any(trainingResult => trainingResult.Id == id);
            }
        }

        public static bool IsOwnTrainingResult(this IPrincipal user, int id)
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                return db.GetUser(user.GetId())
                    .TrainingResults
                    .Any(trainingResult => trainingResult.Id == id);
            }
        }

        internal static bool IsEmployee(this IPrincipal user, string id)
        {
            using (var db = new KingsportMillSafetyTrainingDbContext())
            {
                return db.GetUser(user.GetId())
                    .Company.Employees.Any(employee => employee.Id == id);
            }
        }
    }
}