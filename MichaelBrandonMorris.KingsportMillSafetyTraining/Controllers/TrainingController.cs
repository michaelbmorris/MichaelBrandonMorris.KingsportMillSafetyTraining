using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class TrainingController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    /// TODO Edit XML Comment Template for TrainingController
    [Authorize]
    public class TrainingController : Controller
    {
        /// <summary>
        ///     Gets the index of the order categories by.
        /// </summary>
        /// <value>The index of the order categories by.</value>
        /// TODO Edit XML Comment Template for OrderCategoriesByIndex
        private static Func<Category, object> OrderCategoriesByIndex => category
            => category.Index;

        /// <summary>
        ///     Gets the index of the order slides by.
        /// </summary>
        /// <value>The index of the order slides by.</value>
        /// TODO Edit XML Comment Template for OrderSlidesByIndex
        private static Func<Slide, object> OrderSlidesByIndex => slide => slide
            .Index;

        /// <summary>
        ///     The database
        /// </summary>
        /// TODO Edit XML Comment Template for Db
        private KingsportMillSafetyTrainingDbContext Db
        {
            get;
        } = new KingsportMillSafetyTrainingDbContext();

        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Index
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var role = GetCurrentUserRole();

                if (role == null)
                {
                    return RedirectToAction("SelectRole");
                }

                Db.SetUserLatestTrainingStartDateTime(User.GetId());
                var model = role.GetSlides(
                        OrderCategoriesByIndex,
                        orderSlidesBy: OrderSlidesByIndex)
                    .AsViewModels();
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        ///     Quizs this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Quiz
        [HttpGet]
        public ActionResult Quiz()
        {
            var role = GetCurrentUserRole();
            var model = role
                .GetSlides(
                    slidesWhere: slide => slide.ShouldShowSlideInSlideshow
                                          && slide.ShouldShowQuestionOnQuiz)
                .AsQuizSlideViewModels()
                .Shuffle();
            System.Web.HttpContext.Current.Session["QuizViewModel"] = model;
            Db.AddTrainingResult(User.GetId());
            Db.SetUserLatestQuizStartDateTime(User.GetId());
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
            var user = Db.GetUser(User.GetId());
            var trainingResult = user.TrainingResults.Last();

            Db.AddQuizResult(
                trainingResult.Id,
                model.Count(x => x.IsCorrect()),
                model.Count);

            if (!model.All(x => x.IsCorrect()))
            {
                Db.SetUserLatestQuizStartDateTime(User.GetId());
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

            Db.Edit(trainingResult);

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
        /// TODO Edit XML Comment Template for SelectRole
        [HttpGet]
        public ActionResult SelectRole()
        {
            var model = Db.GetRoles(x => x.Index).AsViewModels();
            model = model.Take(model.Count - 1);
            return View(model);
        }

        /// <summary>
        ///     Selects the role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for SelectRole
        [HttpPost]
        public ActionResult SelectRole(int? roleId)
        {
            Db.SetUserRole(User.GetId(), roleId);
            return RedirectToAction("Index");
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

        /// <summary>
        ///     Gets the current user role.
        /// </summary>
        /// <returns>Role.</returns>
        /// <exception cref="Exception"></exception>
        /// TODO Edit XML Comment Template for GetCurrentUserRole
        private Role GetCurrentUserRole()
        {
            var user = Db.GetUser(User.GetId());

            if (user == null)
            {
                throw new Exception();
            }

            return user.Role;
        }
    }
}