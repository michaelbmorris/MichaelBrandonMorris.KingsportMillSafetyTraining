using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class ResultsController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for ResultsController
    [Authorize]
    public class ResultsController : Controller
    {
        /// <summary>
        ///     The database
        /// </summary>
        /// TODO Edit XML Comment Template for Db
        private KingsportMillSafetyTrainingDbContext Db
        {
            get;
        } = new KingsportMillSafetyTrainingDbContext();

        /// <summary>
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nType: 'int'\nName: 'id'
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     You do not
        ///     have permission to view this.
        /// </exception>
        /// TODO Edit XML Comment Template for Details
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
                    && !Db.IsUserTrainingResult(User.GetId(), id.Value))
                {
                    throw new UnauthorizedAccessException(
                        "You do not have permission to view this.");
                }

                var model = Db.GetTrainingResult(id.Value).AsViewModel();
                return View(model);
            }
            catch (UnauthorizedAccessException e)
            {
                return this.CreateError(HttpStatusCode.Forbidden, e);
            }
            catch (InvalidOperationException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e);
            }
            catch (KeyNotFoundException e)
            {
                return this.CreateError(HttpStatusCode.NotFound, e);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Index
        [HttpGet]
        public ActionResult Index()
        {
            if (!User.IsInRole("Administrator") && !User.IsInRole("Owner") && User.IsInRole("Security") && User.IsInRole("Supervisor"))
            {
                return RedirectToAction(
                    "UserResults",
                    new
                    {
                        id = User.GetId()
                    });
            }

            var model = Db.GetUser(User.GetId()).AsViewModel();
            return View(model);
        }

        /// <summary>
        ///     Users the results.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nName: 'id'\nType: 'string'
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     You do not
        ///     have permission to view this.
        /// </exception>
        /// TODO Edit XML Comment Template for UserResults
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

                var model = new UserTrainingResultsViewModel(
                    Db.GetUser(id).AsViewModel(),
                    Db.GetTrainingResultsDescending().AsViewModels());
                return View(model);
            }
            catch (UnauthorizedAccessException e)
            {
                return this.CreateError(HttpStatusCode.Forbidden, e);
            }
            catch (InvalidOperationException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e);
            }
            catch (KeyNotFoundException e)
            {
                return this.CreateError(HttpStatusCode.NotFound, e);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Releases unmanaged resources and optionally releases
        ///     managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources;
        ///     false to release only unmanaged resources.
        /// </param>
        /// TODO Edit XML Comment Template for Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}