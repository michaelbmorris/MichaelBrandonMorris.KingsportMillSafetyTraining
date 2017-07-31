using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using Microsoft.AspNet.Identity.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class UsersController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for UsersController
    [Authorize(Roles = "Owner, Administrator")]
    public class UsersController : Controller
    {
        /// <summary>
        ///     Gets the database.
        /// </summary>
        /// <value>The database.</value>
        /// TODO Edit XML Comment Template for Db
        private KingsportMillSafetyTrainingDbContext Db
        {
            get;
        } = new KingsportMillSafetyTrainingDbContext();

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for ChangePassword
        [Authorize(Roles = "Owner, Administrator, Supervisor")]
        [HttpGet]
        public ActionResult ChangePassword(string id)
        {
            try
            {
                var user = Db.GetUser(id);

                if (User.IsInRole("Supervisor"))
                {
                    var currentUser = Db.GetUser(User.GetId());
                    var company = currentUser.Company;

                    if (company.GetEmployees().All(employee => employee.Id != user.Id))
                    {
                        throw new UnauthorizedAccessException(
                            "You are not permitted to change the password for this user.");
                    }
                }

                var model = new ChangePasswordViewModel
                {
                    UserId = user.Id,
                    Email = user.Email
                };

                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for ChangePassword
        [HttpPost]
        public async Task<ActionResult> ChangePassword(
            ChangePasswordViewModel model)
        {
            try
            {
                var manager = HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();

                var user = await manager.FindByIdAsync(model.UserId);

                user.PasswordHash =
                    manager.PasswordHasher.HashPassword(model.Password);

                await manager.UpdateSecurityStampAsync(user.Id);
                await manager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Changes the role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for ChangeRole
        [Authorize(Roles = "Owner, Administrator")]
        [HttpGet]
        public ActionResult ChangeRole(string id)
        {
            try
            {
                var model = new ChangeRoleViewModel(Db.GetUser(id));
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Posts the change role view with the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        [Authorize(Roles = "Owner, Administrator")]
        [HttpPost]
        public async Task<ActionResult> ChangeRole(ChangeRoleViewModel model)
        {
            try
            {
                var roleName = Db.GetRole(model.RoleId).Name;

                if (User.IsInRole("Administrator"))
                {
                    if (roleName == "Owner"
                        || roleName == "Administrator")
                    {
                        throw new AccessViolationException(
                            "You are not permitted to promote users to Owner or Administrator.");
                    }
                }

                var userManager = HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();

                var roles = await userManager.GetRolesAsync(model.UserId);

                await userManager.RemoveFromRolesAsync(
                    model.UserId,
                    roles.ToArray());

                await userManager.AddToRoleAsync(
                    model.UserId,
                    Db.GetRole(model.RoleId).Name);

                return RedirectToAction("Index");
            }
            catch (AccessViolationException e)
            {
                return this.CreateError(HttpStatusCode.Forbidden, e);
            }
            catch (Exception e)
            {
                // TODO Replace general error handling with specific.
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Returns the details view for the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        [Authorize(Roles = "Owner, Administrator, Security, Supervisor")]
        [HttpGet]
        public ActionResult Details(string id)
        {
            try
            {
                if (User.IsInRole("Supervisor"))
                {
                    var currentUser = Db.GetUser(User.GetId());
                    var company = currentUser.Company;

                    if (company.GetEmployees().All(employee => employee.Id != id))
                    {
                        throw new UnauthorizedAccessException(
                            "You are not permitted to view the details for this user.");
                    }
                }

                var model = Db.GetUser(id).AsViewModel();
                return View(model);
            }
            catch (Exception e)
            {
                // TODO Replace general error handling with specific.
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Gets the edit view for the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        [Authorize(Roles = "Owner, Administrator")]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                var model = Db.GetUser(id).AsViewModel();
                return View(model);
            }
            catch (Exception e)
            {
                // TODO Replace general error handling with specific.
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Edits the specified user.
        /// </summary>
        /// <param name="model">The user.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Edit
        [Authorize(Roles = "Owner, Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Db.EditUser(
                    model.CompanyId,
                    model.Email,
                    model.FirstName,
                    model.Id,
                    model.LastName,
                    model.MiddleName,
                    model.OtherCompanyName,
                    model.PhoneNumber);

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
        [Authorize(Roles = "Owner,Administrator,Security,Supervisor")]
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var model = Db.GetUser(User.GetId()).AsViewModel();
                return View(model);
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
                Db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}