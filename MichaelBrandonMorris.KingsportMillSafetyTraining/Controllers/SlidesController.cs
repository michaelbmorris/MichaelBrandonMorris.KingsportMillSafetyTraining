using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.Extensions.Web.HttpPostedFileBase;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class SlidesController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for SlidesController
    public class SlidesController : Controller
    {
        private AnswerManager AnswerManager => OwinContext.Get<AnswerManager>();

        private CategoryManager CategoryManager => OwinContext
            .Get<CategoryManager>();

        private GroupManager GroupManager => OwinContext.Get<GroupManager>();
        private IOwinContext OwinContext => HttpContext.GetOwinContext();
        private SlideManager SlideManager => OwinContext.Get<SlideManager>();

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
                new SlideViewModel(new List<Category>())
                {
                    Answers = new List<Answer>
                    {
                        new Answer()
                    },
                });
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var categories = await CategoryManager.Categories
                .OrderBy(c => c.Index)
                .ToListAsync();

            var model = new SlideViewModel(categories)
            {
                ShouldShowSlideInSlideshow = true
            };

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
        public async Task<ActionResult> Create(SlideViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await CategoryManager.Categories
                    .OrderBy(c => c.Index)
                    .ToListAsync();

                return View(model);
            }

            var category =
                await CategoryManager.FindByIdAsync(model.CategoryId);

            var slide = new Slide
            {
                Answers = model.Answers,
                Category = category,
                Content = model.Content,
                CorrectAnswerIndex = model.CorrectAnswerIndex,
                ImageBytes = model.Image?.ToBytes(),
                ImageDescription = model.ImageDescription,
                MimeType = model.Image == null ? null : MimeMapping.GetMimeMapping(model.Image?.FileName),
                Question = model.Question,
                ShouldShowImageOnQuiz = model.ShouldShowImageOnQuiz,
                ShouldShowQuestionOnQuiz = model.ShouldShowQuestionOnQuiz,
                ShouldShowSlideInSlideshow = model.ShouldShowSlideInSlideshow,
                Title = model.Title
            };

            await SlideManager.CreateAsync(slide);
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
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var slide = await SlideManager.FindByIdAsync(id.Value);
                var model = new SlideViewModel(slide, new List<Category>());
                return View(model);
            }
            catch (ArgumentNullException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e);
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
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var slide = await SlideManager.Slides.Include(s => s.Answers)
                    .SingleOrDefaultAsync(s => s.Id == id.Value);

                if (slide == null)
                {
                    throw new KeyNotFoundException();
                }

                foreach (var answer in slide.Answers.ToList())
                {
                    await AnswerManager.DeleteAsync(answer);
                }

                await SlideManager.DeleteAsync(slide);
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Details
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var slide = await SlideManager.FindByIdAsync(id.Value);

                if (slide == null)
                {
                    throw new KeyNotFoundException();
                }

                var model = new SlideViewModel(slide, new List<Category>());
                return View(model);
            }
            catch (ArgumentNullException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e);
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
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var slide = await SlideManager.FindByIdAsync(id.Value);

                if (slide == null)
                {
                    throw new KeyNotFoundException();
                }

                var categories = await CategoryManager.Categories
                    .OrderBy(c => c.Index)
                    .ToListAsync();

                var model = new SlideViewModel(slide, categories);
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
        public async Task<ActionResult> Edit(SlideViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Categories = await CategoryManager.Categories
                        .OrderBy(c => c.Index)
                        .ToListAsync();

                    return View(model);
                }

                var slide = await SlideManager.FindByIdAsync(model.Id);

                if (model.Answers == null)
                {
                    foreach (var answer in slide.Answers.ToList())
                    {
                        await AnswerManager.DeleteAsync(answer);
                    }
                }
                else
                {
                    foreach (var answer in model.Answers)
                    {
                        var originalAnswer =
                            await AnswerManager.FindByIdAsync(answer.Id);

                        if (originalAnswer == null)
                        {
                            answer.Id = 0;
                            slide.Answers.Add(answer);
                        }
                        else
                        {
                            originalAnswer.Title = answer.Title;
                            await AnswerManager.UpdateAsync(originalAnswer);
                        }
                    }

                    foreach (var answer in slide.Answers.ToList())
                    {
                        if (model.Answers.All(x => x.Id != answer.Id))
                        {
                            await AnswerManager.DeleteAsync(answer);
                        }
                    }
                }

                slide.Category =
                    await CategoryManager.FindByIdAsync(model.CategoryId);

                slide.CorrectAnswerIndex = model.CorrectAnswerIndex;

                if (model.Image != null)
                {
                    slide.ImageBytes = model.Image.ToBytes();

                    slide.MimeType =
                        MimeMapping.GetMimeMapping(model.Image.FileName);
                }

                slide.ImageDescription = model.ImageDescription;
                slide.Question = model.Question;
                slide.ShouldShowImageOnQuiz = model.ShouldShowImageOnQuiz;
                slide.ShouldShowQuestionOnQuiz = model.ShouldShowQuestionOnQuiz;

                slide.ShouldShowSlideInSlideshow =
                    model.ShouldShowSlideInSlideshow;

                slide.Title = model.Title;
                await SlideManager.UpdateAsync(slide);

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
        public async Task<ActionResult> Index(int? id)
        {
            try
            {
                IList<Category> model;

                if (id == null)
                {
                    var categories = await CategoryManager.Categories
                        .Include(c => c.Slides)
                        .OrderBy(c => c.Index)
                        .ToListAsync();

                    foreach (var category in categories)
                    {
                        category.Slides =
                            category.Slides.OrderBy(s => s.Index).ToList();
                    }

                    model = categories;
                }
                else
                {
                    var group = await GroupManager.Groups
                        .Include(g => g.Categories.Select(c => c.Slides))
                        .SingleOrDefaultAsync(g => g.Id == id);

                    model = group.Categories.OrderBy(c => c.Index);
                }

                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Removes the image from the specified slide.
        /// </summary>
        /// <param name="id">The slide identifier.</param>
        /// <returns></returns>
        public async Task RemoveImage(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var slide = await SlideManager.FindByIdAsync(id.Value);

                if (slide == null)
                {
                    throw new KeyNotFoundException();
                }

                slide.ImageBytes = null;
                await SlideManager.UpdateAsync(slide);
            }
            catch (ArgumentNullException e)
            {
                this.CreateError(HttpStatusCode.BadRequest, e);
            }
            catch (KeyNotFoundException e)
            {
                this.CreateError(HttpStatusCode.NotFound, e);
            }
            catch (Exception e)
            {
                this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Renders the image.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> RenderImage(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var slide = await SlideManager.FindByIdAsync(id.Value);

                if (slide == null)
                {
                    throw new KeyNotFoundException();
                }

                return slide.ImageBytes == null
                    ? null
                    : File(slide.ImageBytes, slide.MimeType);
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
        ///     Reorders the specified category identifier.
        /// </summary>
        /// <param name="id">The category identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> Reorder(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("SelectCategoryToReorder");
                }

                var category = await CategoryManager.Categories
                    .Include(c => c.Slides)
                    .SingleOrDefaultAsync(c => c.Id == id.Value);

                var slides = category.Slides.OrderBy(s => s.Index).ToList();
                var model =
                    slides.Select(
                        s => new SlideViewModel(s, new List<Category>()));
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Reorders the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        public async Task<ActionResult> Reorder(IList<SlideViewModel> model)
        {
            try
            {
                foreach (var item in model)
                {
                    var slide = await SlideManager.FindByIdAsync(item.Id);
                    slide.Index = item.Index;
                    await SlideManager.UpdateAsync(slide);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Views the slide.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Review
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> Review(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var slide = await SlideManager.FindByIdAsync(id.Value);

                if (slide == null)
                {
                    throw new KeyNotFoundException();
                }

                var model = new SlideViewModel(slide, new List<Category>());
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
        ///     Selects the category to reorder.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for SelectCategoryToReorder
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> SelectCategoryToReorder()
        {
            try
            {
                var categories = await CategoryManager.Categories
                    .OrderBy(c => c.Index)
                    .ToListAsync();
                return View(categories);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
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
                CategoryManager?.Dispose();
                GroupManager?.Dispose();
                SlideManager?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}