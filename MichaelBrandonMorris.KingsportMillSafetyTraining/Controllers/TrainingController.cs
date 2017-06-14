using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels;
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

            var user = _db.GetUser(User.Identity.GetUserId());
            user.LatestTrainingStartDateTime = DateTime.Now;
            _db.Edit(user);
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
            trainingResult.QuizAttemptsCount++;

            if (!model.All(x => x.IsCorrect()))
            {
                return View(model);
            }

            trainingResult.CompletionDateTime = DateTime.Now;

            trainingResult.TimeToComplete =
                trainingResult.CompletionDateTime
                - user.LatestTrainingStartDateTime;

            _db.Edit(trainingResult);

            return RedirectToAction("Results");
        }

        [HttpGet]
        public ActionResult Results()
        {
            var model =
                _db.GetTrainingResultViewModel(User.Identity.GetUserId());

            return View(model);
        }

        [HttpGet]
        public ActionResult SelectRole()
        {
            var roles = _db.GetRoles(x => x.Index);
            roles = roles.Take(roles.Count - 1).ToList();
            return View(roles);
        }

        [HttpPost]
        public ActionResult SelectRole(int? roleId)
        {
            var user = _db.GetUser(User.Identity.GetUserId());

            user.Role = roleId == null
                ? _db.GetRoles().MaxBy(x => x.Index)
                : _db.GetRole(roleId.Value);

            _db.Edit(user);

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