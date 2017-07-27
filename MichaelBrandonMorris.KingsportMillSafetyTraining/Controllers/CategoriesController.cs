using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers.Helpers;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.Math;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class CategoriesController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for CategoriesController
    public class CategoriesController : Controller
    {
        /// <summary>
        ///     Gets the index of the order by.
        /// </summary>
        /// <value>The index of the order by.</value>
        /// TODO Edit XML Comment Template for OrderByIndex
        private static Func<Category, object> OrderByIndex => category =>
            category.Index;

        /// <summary>
        ///     The database
        /// </summary>
        /// TODO Edit XML Comment Template for Db
        private KingsportMillSafetyTrainingDbContext Db
        {
            get;
        } = new KingsportMillSafetyTrainingDbContext();

        /// <summary>
        ///     Assigns the roles.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignGroups
        [HttpGet]
        [AuthorizeDynamic]
        public ActionResult AssignGroups(int? id)
        {
            try
            {
                var model = id == null
                    ? new AssignGroupsViewModel(
                        Db.GetCategories(),
                        Db.GetGroups().AsViewModels())
                    : new AssignGroupsViewModel(
                        Db.GetCategory(id.Value),
                        Db.GetGroups().AsViewModels());

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
        ///     Assigns the roles.
        /// </summary>
        /// <param name="categoryGroups">The category roles.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignGroups
        [HttpPost]
        public ActionResult AssignGroups(IList<int> categoryGroups)
        {
            try
            {
                Db.UnpairCategoriesAndGroups();

                if (categoryGroups == null)
                {
                    return RedirectToAction("Index");
                }

                foreach (var categoryGroup in categoryGroups)
                {
                    Db.PairCategoryAndGroup(Cantor.Inverse(categoryGroup));
                }

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
        ///     Creates this instance.
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
                    e);
            }
        }

        /// <summary>
        ///     Creates the specified category.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Db.CreateCategory(
                    new Category
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Index = ++Category.CurrentIndex
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
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nType: 'int'\nName: 'id'
        /// </exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// TODO Edit XML Comment Template for Delete
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {

                var model = Db.GetCategory(id).AsViewModel();
                return View(model);
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
        ///     Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nType: 'int'\nName: 'id'
        /// </exception>
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
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nType: 'int'\nName: 'id'
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

                var model = Db.GetCategory(id.Value).AsViewModel();
                return View(model);
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
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nType: 'int'\nName: 'id'
        /// </exception>
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

                var model = Db.GetCategory(id.Value).AsViewModel();
                return View(model);
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
        ///     Edits the specified category.
        /// </summary>
        /// <param name="model">The category.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Db.Edit(new Category
                {
                    Title = model.Title,
                    Description = model.Description,
                    Index = model.Index,
                    Id = model.Id,
                    Groups = model.Roles,
                    Slides = model.Slides
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
                    e);
            }
        }

        /// <summary>
        ///     Reorders this instance.
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
                    e);
            }
        }

        /// <summary>
        ///     Reorders the specified categories.
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