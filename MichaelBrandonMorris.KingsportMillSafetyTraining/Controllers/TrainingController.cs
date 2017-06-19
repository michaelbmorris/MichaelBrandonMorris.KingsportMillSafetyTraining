using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels;
using Microsoft.AspNet.Identity;

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
                "Details",
                "Results",
                new
                {
                    id = trainingResult.Id
                });
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
            Debug.WriteLine(roleId);
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