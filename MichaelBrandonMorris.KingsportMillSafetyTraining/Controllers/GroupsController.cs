using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.Math;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class GroupsController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for GroupsController
    public class GroupsController : Controller
    {
        /// <summary>
        ///     Gets the index of the order by.
        /// </summary>
        /// <value>The index of the order by.</value>
        /// TODO Edit XML Comment Template for OrderByIndex
        private static Func<Group, object> OrderByIndex => role => role.Index;

        /// <summary>
        ///     The database
        /// </summary>
        /// TODO Edit XML Comment Template for Db
        private KingsportMillSafetyTrainingDbContext Db
        {
            get;
        } = new KingsportMillSafetyTrainingDbContext();

        /// <summary>
        ///     Assigns the categories.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignCategories
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult AssignCategories(int? id)
        {
            var model = id == null
                ? new AssignCategoriesViewModel(
                    Db.GetGroups(),
                    Db.GetCategories().AsViewModels())
                : new AssignCategoriesViewModel(
                    Db.GetGroup(id.Value),
                    Db.GetCategories().AsViewModels());

            return View(model);
        }

        /// <summary>
        ///     Assigns the categories.
        /// </summary>
        /// <param name="groupCategories">The role categories.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignCategories
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        public ActionResult AssignCategories(IList<int> groupCategories)
        {
            Db.UnpairCategoriesAndGroups();

            if (groupCategories == null)
            {
                return RedirectToAction("Index");
            }

            foreach (var groupCategory in groupCategories)
            {
                Db.PairGroupAndCategory(Cantor.Inverse(groupCategory));
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///     Creates the specified role.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Db.CreateGroup(
                new Group
                {
                    Title = model.Title,
                    Description = model.Description,
                    Index = ++Group.CurrentIndex,
                    Question = model.Question
                });

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Delete
        [Authorize(Roles = "Owner, Administrator")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                var model = Db.GetGroup(id).AsViewModel();
                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        ///     Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for DeleteConfirmed
        [Authorize(Roles = "Owner, Administrator")]
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                Db.DeleteGroup(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException e)
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
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Details
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
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

                var model = Db.GetGroup(id.Value).AsViewModel();

                if (model == null)
                {
                    return HttpNotFound();
                }

                return View(model);
            }
            catch (ArgumentNullException e)
            {
                return this.CreateError(HttpStatusCode.NotFound, e);
            }
            catch (InvalidOperationException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e);
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
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new InvalidOperationException(
                        "Parameter missing.\nType: 'int'\nName: 'id'");
                }

                var model = Db.GetGroup(id.Value).AsViewModel();
                return View(model);
            }
            catch (ArgumentNullException e)
            {
                return this.CreateError(HttpStatusCode.NotFound, e);
            }
            catch (InvalidOperationException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Edits the specified role.
        /// </summary>
        /// <param name="model">The role.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Db.Edit(
                    new Group
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Description = model.Description,
                        Question = model.Question,
                        Index = model.Index
                    });

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
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Index()
        {
            var model = Db.GetGroups(OrderByIndex).AsViewModels();
            return View(model);
        }

        /// <summary>
        ///     Reorders this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Reorder()
        {
            var model = Db.GetGroups(OrderByIndex).AsViewModels();
            return View(model);
        }

        /// <summary>
        ///     Reorders the specified roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        public ActionResult Reorder(IList<Group> roles)
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