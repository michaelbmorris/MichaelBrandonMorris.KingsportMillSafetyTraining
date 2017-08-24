namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Db.Models;

    using Math;

    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    using Models;

    /// <summary>
    ///     Class CategoriesController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for CategoriesController
    public class CategoriesController : Controller
    {
        /// <summary>
        ///     Gets the index of the order by.
        /// </summary>
        /// <value>The index of the order by.</value>
        /// TODO Edit XML Comment Template for OrderByIndex
        private static Func<Category, object> OrderByIndex => category =>
            category.Index;

        private CategoryManager CategoryManager => OwinContext
            .Get<CategoryManager>();

        private GroupManager GroupManager => OwinContext.Get<GroupManager>();

        private IOwinContext OwinContext => HttpContext.GetOwinContext();

        /// <summary>
        ///     Assigns the roles.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignGroups
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> AssignGroups(int? id)
        {
            try
            {
                AssignGroupsViewModel model;

                var groups = (await GroupManager.Groups.OrderBy(g => g.Index)
                        .ToListAsync())
                    .AsViewModels();

                if (id == null)
                {
                    var categories =
                        await CategoryManager.Categories.OrderBy(c => c.Index)
                            .ToListAsync();

                    model = new AssignGroupsViewModel(categories, groups);
                }
                else
                {
                    var category =
                        await CategoryManager.FindByIdAsync(id.Value);

                    model = new AssignGroupsViewModel(category, groups);
                }

                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Assigns the groups.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for AssignGroups
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        public async Task<ActionResult> AssignGroups(IList<int> model)
        {
            try
            {
                if (model == null)
                {
                    return RedirectToAction("Index");
                }

                var unpairedCategories = new HashSet<int>();

                foreach (var item in model)
                {
                    // ReSharper disable once UnusedVariable
                    (int categoryId, int groupId) = Cantor.Inverse(item);

                    if (unpairedCategories.Contains(categoryId))
                    {
                        continue;
                    }

                    await CategoryManager.RemoveGroups(categoryId);
                    unpairedCategories.Add(categoryId);
                }

                foreach (var item in model)
                {
                    (int categoryId, int groupId) = Cantor.Inverse(item);

                    var category =
                        await CategoryManager.FindByIdAsync(categoryId);

                    var group = await GroupManager.FindByIdAsync(groupId);
                    await CategoryManager.Pair(category, group);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }

                category.Index = await CategoryManager.Categories.CountAsync();
                await CategoryManager.CreateAsync(category);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nType: 'int'\nName: 'id'
        /// </exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// TODO Edit XML Comment Template for Delete
        [KingsportMillSafetyTraining.Authorize(Roles = "Owner, Administrator")]
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var category = await CategoryManager.FindByIdAsync(id.Value);
                return View(category);
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
        ///     Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nType: 'int'\nName: 'id'
        /// </exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// TODO Edit XML Comment Template for DeleteConfirmed
        [KingsportMillSafetyTraining.Authorize(Roles = "Owner, Administrator")]
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new InvalidOperationException(
                        "Parameter missing.\nType: 'int'\nName: 'id'");
                }

                var category = await CategoryManager.Categories
                    .Include(c => c.Slides)
                    .SingleOrDefaultAsync(c => c.Id == id);

                if (category == null)
                {
                    throw new KeyNotFoundException(
                        $"Category with Id {id} not found.");
                }

                if (category.Slides.Count > 0)
                {
                    return View("Error");
                }

                await CategoryManager.DeleteAsync(category);
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException e)
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
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nType: 'int'\nName: 'id'
        /// </exception>
        /// TODO Edit XML Comment Template for Details
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new InvalidOperationException(
                        "Parameter missing.\nType: 'int'\nName: 'id'");
                }

                var category = await CategoryManager.FindByIdAsync(id.Value);
                return View(category);
            }
            catch (InvalidOperationException e)
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
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nType: 'int'\nName: 'id'
        /// </exception>
        /// TODO Edit XML Comment Template for Edit
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new InvalidOperationException(
                        "Parameter missing.\nType: 'int'\nName: 'id'");
                }

                var category = await CategoryManager.FindByIdAsync(id.Value);

                if (category == null)
                {
                    return HttpNotFound();
                }

                return View(category);
            }
            catch (InvalidOperationException e)
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

        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var category = await CategoryManager.FindByIdAsync(model.Id);
                category.Description = model.Description;
                category.DisplayName = model.DisplayName;
                category.Name = model.Name;
                await CategoryManager.UpdateAsync(category);
                return RedirectToAction("Index");
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
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var categories = await CategoryManager.Categories.ToListAsync();
                return View(categories);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Reorders this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> Reorder()
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

        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        public async Task<ActionResult> Reorder(IList<Category> model)
        {
            try
            {
                foreach (var item in model)
                {
                    var category = await CategoryManager.FindByIdAsync(item.Id);
                    category.Index = item.Index;
                    await CategoryManager.UpdateAsync(category);
                }

                return RedirectToAction("Index");
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
            }

            base.Dispose(disposing);
        }
    }
}