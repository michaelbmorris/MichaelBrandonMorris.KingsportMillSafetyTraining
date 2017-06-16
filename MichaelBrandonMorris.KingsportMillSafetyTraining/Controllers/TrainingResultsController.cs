using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize]
    public class TrainingResultsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _db.GetTrainingResultViewModel(id.Value);
            return View(model);
        }

        [HttpGet]
        public ActionResult Index(string userId = null)
        {
            if (User.IsInRole("Administrator"))
            {
                return AdministratorIndex(userId);
            }

            var model = _db.GetTrainingResultViewModels(userId);
            return View(model);
        }

        public ActionResult AdministratorIndex(string userId = null)
        {
            return View();
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