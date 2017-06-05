using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
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

        // GET: Slides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = FieldsToInclude)] Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View(slide);
            }

            _db.Slides.Add(slide);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Slides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var slide = _db.Slides.Find(id);

            if (slide == null)
            {
                return HttpNotFound();
            }

            return View(slide);
        }

        // POST: Slides/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var slide = _db.Slides.Find(id);

            if (slide != null)
            {
                _db.Slides.Remove(slide);
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Slides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var slide = _db.Slides.Find(id);

            if (slide == null)
            {
                return HttpNotFound();
            }

            return View(slide);
        }

        // GET: Slides/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var slide = _db.Slides.Find(id);

            if (slide == null)
            {
                return HttpNotFound();
            }

            return View(slide);
        }

        // POST: Slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = FieldsToInclude)] Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View(slide);
            }

            _db.Entry(slide).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Slides
        public ActionResult Index()
        {
            return View(_db.Slides.ToList());
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