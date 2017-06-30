using System;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class ErrorController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for ErrorController
    public class ErrorController : Controller
    {
        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Index
        public ActionResult Index()
        {
            try
            {
                var error = this.GetError();

                var model = new ErrorViewModel
                {
                    Code = error.Code,
                    Message = error.Message
                };

                Response.StatusCode = (int) error.Code;
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }
    }
}