﻿using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SlidesController : Controller
    {
        private const string FieldsToInclude =
            "Id,"
            + "Content,"
            + "CorrectAnswerIndex,"
            + "ImageBytes,"
            + "ImageDescription,"
            + "Index,"
            + "Question,"
            + "ShouldShowImageOnQuiz,"
            + "ShouldShowQuestionOnQuiz,"
            + "ShouldShowSlideInSlideshow,"
            + "Title";

        private readonly KingsportMillSafetyTrainingDbContext _db =
            new KingsportMillSafetyTrainingDbContext();

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = FieldsToInclude)] Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View(slide);
            }

            _db.CreateSlide(slide);
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

            var slide = _db.GetSlide(id.Value);

            if (slide == null)
            {
                return HttpNotFound();
            }

            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = FieldsToInclude)] Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View(slide);
            }

            _db.Edit(slide);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_db.GetSlides());
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