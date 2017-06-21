using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult AssignRoles(int? id)
        {
            try
            {
                var model = _db.GetAssignRolesViewModel(id);
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

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var category = _db.GetCategory(id.Value);

                if (category == null)
                {
                    return HttpNotFound();
                }

                return View(category);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }          
        }

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var category = _db.GetCategory(id);

                if (category != null)
                {
                    _db.DeleteCategory(category);
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

        [HttpGet]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var model = _db.GetCategoryViewModel(id.Value);

                if (model == null)
                {
                    return HttpNotFound();
                }

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
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return this.CreateError(
                        HttpStatusCode.BadRequest,
                        "Parameter missing.\nType: 'int'\nName: 'id'");
                }

                var category = _db.GetCategory(id.Value);

                if (category == null)
                {
                    return this.CreateError(
                        HttpStatusCode.NotFound,
                        $"Category with id {id} could not be found.");
                }

                return View(category);
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