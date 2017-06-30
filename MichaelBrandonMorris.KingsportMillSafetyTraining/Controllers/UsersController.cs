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
    /// <seealso cref="System.Web.Mvc.Controller" />
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
        ///     Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///     Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            Db.Users.Add(user);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Delete
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var applicationUser = Db.Users.Find(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        /// <summary>
        ///     Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="Exception"></exception>
        /// TODO Edit XML Comment Template for DeleteConfirmed
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var applicationUser = Db.Users.Find(id);

            if (applicationUser == null)
            {
                throw new Exception();
            }

            Db.Users.Remove(applicationUser);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Details
        [HttpGet]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var applicationUser = Db.Users.Find(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var applicationUser = Db.Users.Find(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        /// <summary>
        ///     Edits the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            Db.Edit(user);
            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Index
        [HttpGet]
        public ActionResult Index()
        {
            var model = Db.GetUsers().AsViewModels();
            return View(model);
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