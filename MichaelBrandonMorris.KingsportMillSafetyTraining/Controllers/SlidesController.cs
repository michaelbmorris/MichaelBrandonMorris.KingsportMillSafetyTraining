using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.Web.HttpPostedFileBase;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class SlidesController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for SlidesController
    public class SlidesController : Controller
    {
        /// <summary>
        ///     The JPG type
        /// </summary>
        /// TODO Edit XML Comment Template for JpgType
        private const string JpgType = "image/jpg";

        /// <summary>
        ///     Gets the index of the order category by.
        /// </summary>
        /// <value>The index of the order category by.</value>
        /// TODO Edit XML Comment Template for OrderCategoryByIndex
        private static Func<Category, object> OrderCategoryByIndex => category
            => category.Index;

        /// <summary>
        ///     Gets the order category by title.
        /// </summary>
        /// <value>The order category by title.</value>
        /// TODO Edit XML Comment Template for OrderCategoryByTitle
        private static Func<Category, object> OrderCategoryByTitle => category
            => category.Title;

        /// <summary>
        ///     The database
        /// </summary>
        /// TODO Edit XML Comment Template for Db
        private KingsportMillSafetyTrainingDbContext Db
        {
            get;
        } = new KingsportMillSafetyTrainingDbContext();

        /// <summary>
        ///     Adds the answer.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AddAnswer
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
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

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new SlideViewModel(null);
            return View(model);
        }

        /// <summary>
        ///     Creates the specified slide view model.
        /// </summary>
        /// <param name="model">The slide view model.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SlideViewModel model)
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

            Db.CreateSlide(slide, model.CategoryId);
            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Delete
        [Authorize(Roles = "Owner, Administrator")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                var model = Db.GetSlide(id).AsViewModel();
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        [Authorize(Roles = "Owner, Administrator")]
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var slide = Db.GetSlide(id);

            if (slide != null)
            {
                Db.DeleteSlide(slide);
            }

            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Details
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            try
            {
                var slide = Db.GetSlide(id).AsViewModel();
                return View(slide);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                var model = Db.GetSlide(id).AsViewModel();
                return View(model);
            }
            catch (ArgumentNullException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e);
            }
            catch (KeyNotFoundException e)
            {
                return this.CreateError(HttpStatusCode.NotFound, e);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Edits the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SlideViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                    Db.EditSlide(
                        model.Answers,
                        model.CategoryId,
                    model.Content,
                    model.CorrectAnswerIndex,
                    model.Id,
                    model.Image == null
                            ? model.ImageBytes
                            : model.Image.ToBytes(),
                    model.ImageDescription,
                    model.Question,
                    model.ShouldShowImageOnQuiz,
                    model.ShouldShowQuestionOnQuiz,
                    model.ShouldShowSlideInSlideshow,
                    model.Title
                );

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Returns the index view.
        /// </summary>
        /// <param name="id">The group identifier.</param>
        /// <returns>The index view.</returns>
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Index(int? id)
        {
            var model = id == null ? Db.GetCategories(category => category.Index).AsViewModels() : Db.GetGroup(id.Value).GetCategories().AsViewModels();

            return View(model);
        }

        /// <summary>
        ///     Renders the image.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for RenderImage
        [AllowAnonymous]
        [HttpGet]
        public ActionResult RenderImage(int id)
        {
            var slide = Db.GetSlide(id);

            if (slide == null)
            {
                return HttpNotFound();
            }

            return slide.ImageBytes == null
                ? null
                : File(slide.ImageBytes, JpgType);
        }

        /// <summary>
        ///     Reorders the specified category identifier.
        /// </summary>
        /// <param name="id">The category identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Reorder(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("SelectCategoryToReorder");
            }

            var model = Db.GetSlides(id.Value).AsViewModels();
            return View(model);
        }

        /// <summary>
        ///     Reorders the specified slides.
        /// </summary>
        /// <param name="slides">The slides.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        public ActionResult Reorder(IList<Slide> slides)
        {
            Db.Reorder(slides);
            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Selects the category to reorder.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for SelectCategoryToReorder
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult SelectCategoryToReorder()
        {
            var model = Db.GetCategories().AsViewModels();
            return View(model);
        }

        /// <summary>
        ///     Views the slide.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for View
        [AllowAnonymous]
        [HttpGet]
        public ActionResult View(int id)
        {
            var model = Db.GetSlide(id).AsViewModel();
            return View(model);
        }

        /// <summary>
        ///     Releases unmanaged resources and optionally releases
        ///     managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources;
        ///     false to release only unmanaged resources.
        /// </param>
        /// TODO Edit XML Comment Template for Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}