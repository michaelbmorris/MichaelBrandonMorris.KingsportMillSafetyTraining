using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    /// Class CategoriesController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for CategoriesController
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        /// TODO Edit XML Comment Template for Db
        private KingsportMillSafetyTrainingDbContext Db
        {
            get;
        } = new KingsportMillSafetyTrainingDbContext();

        /// <summary>
        /// Gets the index of the order by.
        /// </summary>
        /// <value>The index of the order by.</value>
        /// TODO Edit XML Comment Template for OrderByIndex
        private static Func<Category, object> OrderByIndex => category =>
            category.Index;

        /// <summary>
        /// Assigns the roles.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignRoles
        [HttpGet]
        public ActionResult AssignRoles(int? id)
        {
            try
            {
                var model = id == null
                    ? new AssignRolesViewModel(
                        Db.GetCategories(),
                        Db.GetRoles().AsViewModels())
                    : new AssignRolesViewModel(
                        Db.GetCategory(id.Value),
                        Db.GetRoles().AsViewModels());

                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Assigns the roles.
        /// </summary>
        /// <param name="categoryRoles">The category roles.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignRoles
        [HttpPost]
        public ActionResult AssignRoles(IList<int> categoryRoles)
        {
            try
            {
                Db.UnpairCategoriesAndRoles();

                if (categoryRoles == null)
                {
                    return RedirectToAction("Index");
                }

                foreach (var categoryRole in categoryRoles)
                {
                    Db.PairCategoryAndRole(Math.Cantor.Inverse(categoryRole));
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Creates the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }

                Db.CreateCategory(category);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">Parameter missing.\nType: 'int'\nName: 'id'</exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// TODO Edit XML Comment Template for Delete
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new InvalidOperationException(
                        "Parameter missing.\nType: 'int'\nName: 'id'");
                }

                var category = Db.GetCategory(id.Value);

                if (category == null)
                {
                    throw new KeyNotFoundException(
                        $"Category with Id {id} not found.");
                }

                return View(category);
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
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">Parameter missing.\nType: 'int'\nName: 'id'</exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// TODO Edit XML Comment Template for DeleteConfirmed
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new InvalidOperationException(
                        "Parameter missing.\nType: 'int'\nName: 'id'");
                }

                var category = Db.GetCategory(id.Value);

                if (category == null)
                {
                    throw new KeyNotFoundException(
                        $"Category with Id {id} not found.");
                }

                Db.DeleteCategory(category);
                return RedirectToAction("Index");
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
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">Parameter missing.\nType: 'int'\nName: 'id'</exception>
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

                var model = Db.GetCategory(id.Value).AsViewModel();
                return View(model);
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
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">Parameter missing.\nType: 'int'\nName: 'id'</exception>
        /// TODO Edit XML Comment Template for Edit
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

                var model = Db.GetCategory(id.Value);
                return View(model);
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
        /// Edits the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }

                Db.Edit(category);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Index
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var model = Db.GetCategories(x => x.Index).AsViewModels();
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Reorders this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [HttpGet]
        public ActionResult Reorder()
        {
            try
            {
                var model = Db.GetCategories(OrderByIndex).AsViewModels();
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Reorders the specified categories.
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [HttpPost]
        public ActionResult Reorder(IList<Category> categories)
        {
            try
            {
                Db.Reorder(categories);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
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