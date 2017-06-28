using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     The controller for training. Requires authorization.
    /// </summary>
    /// <seealso cref="Controller" />
    [Authorize]
    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        ///     Gets the index view. Checks if the current <see cref="User" />
        ///     has an assigned <see cref="Role" />, and redirects to selection
        ///     if not. Otherwise, displays the appropriate training for the
        ///     role.
        /// </summary>
        /// <returns>The index view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var role = GetCurrentUserRole();

            if (role == null)
            {
                return RedirectToAction("SelectRole");
            }

            _db.SetUserLatestTrainingStartDateTime(User.GetId());
            var model = role.GetSlides(OrderCategoriesByIndex, orderSlidesBy: OrderSlidesByIndex).AsViewModels();
            return View(model);
        }

        private static Func<Slide, object> OrderSlidesByIndex => slide => slide
            .Index;

        private static Func<Category, object> OrderCategoriesByIndex => category
            => category.Index;

        /// <summary>
        ///     Gets the quiz view.
        /// </summary>
        /// <returns>The quiz view.</returns>
        [HttpGet]
        public ActionResult Quiz()
        {
            var role = GetCurrentUserRole();
            var model = role.GetSlides(slidesWhere: slide => slide.ShouldShowSlideInSlideshow && slide.ShouldShowQuestionOnQuiz).AsQuizSlideViewModels().Shuffle();
            System.Web.HttpContext.Current.Session["QuizViewModel"] = model;
            _db.AddTrainingResult(User.GetId());
            _db.SetUserLatestQuizStartDateTime(User.GetId());
            return View(model);
        }

        /// <summary>
        ///     Posts the quiz view. Grades the quiz results, and returns to the
        ///     quiz view if there is an incorrect question. If all questions
        ///     are correct, redirects to the training result.
        /// </summary>
        /// <param name="model">
        ///     Each <see cref="QuizSlideViewModel" /> in an
        ///     <see cref="IList{T}" />.
        /// </param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public ActionResult Quiz(IList<QuizSlideViewModel> model)
        {
            var quizViewModel =
                (IList<QuizSlideViewModel>) System.Web.HttpContext.Current
                    .Session["QuizViewModel"];

            for (var i = 0; i < model.Count; i++)
            {
                quizViewModel[i].AnswerQuestion(model[i].SelectedAnswerIndex);
            }

            model = quizViewModel;
            var user = _db.GetUser(User.GetId());
            var trainingResult = user.TrainingResults.Last();

            _db.AddQuizResult(
                trainingResult.Id,
                model.Count(x => x.IsCorrect()),
                model.Count);

            if (!model.All(x => x.IsCorrect()))
            {
                _db.SetUserLatestQuizStartDateTime(User.GetId());
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

            _db.Edit(trainingResult);

            return RedirectToAction(
                "Details",
                "Results",
                new
                {
                    id = trainingResult.Id
                });
        }

        /// <summary>
        ///     Gets the select role view.
        /// </summary>
        /// <returns>The select role view.</returns>
        [HttpGet]
        public ActionResult SelectRole()
        {
            var model = _db.GetRoles(x => x.Index).AsViewModels();
            model = model.Take(model.Count - 1);
            return View(model);
        }

        /// <summary>
        ///     Posts the select role view.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SelectRole(int? roleId)
        {
            _db.SetUserRole(User.GetId(), roleId);
            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Releases unmanaged resources and optionally releases managed
        ///     resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to
        ///     release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }

        private Role GetCurrentUserRole()
        {
            var user = _db.GetUser(User.GetId());

            if (user == null)
            {
                throw new Exception();
            }

            return user.Role;
        }
    }
}