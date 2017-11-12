using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    using System.Diagnostics;

    /// <summary>
    ///     Class TrainingController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for TrainingController
    [Authorize]
    public class OrientationController : Controller
    {
        private GroupManager GroupManager => OwinContext.Get<GroupManager>();
        private IOwinContext OwinContext => HttpContext.GetOwinContext();

        private TrainingResultManager TrainingResultManager => OwinContext
            .Get<TrainingResultManager>();

        private UserManager UserManager => OwinContext.Get<UserManager>();
        private SlideManager SlideManager => OwinContext.Get<SlideManager>();

        /// <summary>
        ///     Confirms the role.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Cannot confirm
        ///     user role because no role is selected.
        /// </exception>
        /// TODO Edit XML Comment Template for ConfirmGroup
        public async Task<ActionResult> ConfirmGroup()
        {
            try
            {
                var group = await GetCurrentUserGroup();

                if (group == null)
                {
                    return RedirectToAction("SelectGroup");
                }

                return View(group);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Index
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var group = await GetCurrentUserGroup();
                Debug.WriteLine(group == null);

                return RedirectToAction(
                    group == null ? "SelectGroup" : "ConfirmGroup");
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Quizs this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Quiz
        [HttpGet]
        public async Task<ActionResult> Quiz()
        {
            var groupId = (await GetCurrentUserGroup()).Id;

            var group = await GroupManager.Groups
                .Include(g => g.Categories.Select(c => c.Slides))
                .SingleOrDefaultAsync(g => g.Id == groupId);

            var categories = group.Categories;

            var slides = categories.SelectMany(
                c => c.Slides).Where(
                s => s.ShouldShowSlideInSlideshow
                     && s.ShouldShowQuestionOnQuiz
                     && !s.Question.IsNullOrWhiteSpace()
                     && s.Answers.Count >= 2);

            var model = slides.AsQuizSlideViewModels().Shuffle();
            System.Web.HttpContext.Current.Session["QuizViewModel"] = model;
            await UserManager.AddTrainingResult(User.GetId());
            await UserManager.SetLatestQuizStartDateTime(User.GetId());
            return View(model);
        }

        /// <summary>
        ///     Quizs the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="Exception"></exception>
        /// TODO Edit XML Comment Template for Quiz
        [HttpPost]
        public async Task<ActionResult> Quiz(IList<QuizSlideViewModel> model)
        {
            var quizViewModel =
                (IList<QuizSlideViewModel>) System.Web.HttpContext.Current
                    .Session["QuizViewModel"];

            for (var i = 0; i < model.Count; i++)
            {
                quizViewModel[i].AnswerQuestion(model[i].SelectedAnswerIndex);
            }

            model = quizViewModel;
            var userId = User.GetId();

            var user = await UserManager.Users.Include(u => u.TrainingResults)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var trainingResult =
                await TrainingResultManager.FindByIdAsync(
                    user.TrainingResults.Last().Id);

            await TrainingResultManager.AddQuizResult(
                trainingResult.Id,
                model.Count(x => x.IsCorrect()),
                model.Count);

            if (!model.All(x => x.IsCorrect()))
            {
                await UserManager.SetLatestQuizStartDateTime(User.GetId());
                return View(model);
            }

            trainingResult.CompletionDateTime = DateTime.Now;

            if (user.LatestTrainingStartDateTime == null)
            {
                throw new Exception();
            }

            trainingResult.TimeToComplete =
                trainingResult.CompletionDateTime.Value
                - user.LatestTrainingStartDateTime.Value;

            await TrainingResultManager.UpdateAsync(trainingResult);

            return RedirectToAction(
                "Details",
                "Results",
                new
                {
                    id = trainingResult.Id
                });
        }

        /// <summary>
        ///     Selects the role.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for SelectGroup
        [HttpGet]
        public async Task<ActionResult> SelectGroup()
        {
            var groups = await GroupManager.Groups.OrderBy(g => g.Index)
                .ToListAsync();

            if (groups.Count == 0)
            {
                return View("Error");
            }

            var model = new SelectGroupViewModel
            {
                Groups = groups.SkipLast(1).AsViewModels(),
                DefaultGroupId= groups.Max(group => group.Id)
            };

            return View(model);
        }

        /// <summary>
        ///     Selects the group.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// <exception cref="ArgumentNullException">id</exception>
        /// TODO Edit XML Comment Template for SelectGroup
        [HttpPost]
        public async Task<ActionResult> SelectGroup(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var group = await GroupManager.FindByIdAsync(id.Value);
                Debug.WriteLine(group == null);
                await UserManager.SetGroup(User.GetId(), group);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Trains this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Train
        public async Task<ActionResult> Orientation()
        {
            try
            {
                var groupId = (await GetCurrentUserGroup()).Id;
                await UserManager.SetLatestTrainingStartDateTime(User.GetId());

                var group = await GroupManager.Groups
                    .Include(g => g.Categories.Select(c => c.Slides))
                    .SingleOrDefaultAsync(g => g.Id == groupId);

                var categories = group.Categories.OrderBy(c => c.Index);

                var slides =
                    categories.SelectMany(
                        c => c.Slides
                        .OrderBy(s => s.Index)
                        .Where(s => s.ShouldShowSlideInSlideshow));

                var model =
                    slides.Select(
                        s => new SlideViewModel(s, new List<Category>()));
                return View(model);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("SelectGroup");
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
                UserManager?.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        ///     Gets the current user role.
        /// </summary>
        /// <returns>Group.</returns>
        /// <exception cref="Exception"></exception>
        /// TODO Edit XML Comment Template for GetCurrentUserGroup
        private Task<Group> GetCurrentUserGroup()
        {
            return UserManager.GetGroup(User.GetId());
        }
    }
}