using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SlidesController : Controller
    {
        private const string JpgType = "image/jpg";
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

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
            var model = _db.GetNewSlideViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SlideViewModel slideViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(slideViewModel);
            }

            Debug.WriteLine(slideViewModel.Image == null);

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
        public ActionResult Edit(SlideViewModel slideViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(slideViewModel);
            }

            Debug.WriteLine(slideViewModel.Image == null);
            _db.Edit(slideViewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_db.GetSlideViewModels());
        }

        [AllowAnonymous]
        [HttpGet]
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
        public ActionResult Reorder(int? categoryId)
        {
            if (categoryId == null)
            {
                return RedirectToAction("SelectCategoryToReorder");
            }

            var model = _db.GetSlideViewModels(categoryId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Reorder(IList<Slide> slides)
        {
            _db.Reorder(slides);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SelectCategoryToReorder()
        {
            var model = _db.GetCategoryViewModels();
            return View(model);
        }

        [HttpGet]
        public ActionResult ViewSlide(int id)
        {
            var model = _db.GetSlideViewModel(id);
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