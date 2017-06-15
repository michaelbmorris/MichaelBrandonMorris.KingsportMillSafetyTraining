using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult AssignCategories(int? id)
        {
            var model = _db.GetAssignCategoriesViewModel(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult AssignCategories(int[] roleCategories)
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

            var model = _db.GetRoleViewModel(id.Value);

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

        [HttpGet]
        public ActionResult Index()
        {
            var model = _db.GetRoleViewModels(x => x.Index);
            return View(model);
        }

        [HttpGet]
        public ActionResult Reorder()
        {
            var model = _db.GetRoles(x => x.Index);
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