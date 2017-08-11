using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class UsersController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for UsersController
    public class UsersController : Controller
    {
        private readonly CompanyManager _companyManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager _userManager;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="UsersController" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public UsersController()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="UsersController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager"></param>
        /// <param name="companyManager"></param>
        /// TODO Edit XML Comment Template for #ctor
        public UsersController(
            UserManager userManager,
            RoleManager<Role> roleManager,
            CompanyManager companyManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _companyManager = companyManager;
        }

        private CompanyManager CompanyManager => _companyManager
                                                 ?? OwinContext
                                                     .Get<CompanyManager>();

        private IOwinContext OwinContext => HttpContext.GetOwinContext();

        private RoleManager<Role> RoleManager => _roleManager
                                                 ?? OwinContext
                                                     .Get<RoleManager<Role>>();

        private UserManager UserManager => _userManager
                                           ?? OwinContext.Get<UserManager>();


        /// <summary>
        ///     Changes the password.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for ChangePassword
        [Authorize(Roles = "Owner, Administrator, Supervisor")]
        [HttpGet]
        public async Task<ActionResult> ChangePassword(string id)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(id);

                if (User.IsInRole("Supervisor"))
                {
                    var currentUser =
                        await UserManager.FindByIdAsync(User.GetId());

                    var company = currentUser.Company;

                    if (company.GetEmployees()
                        .All(employee => employee.Id != user.Id))
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
        ///     Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for ChangePassword
        [Authorize(Roles = "Owner, Administrator, Supervisor")]
        [HttpPost]
        public async Task<ActionResult> ChangePassword(
            ChangePasswordViewModel model)
        {
            try
            {
                var manager = HttpContext.GetOwinContext()
                    .GetUserManager<UserManager>();

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
        ///     Changes the role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for ChangeRole
        [Authorize(Roles = "Owner, Administrator")]
        [HttpGet]
        public async Task<ActionResult> ChangeRole(string id)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(id);
                var model = new ChangeRoleViewModel(user);
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
                var roleName = (await RoleManager.FindByIdAsync(model.RoleId))
                    .Name;

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
                    .GetUserManager<UserManager>();

                var roles = await userManager.GetRolesAsync(model.UserId);

                await userManager.RemoveFromRolesAsync(
                    model.UserId,
                    roles.ToArray());

                await userManager.AddToRoleAsync(model.UserId, roleName);

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
        public async Task<ActionResult> Details(string id)
        {
            try
            {
                if (User.IsInRole("Supervisor"))
                {
                    var currentUser =
                        await UserManager.FindByIdAsync(User.GetId());
                    var company = currentUser.Company;

                    if (company.GetEmployees()
                        .All(employee => employee.Id != id))
                    {
                        throw new UnauthorizedAccessException(
                            "You are not permitted to view the details for this user.");
                    }
                }

                var user = await UserManager.FindByIdAsync(id);
                var model = user.AsViewModel();
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
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(id);
                var model = user.AsViewModel();
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
        public async Task<ActionResult> Edit(UserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await UserManager.FindByIdAsync(model.Id);

                user.Company =
                    await CompanyManager.FindByIdAsync(model.CompanyId);

                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MiddleName = model.MiddleName;
                user.OtherCompanyName = model.OtherCompanyName;
                user.PhoneNumber = model.PhoneNumber;
                await UserManager.UpdateAsync(user);
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
        [Authorize(Roles = "Owner, Administrator, Security, Supervisor")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var users = await UserManager.Users.ToListAsync();
                var model = users.Select(user => new UserViewModel(user)).ToList();
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
                CompanyManager?.Dispose();
                RoleManager?.Dispose();
                UserManager?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}