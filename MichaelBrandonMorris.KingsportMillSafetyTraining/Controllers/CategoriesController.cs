using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     The controller for categories. Accessible only to administrators.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        ///     Gets the view to assign roles to the specified category. If no 
        ///     category is specified, gets the assign roles model for all 
        ///     categories.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AssignRoles(int? id)
        {
            try
            {
                var model = _db.GetAssignRolesViewModel(id.Value);
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
        ///     Posts the results of assigning roles to categories. Unpairs all 
        ///     categories and roles, and then pairs the assigned categories 
        ///     and roles.
        /// </summary>
        /// <param name="categoryRoles"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AssignRoles(IList<int> categoryRoles)
        {
            try
            {
                _db.UnpairCategoriesAndRoles();

                if (categoryRoles == null)
                {
                    return RedirectToAction("Index");
                }

                foreach (var categoryRole in categoryRoles)
                {
                    var cantorInversePair = Helpers.CantorInverse(categoryRole);
                    var categoryId = cantorInversePair.Item1;
                    var roleId = cantorInversePair.Item2;
                    _db.PairCategoryAndRole(categoryId, roleId);
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
        ///     Gets the view to create a new category.
        /// </summary>
        /// <returns></returns>
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
        ///     Posts the new category. If the new category is valid, adds it 
        ///     to the database.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
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

                _db.CreateCategory(category);
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
        ///     Gets the view to delete the specified category. If no category 
        ///     is specified, returns HTTP Bad Request. If the specified 
        ///     category is not found, returns HTTP Not Found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

                var category = _db.GetCategory(id.Value);

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
        ///     Posts the delete confirmation for the specified category. If no 
        ///     category is specified, returns HTTP Bad Request. If the 
        ///     specified category is not found, returns HTTP Not Found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

                var category = _db.GetCategory(id.Value);

                if (category == null)
                {
                    throw new KeyNotFoundException(
                        $"Category with Id {id} not found.");
                }

                _db.DeleteCategory(category);
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
        ///     Returns the detailed view for the specified category. If no 
        ///     category is specified, returns HTTP Bad Request. If the 
        ///     specified category is not found, returns HTTP Not Found.
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

                var model = _db.GetCategoryViewModel(id.Value);
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

                var model = _db.GetCategoryViewModel(id.Value);
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

                _db.Edit(category);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var model = _db.GetCategoryViewModels(x => x.Index);
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        [HttpGet]
        public ActionResult Reorder()
        {
            try
            {
                var model = _db.GetCategoryViewModels(x => x.Index);
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        [HttpPost]
        public ActionResult Reorder(IList<Category> categories)
        {
            try
            {
                _db.Reorder(categories);
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