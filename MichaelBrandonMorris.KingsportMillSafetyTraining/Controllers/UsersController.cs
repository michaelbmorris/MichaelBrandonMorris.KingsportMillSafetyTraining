using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
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
        private CompanyManager CompanyManager =>
            OwinContext.Get<CompanyManager>();

        private GroupManager GroupManager => OwinContext.Get<GroupManager>();
        private IOwinContext OwinContext => HttpContext.GetOwinContext();

        private RoleManager<Role> RoleManager => OwinContext
            .Get<RoleManager<Role>>();

        private UserManager UserManager => OwinContext.Get<UserManager>();

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
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var user = await UserManager.FindByIdAsync(id);

                if (user == null)
                {
                    throw new KeyNotFoundException();
                }

                if (User.IsInRole("Supervisor"))
                {
                    var currentUser =
                        await UserManager.FindByIdAsync(User.GetId());

                    var companyId = currentUser.Company.Id;

                    var company = await CompanyManager.Companies
                        .Include(c => c.Employees)
                        .SingleOrDefaultAsync(c => c.Id == companyId);

                    if (company.Employees.All(e => e.Id != user.Id))
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
            catch (ArgumentNullException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e);
            }
            catch (KeyNotFoundException e)
            {
                return this.CreateError(HttpStatusCode.NotFound, e);
            }
            catch (UnauthorizedAccessException e)
            {
                return this.CreateError(HttpStatusCode.Forbidden, e);
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
                var user = await UserManager.FindByIdAsync(model.UserId);

                user.PasswordHash =
                    UserManager.PasswordHasher.HashPassword(model.Password);

                await UserManager.UpdateSecurityStampAsync(user.Id);
                await UserManager.UpdateAsync(user);
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
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var user = await UserManager.FindByIdAsync(id);

                if (user == null)
                {
                    throw new KeyNotFoundException();
                }

                var roleName = (await UserManager.GetRolesAsync(id)).Single();

                var roles = await RoleManager.Roles.OrderBy(r => r.Index)
                    .ToListAsync();

                var roleNames = roles.Select(r => r.Name);
                var model = new ChangeRoleViewModel(user, roleName, roleNames);
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
                var roleName = model.RoleName;

                if (User.IsInRole("Administrator"))
                {
                    if (roleName == "Owner"
                        || roleName == "Administrator")
                    {
                        throw new AccessViolationException(
                            "You are not permitted to promote users to Owner or Administrator.");
                    }
                }

                var roles = await UserManager.GetRolesAsync(model.UserId);

                await UserManager.RemoveFromRolesAsync(
                    model.UserId,
                    roles.ToArray());

                await UserManager.AddToRoleAsync(model.UserId, roleName);
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
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (User.IsInRole("Supervisor"))
                {
                    var currentUser =
                        await UserManager.FindByIdAsync(User.GetId());

                    var companyId = currentUser.Company.Id;

                    var company = await CompanyManager.Companies
                        .Include(c => c.Employees)
                        .SingleOrDefaultAsync(c => c.Id == companyId);

                    if (company.Employees.All(employee => employee.Id != id))
                    {
                        throw new UnauthorizedAccessException(
                            "You are not permitted to view the details for this user.");
                    }
                }

                var user = await UserManager.FindByIdAsync(id);

                if (user == null)
                {
                    throw new KeyNotFoundException();
                }

                var role =
                    await RoleManager.FindByIdAsync(user.Roles.Single().RoleId);

                var companies = await CompanyManager.Companies.ToListAsync();
                var roles = await RoleManager.Roles.ToListAsync();
                var groups = await GroupManager.Groups.ToListAsync();

                var model = new UserViewModel(
                    user,
                    role,
                    companies,
                    roles,
                    groups);

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
            catch (UnauthorizedAccessException e)
            {
                return this.CreateError(HttpStatusCode.Forbidden, e);
            }
            catch (Exception e)
            {
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

                var role =
                    await RoleManager.FindByIdAsync(user.Roles.Single().RoleId);

                var companies = await CompanyManager.Companies.ToListAsync();
                var roles = await RoleManager.Roles.ToListAsync();
                var groups = await GroupManager.Groups.ToListAsync();

                var model = new UserViewModel(
                    user,
                    role,
                    companies,
                    roles,
                    groups);

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
                var users = await UserManager.Users.Include(u => u.Roles)
                    .ToListAsync();
                var companies = await CompanyManager.Companies.ToListAsync();
                if (RoleManager == null)
                {
                    throw new Exception("Role manager null");
                }

                var roles = await RoleManager.Roles.ToListAsync();
                var groups = await GroupManager.Groups.ToListAsync();
                IList<UserViewModel> model = new List<UserViewModel>();

                foreach (var user in users)
                {
                    var roleId = user.Roles.Single().RoleId;
                    var role = await RoleManager.FindByIdAsync(roleId);

                    model.Add(
                        new UserViewModel(
                            user,
                            role,
                            companies,
                            roles,
                            groups));
                }

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
                GroupManager?.Dispose();
                RoleManager?.Dispose();
                UserManager?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}