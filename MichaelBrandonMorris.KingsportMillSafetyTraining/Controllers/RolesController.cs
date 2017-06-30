using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.Math;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class RolesController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for RolesController
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
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
        ///     Gets the index of the order by.
        /// </summary>
        /// <value>The index of the order by.</value>
        /// TODO Edit XML Comment Template for OrderByIndex
        private static Func<Role, object> OrderByIndex => role => role.Index;

        /// <summary>
        ///     Assigns the categories.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignCategories
        [HttpGet]
        public ActionResult AssignCategories(int? id)
        {
            var model = id == null
                ? new AssignCategoriesViewModel(
                    Db.GetRoles(),
                    Db.GetCategories().AsViewModels())
                : new AssignCategoriesViewModel(
                    Db.GetRole(id.Value),
                    Db.GetCategories().AsViewModels());

            return View(model);
        }

        /// <summary>
        ///     Assigns the categories.
        /// </summary>
        /// <param name="roleCategories">The role categories.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignCategories
        [HttpPost]
        public ActionResult AssignCategories(IList<int> roleCategories)
        {
            Db.UnpairCategoriesAndRoles();

            if (roleCategories == null)
            {
                return RedirectToAction("Index");
            }

            foreach (var roleCategory in roleCategories)
            {
                Db.PairCategoryAndRole(Cantor.Inverse(roleCategory));
            }

            return RedirectToAction("Index");
        }

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
        ///     Creates the specified role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }

            Db.CreateRole(role);
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = Db.GetRole(id.Value);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        /// <summary>
        ///     Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for DeleteConfirmed
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var role = Db.GetRole(id);

            if (role != null)
            {
                Db.DeleteRole(role);
            }

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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = Db.GetRole(id.Value).AsViewModel();

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        /// <summary>
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = Db.GetRole(id.Value);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        /// <summary>
        ///     Edits the specified role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }

            Db.Entry(role).State = EntityState.Modified;
            Db.SaveChanges();
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
            var model = Db.GetRoles(OrderByIndex).AsViewModels();
            return View(model);
        }

        /// <summary>
        ///     Reorders this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [HttpGet]
        public ActionResult Reorder()
        {
            var model = Db.GetRoles(OrderByIndex).AsViewModels();
            return View(model);
        }

        /// <summary>
        ///     Reorders the specified roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [HttpPost]
        public ActionResult Reorder(IList<Role> roles)
        {
            Db.Reorder(roles);
            return RedirectToAction("Index");
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