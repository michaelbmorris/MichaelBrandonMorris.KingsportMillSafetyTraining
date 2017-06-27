using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     The controller for training results. Accessible only to 
    ///     authenticated users.
    /// </summary>
    [Authorize]
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        ///     Shows the details of the specified training result. If no 
        ///     training result is specified, returns HTTP Bad Request. If the 
        ///     specified training result does not exist, returns HTTP Not 
        ///     Found. If the current user is not an administrator and they do 
        ///     not own the specified training result, returns HTTP Forbidden.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new InvalidOperationException(
                        "Parameter missing.\nType: 'int'\nName: 'id'");
                }

                if (!User.IsInRole("Administrator")
                    && !_db.IsUserTrainingResult(User.GetId(), id.Value))
                {
                    throw new UnauthorizedAccessException(
                        "You do not have permission to view this.");
                }

                var model = _db.GetTrainingResultViewModel(id.Value);
                return View(model);
            }
            catch (UnauthorizedAccessException e)
            {
                return this.CreateError(HttpStatusCode.Forbidden, e.Message);
            }
            catch (InvalidOperationException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return this.CreateError(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        ///     If the current user is an administrator, shows all training 
        ///     results. If the user is not an administrator, redirects to the 
        ///     page for the user's results.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToAction(
                    "UserResults",
                    new
                    {
                        id = User.GetId()
                    });
            }

            var model = _db.GetUserTrainingResultsViewModel();
            return View(model);
        }

        /// <summary>
        ///     Shows the training results for the specified user. If no user 
        ///     is specified, returns HTTP Bad Request. If the current user is 
        ///     not an administrator and not the specified user, returns HTTP 
        ///     Forbidden.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserResults(string id = null)
        {
            try
            {
                if (id == null)
                {
                    throw new InvalidOperationException(
                        "Parameter missing.\nName: 'id'\nType: 'string'");
                }

                if (!User.IsInRole("Administrator")
                    && !User.GetId().Equals(id))
                {
                    throw new UnauthorizedAccessException(
                        "You do not have permission to view this.");
                }

                var model = _db.GetUserTrainingResultsViewModel(id);
                return View(model);
            }
            catch (UnauthorizedAccessException e)
            {
                return this.CreateError(HttpStatusCode.Forbidden, e.Message);
            }
            catch (InvalidOperationException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return this.CreateError(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        ///     Disposes of the database context when the controller is 
        ///     disposed.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}