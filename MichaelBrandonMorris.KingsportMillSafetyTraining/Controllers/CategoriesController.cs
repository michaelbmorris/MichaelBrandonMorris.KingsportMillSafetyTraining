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
            var model = _db.GetAssignRolesViewModel(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult AssignRoles(IList<int> categoryRoles)
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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _db.CreateCategory(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
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

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = _db.GetCategory(id);

            if (category != null)
            {
                _db.DeleteCategory(category);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
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

        [HttpGet]
        public ActionResult Edit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _db.Edit(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _db.GetCategoryViewModels(x => x.Index);
            return View(model);
        }

        [HttpGet]
        public ActionResult Reorder()
        {
            var model = _db.GetCategories(x => x.Index);
            return View(model);
        }

        [HttpPost]
        public ActionResult Reorder(IList<Category> categories)
        {
            _db.Reorder(categories);
            return RedirectToAction("Index");
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