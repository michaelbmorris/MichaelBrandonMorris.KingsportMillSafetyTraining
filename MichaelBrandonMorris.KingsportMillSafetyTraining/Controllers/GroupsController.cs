namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Db.Models;

    using Math;

    using MichaelBrandonMorris.Extensions.CollectionExtensions;

    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    using Models;

    /// <summary>
    ///     Class GroupsController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for GroupsController
    public class GroupsController : Controller
    {
        private const string EditInclude = "Description,Id,Question,Title";

        /// <summary>
        ///     Gets the index of the order by.
        /// </summary>
        /// <value>The index of the order by.</value>
        /// TODO Edit XML Comment Template for OrderByIndex
        private static Func<Group, object> OrderByIndex => role => role.Index;

        private CategoryManager CategoryManager => OwinContext
            .Get<CategoryManager>();

        private GroupManager GroupManager => OwinContext.Get<GroupManager>();

        private IOwinContext OwinContext => HttpContext.GetOwinContext();

        /// <summary>
        ///     Assigns the categories.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignCategories
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> AssignCategories(int? id)
        {
            var categories = await CategoryManager.Categories
                .OrderBy(c => c.Index)
                .ToListAsync();

            AssignCategoriesViewModel model;

            if (id == null)
            {
                var groups = await GroupManager.Groups.OrderBy(g => g.Index)
                    .ToListAsync();

                model = new AssignCategoriesViewModel(
                    groups,
                    categories);
            }
            else
            {
                var group = await GroupManager.FindByIdAsync(id.Value);
                model = new AssignCategoriesViewModel(
                    group,
                    categories);
            }

            return View(model);
        }

        /// <summary>
        ///     Assigns the categories.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for AssignCategories
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        public async Task<ActionResult> AssignCategories(IList<int> model)
        {
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            var unpairedGroups = new HashSet<int>();

            foreach (var item in model)
            {
                // ReSharper disable once UnusedVariable
                (int groupId, int categoryId) = Cantor.Inverse(item);

                if (unpairedGroups.Contains(groupId))
                {
                    continue;
                }

                await GroupManager.RemoveCategories(groupId);
                unpairedGroups.Add(groupId);
            }

            foreach (var item in model)
            {
                (int groupId, int categoryId) = Cantor.Inverse(item);
                var group = await GroupManager.FindByIdAsync(groupId);
                var category = await CategoryManager.FindByIdAsync(categoryId);
                await GroupManager.Pair(group, category);
            }

            return RedirectToAction("Index");
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
            return View();
        }

        /// <summary>
        ///     Creates the specified role.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GroupViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await GroupManager.CreateAsync(
                    new Group
                    {
                        Description = model.Description,
                        Index = ++Group.CurrentIndex,
                        Question = model.Question,
                        Title = model.Title
                    });

                if (result == 0)
                {
                    throw new DbUpdateException("Failed to save changes.");
                }

                return RedirectToAction("Index");
            }
            catch (DbUpdateException e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
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

                var group = await GroupManager.FindByIdAsync(id.Value);
                var model = group.AsViewModel();
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
                    throw new ArgumentNullException(nameof(id));
                }

                var group = await GroupManager.FindByIdAsync(id.Value);
                await GroupManager.DeleteAsync(group);
                return RedirectToAction("Index");
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
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
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
                    throw new ArgumentNullException(nameof(id));
                }

                var group = await GroupManager.FindByIdAsync(id.Value);
                var model = group.AsViewModel();

                if (model == null)
                {
                    throw new KeyNotFoundException();
                }

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
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
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
                    throw new ArgumentNullException(nameof(id));
                }

                var group = await GroupManager.FindByIdAsync(id.Value);
                var model = group.AsViewModel();
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
        ///     Edits the specified role.
        /// </summary>
        /// <param name="model">The role.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = EditInclude)] GroupViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var group = await GroupManager.FindByIdAsync(model.Id);
                group.Description = model.Description;
                group.Question = model.Question;
                group.Title = model.Title;
                await GroupManager.UpdateAsync(group);
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
            var groups = await GroupManager.Groups.ToListAsync();
            var model = groups.AsViewModels();
            return View(model);
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
            var groups = await GroupManager.Groups.ToListAsync();
            var model = groups.OrderBy(OrderByIndex).AsViewModels();
            return View(model);
        }

        /// <summary>
        ///     Reorders the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [KingsportMillSafetyTraining.Authorize(
            Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        public async Task<ActionResult> Reorder(IList<GroupViewModel> model)
        {
            foreach (var item in model)
            {
                var group = await GroupManager.FindByIdAsync(item.Id);
                group.Index = item.Index;
                await GroupManager.UpdateAsync(group);
            }

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
                CategoryManager?.Dispose();
                GroupManager?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}