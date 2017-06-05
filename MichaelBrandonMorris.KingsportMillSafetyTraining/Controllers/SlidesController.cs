using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SlidesController : Controller
    {
        private const string FieldsToInclude =
            "Answers"
            + "Content,"
            + "CorrectAnswerIndex,"
            + "ImageBytes,"
            + "ImageDescription,"
            + "Question,"
            + "ShouldShowImageOnQuiz,"
            + "ShouldShowQuestionOnQuiz,"
            + "ShouldShowSlideInSlideshow,"
            + "Title";

        private const string JpgType = "image/jpg";

        private readonly KingsportMillSafetyTrainingDbContext _db =
            new KingsportMillSafetyTrainingDbContext();

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AddAnswer()
        {
            return View(
                new SlideViewModel
                {
                    Answers = new List<Answer>
                    {
                        new Answer()
                    }
                });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new SlideViewModel(null, _db.GetCategories()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = FieldsToInclude)] SlideViewModel slideViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(slideViewModel);
            }

            _db.CreateSlide(slideViewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var slide = _db.GetSlide(id.Value);

            if (slide == null)
            {
                return HttpNotFound();
            }

            return View(slide);
        }

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var slide = _db.GetSlide(id);

            if (slide != null)
            {
                _db.DeleteSlide(slide);
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

            var slide = _db.GetSlide(id.Value);

            if (slide == null)
            {
                return HttpNotFound();
            }

            return View(slide);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var slideViewModel = _db.GetSlideViewModel(id.Value);

            if (slideViewModel == null)
            {
                return HttpNotFound();
            }

            return View(slideViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = FieldsToInclude)] SlideViewModel slideViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(slideViewModel);
            }

            _db.Edit(slideViewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_db.GetSlideViewModels());
        }

        public ActionResult RenderImage(int id)
        {
            var slide = _db.GetSlide(id);

            if (slide == null)
            {
                return HttpNotFound();
            }

            return slide.ImageBytes == null
                ? null
                : File(slide.ImageBytes, JpgType);
        }

        [HttpGet]
        public ActionResult Reorder(int categoryId)
        {
            return View(_db.GetSlides());
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