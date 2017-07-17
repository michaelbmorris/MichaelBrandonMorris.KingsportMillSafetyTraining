using System;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class UsersController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for UsersController
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        /// <summary>
        ///     Gets the database.
        /// </summary>
        /// <value>The database.</value>
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
        /// TODO Edit XML Comment Template for Details
        [HttpGet]
        public ActionResult Details(string id)
        {
            try
            {
                var model = Db.GetUser(id).AsViewModel();
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                var model = Db.GetUser(id).AsViewModel();
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        [HttpGet]
        public ActionResult ChangeRole(string id)
        {
            try
            {
                var model = Db.GetUser(id).AsViewModel();
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        [HttpPost]
        public ActionResult ChangeRole(string[] roleIds)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Edits the specified user.
        /// </summary>
        /// <param name="model">The user.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Db.Edit(
                    new User
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        Id = model.Id,
                        LastName = model.LastName,
                        MiddleName = model.MiddleName,
                        PhoneNumber = model.PhoneNumber,
                        UserName = model.UserName,
                        
                    },
                    model.CompanyId);

                return RedirectToAction("Index");
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
            try
            {
                return View();
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