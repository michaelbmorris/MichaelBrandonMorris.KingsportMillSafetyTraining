using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity;
using Microsoft.AspNet.Identity;
using MoreLinq;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize]
    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            var role = GetCurrentUserRole();

            if (role == null)
            {
                return RedirectToAction("SelectRole");
            }

            _db.SetUserLatestTrainingStartDateTime(User.Identity.GetUserId());
            var model = _db.GetSlideshowViewModel(role);
            return View(model);
        }

        [HttpGet]
        public ActionResult Quiz()
        {
            var role = GetCurrentUserRole();
            var model = _db.GetQuizViewModel(role);
            System.Web.HttpContext.Current.Session["QuizViewModel"] = model;
            _db.AddTrainingResult(User.Identity.GetUserId());
            _db.SetUserLatestQuizStartDateTime(User.Identity.GetUserId());
            return View(model);
        }

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
            var user = _db.GetUser(User.Identity.GetUserId());
            var trainingResult = user.TrainingResults.Last();

            _db.AddQuizResult(
                trainingResult.Id,
                model.Count(x => x.IsCorrect()),
                model.Count);

            if (!model.All(x => x.IsCorrect()))
            {
                _db.SetUserLatestQuizStartDateTime(User.Identity.GetUserId());
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
                "Result",
                new
                {
                    id = trainingResult.Id
                });
        }

        /// <summary>
        ///     Shows the result of the training. Since this controller does not
        ///     require authorization, only training results belonging to the
        ///     current user will be shown. Administrators have access to the
        ///     Result action in the Users controller to view results for all users.
        /// </summary>
        /// <param name="id">The <see cref="TrainingResult" /> id.</param>
        /// <returns>
        ///     If the user is authorized, the view of the specified
        ///     <see cref="TrainingResult" />. Otherwise, a
        ///     <see cref="HttpStatusCode.Forbidden" />.
        /// </returns>
        [HttpGet]
        public ActionResult Result(int id)
        {
            if (!_db.IsUserTrainingResult(User.Identity.GetUserId(), id))
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.Forbidden,
                    "You do not have permission to view training results for other users.");
            }

            var model = _db.GetTrainingResultViewModel(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult SelectRole()
        {
            var model = _db.GetRoleViewModels(x => x.Index);
            model = model.Take(model.Count - 1);
            return View(model);
        }

        [HttpPost]
        public ActionResult SelectRole(int? roleId)
        {
            _db.SetUserRole(User.Identity.GetUserId(), roleId);
            return RedirectToAction("Index");
        }

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
            var user = _db.GetUser(User.Identity.GetUserId());

            if (user == null)
            {
                throw new Exception();
            }

            return user.Role;
        }
    }

    public static class Extensions
    {
        public static void SetQuizViewModel(
            this IController controller,
            IList<QuizSlideViewModel> quizViewModel)
        {
        }
    }
}