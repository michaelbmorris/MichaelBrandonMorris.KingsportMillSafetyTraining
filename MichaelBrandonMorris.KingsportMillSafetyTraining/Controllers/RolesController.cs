using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private const string FieldsToInclude = "Id,Description,Title";

        private readonly KingsportMillSafetyTrainingDbContext _db =
            new KingsportMillSafetyTrainingDbContext();

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = FieldsToInclude)] Role role)
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

        [HttpPost]
        [ActionName("Delete")]
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

            var role = _db.GetRole(id.Value);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
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
        public ActionResult Edit([Bind(Include = FieldsToInclude)] Role role)
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
            return View(_db.GetRoles());
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