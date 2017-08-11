﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.Math;
using Microsoft.Owin;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
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

        private CategoryManager CategoryManager =>
            OwinContext.Get<CategoryManager>();

        private GroupManager GroupManager =>
            OwinContext.Get<GroupManager>();

        private IOwinContext OwinContext => HttpContext.GetOwinContext();

        /// <summary>
        ///     Assigns the roles.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignGroups
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public async Task<ActionResult> AssignGroups(int? id)
        {
            try
            {
                AssignGroupsViewModel model;
                var groups = (await GroupManager.Groups.ToListAsync()).AsViewModels();

                if (id == null)
                {
                    var categories = await CategoryManager.Categories.ToListAsync();
                    model = new AssignGroupsViewModel(categories, groups);
                }
                else
                {
                    var category = await CategoryManager.FindByIdAsync(id.Value);
                    model = new AssignGroupsViewModel(category, groups);
                }

                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Assigns the roles.
        /// </summary>
        /// <param name="categoryGroups">The category roles.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AssignGroups
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        public ActionResult AssignGroups(IList<int> categoryGroups)
        {
            try
            {
                CategoryManager.RemoveGroups();

                if (categoryGroups == null)
                {
                    return RedirectToAction("Index");
                }

                foreach (var categoryGroup in categoryGroups)
                {
                    Db.PairCategoryAndGroup(Cantor.Inverse(categoryGroup));
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Creates the specified category.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Create
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Db.CreateCategory(
                    new Category
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Index = ++Category.CurrentIndex
                    });

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
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
        [Authorize(Roles = "Owner, Administrator")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {

                var model = Db.GetCategory(id).AsViewModel();
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
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
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
        [Authorize(Roles = "Owner, Administrator")]
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new InvalidOperationException(
                        "Parameter missing.\nType: 'int'\nName: 'id'");
                }

                var category = Db.GetCategory(id.Value);

                if (category == null)
                {
                    throw new KeyNotFoundException(
                        $"Category with Id {id} not found.");
                }

                Db.DeleteCategory(category);
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
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
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
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new InvalidOperationException(
                        "Parameter missing.\nType: 'int'\nName: 'id'");
                }

                var model = Db.GetCategory(id.Value).AsViewModel();
                return View(model);
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
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
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
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new InvalidOperationException(
                        "Parameter missing.\nType: 'int'\nName: 'id'");
                }

                var model = Db.GetCategory(id.Value).AsViewModel();
                return View(model);
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
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Edits the specified category.
        /// </summary>
        /// <param name="model">The category.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Db.Edit(new Category
                {
                    Title = model.Title,
                    Description = model.Description,
                    Index = model.Index,
                    Id = model.Id,
                    Groups = model.Groups,
                    Slides = model.Slides
                });

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Index
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var model = Db.GetCategories(x => x.Index).AsViewModels();
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Reorders this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpGet]
        public ActionResult Reorder()
        {
            try
            {
                var model = Db.GetCategories(OrderByIndex).AsViewModels();
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Reorders the specified categories.
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Reorder
        [Authorize(Roles = "Owner, Administrator, Collaborator")]
        [HttpPost]
        public ActionResult Reorder(IList<Category> categories)
        {
            try
            {
                Db.Reorder(categories);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
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
                Db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}