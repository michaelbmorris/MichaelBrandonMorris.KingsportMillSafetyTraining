using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using Microsoft.AspNet.Identity;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize]
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.CreateError(
                    HttpStatusCode.BadRequest,
                    "Parameter missing.\nType: 'int'\nName: 'id'");
            }

            if (!User.IsInRole("Administrator")
                && _db.IsUserTrainingResult(
                    User.Identity.GetUserId(),
                    id.Value))
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.Forbidden,
                    "You do not have permission to view this.");
            }

            var model = _db.GetTrainingResultViewModel(id.Value);
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Index()
        {
            var model = _db.GetTrainingResultsViewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult UserResults(string id = null)
        {
            if (id == null)
            {
                return this.CreateError(
                    HttpStatusCode.BadRequest,
                    "Parameter missing.\nName: 'id'\nType: 'string'");
            }

            var model = _db.GetTrainingResultsViewModel(id);
            return View(model);
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