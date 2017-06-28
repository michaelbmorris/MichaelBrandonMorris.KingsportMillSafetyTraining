using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.OtherExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels;
using System;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     The slides controller. Requires authorization and authentication as an administrator.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize(Roles = "Administrator")]
    public class SlidesController : Controller
    {
        private const string JpgType = "image/jpg";
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>Gets the add answer view.</summary>
        /// <returns>The add answer view.</returns>
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

        private static Func<Category, object> OrderCategoryByIndex => category
            => category.Index;

        private static Func<Category, object> OrderCategoryByTitle => category
            => category.Title;

        /// <summary>Gets the create view.</summary>
        /// <returns>The create view.</returns>
        [HttpGet]
        public ActionResult Create()
        {
            var model = new SlideViewModel(
                null,
                _db.GetCategories(OrderCategoryByTitle));

            return View(model);
        }

        /// <summary>Posts the create view.</summary>
        /// <param name="slideViewModel">The slide view model.</param>
        /// <returns>Redirects to <see cref="Index"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SlideViewModel slideViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(slideViewModel);
            }

            var slide = new Slide
            {
                Answers = slideViewModel.Answers,
                Content = slideViewModel.Content,
                CorrectAnswerIndex = slideViewModel.CorrectAnswerIndex,
                ImageBytes = slideViewModel.Image?.ToBytes(),
                ImageDescription = slideViewModel.ImageDescription,
                Question = slideViewModel.Question,
                ShouldShowImageOnQuiz = slideViewModel.ShouldShowImageOnQuiz,
                ShouldShowQuestionOnQuiz = slideViewModel.ShouldShowQuestionOnQuiz,
                ShouldShowSlideInSlideshow = slideViewModel.ShouldShowSlideInSlideshow,
                Title = slideViewModel.Title
            };

            _db.CreateSlide(slide, slideViewModel.CategoryId);
            return RedirectToAction("Index");
        }

        /// <summary>Gets the delete view for the specified <see cref="Slide"/>.</summary>
        /// <param name="id">The <see cref="Slide"/> identifier.</param>
        /// <returns>The delete view for the specified <see cref="Slide"/>.</returns>
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.CreateError(
                    HttpStatusCode.BadRequest,
                    "Parameter missing.\nType: 'int'\nName: 'id'");
            }

            var slide = _db.GetSlide(id.Value).AsViewModel();

            if (slide == null)
            {
                return this.CreateError(
                    HttpStatusCode.NotFound,
                    $"Slide with id '{id.Value}' not found.");
            }

            return View(slide);
        }

        /// <summary>Posts the delete view for the specified <see cref="Slide"/>.</summary>
        /// <param name="id">The <see cref="Slide"/> identifier.</param>
        /// <returns>Redirects to <see cref="Index"/>.</returns>
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

        /// <summary>Gets the details view for the specified <see cref="Slide"/>.</summary>
        /// <param name="id">The <see cref="Slide"/> identifier.</param>
        /// <returns>The details view for the specified <see cref="Slide"/>.</returns>
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.CreateError(
                    HttpStatusCode.BadRequest,
                    "Parameter missing.\nType: 'int'\nName: 'id'");
            }

            var slide = _db.GetSlide(id.Value).AsViewModel();

            if (slide == null)
            {
                return this.CreateError(
                    HttpStatusCode.NotFound,
                    $"Slide with id '{id.Value}' not found.");
            }

            return View(slide);
        }

        /// <summary>Gets the edit view for the specified <see cref="Slide"/>.</summary>
        /// <param name="id">The <see cref="Slide"/> identifier.</param>
        /// <returns>The edit view for the specified <see cref="Slide"/>.</returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.CreateError(
                    HttpStatusCode.BadRequest,
                    "Parameter missing.\nType: 'int'\nName: 'id'");
            }

            var model = _db.GetSlide(id.Value).AsViewModel();

            if (model == null)
            {
                return this.CreateError(
                    HttpStatusCode.NotFound,
                    $"Slide with id '{id.Value}' not found.");
            }

            return View(model);
        }

        /// <summary>Posts the edit view for the specified <see cref="SlideViewModel"/>.</summary>
        /// <param name="model">The <see cref="SlideViewModel"/>.</param>
        /// <returns>Redirects to <see cref="Index"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SlideViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var slide = new Slide
            {
                Answers = model.Answers,
                Content = model.Content,
                CorrectAnswerIndex = model.CorrectAnswerIndex,
                ImageBytes = model.Image?.ToBytes(),
                ImageDescription = model.ImageDescription,
                Question = model.Question,
                ShouldShowImageOnQuiz = model.ShouldShowImageOnQuiz,
                ShouldShowQuestionOnQuiz = model.ShouldShowQuestionOnQuiz,
                ShouldShowSlideInSlideshow = model.ShouldShowSlideInSlideshow,
                Title = model.Title
            };

            _db.Edit(slide, model.CategoryId);
            return RedirectToAction("Index");
        }

        /// <summary>Gets the index view.</summary>
        /// <returns>The index view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var model = _db.GetSlides().AsViewModels();
            return View(model);
        }

        /// <summary>Gets the image for the specified <see cref="Slide"/>.</summary>
        /// <param name="id">The <see cref="Slide"/> identifier.</param>
        /// <returns>The image for the specified <see cref="Slide"/>.</returns>
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

        /// <summary>Gets the reorder view for the specified <see cref="Category"/>.</summary>
        /// <param name="categoryId">The <see cref="Category"/> identifier.</param>
        /// <returns>The reorder view for the specified <see cref="Category"/>.</returns>
        [HttpGet]
        public ActionResult Reorder(int? categoryId)
        {
            if (categoryId == null)
            {
                return RedirectToAction("SelectCategoryToReorder");
            }

            var model = _db.GetSlides(categoryId.Value).AsViewModels();
            return View(model);
        }

        /// <summary>Posts the reorder view for the specified slides.</summary>
        /// <param name="slides">The slides.</param>
        /// <returns>Redirects to <see cref="Index"/>.</returns>
        [HttpPost]
        public ActionResult Reorder(IList<Slide> slides)
        {
            _db.Reorder(slides);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SelectCategoryToReorder()
        {
            var model = _db.GetCategories().AsViewModels();
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ViewSlide(int id)
        {
            var model = _db.GetSlide(id).AsViewModel();
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