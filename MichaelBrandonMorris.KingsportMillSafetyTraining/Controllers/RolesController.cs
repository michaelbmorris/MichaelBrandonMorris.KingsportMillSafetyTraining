using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private readonly KingsportMillSafetyTrainingDbContext _db = new KingsportMillSafetyTrainingDbContext();

        [HttpGet]
        public ActionResult AssignCategories(int? id)
        {
            AssignCategoriesViewModel model;
            if (id == null)
            {
                model = new AssignCategoriesViewModel(_db.GetRoles(), _db.GetCategories().AsViewModels());
            }
            else
            {
                model = new AssignCategoriesViewModel(_db.GetRole(id.Value), _db.GetCategories().AsViewModels());
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AssignCategories(IList<int> roleCategories)
        {
            _db.UnpairCategoriesAndRoles();

            if (roleCategories == null)
            {
                return RedirectToAction("Index");
            }

            foreach (var roleCategory in roleCategories)
            {
                var cantorInversePair = Helpers.CantorInverse(roleCategory);
                var roleId = cantorInversePair.Item1;
                var categoryId = cantorInversePair.Item2;
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
        public ActionResult Create(Role role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }

            _db.CreateRole(role);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = _db.GetRole(id.Value);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var role = _db.GetRole(id);

            if (role != null)
            {
                _db.DeleteRole(role);
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _db.GetRole(id.Value).AsViewModel();

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

            var role = _db.GetRole(id.Value);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }

            _db.Entry(role).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private static Func<Role, object> OrderByIndex => role => role.Index;

        [HttpGet]
        public ActionResult Index()
        {
            var model = _db.GetRoles(OrderByIndex).AsViewModels();
            return View(model);
        }

        [HttpGet]
        public ActionResult Reorder()
        {
            var model = _db.GetRoles(OrderByIndex).AsViewModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Reorder(IList<Role> roles)
        {
            _db.Reorder(roles);
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