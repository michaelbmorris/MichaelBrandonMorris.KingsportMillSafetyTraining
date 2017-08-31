using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Views;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Linq;
using System.Linq.Expressions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class ResultsController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for ResultsController
    [Authorize]
    public class ResultsController : Controller
    {
        private IOwinContext OwinContext => HttpContext.GetOwinContext();

        private TrainingResultManager TrainingResultManager => OwinContext
            .Get<TrainingResultManager>();

        private UserManager UserManager => OwinContext.Get<UserManager>();

        private CompanyManager CompanyManager =>
            OwinContext.Get<CompanyManager>();

        private GroupManager GroupManager => OwinContext.Get<GroupManager>();

        private RoleManager<Role> RoleManager => OwinContext
            .Get<RoleManager<Role>>();

        private static Expression<Func<TrainingResult, User>> TrainingResultUser =>
            trainingResult => trainingResult.User;

        /// <summary>
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nType: 'int'\nName: 'id'
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     You do not
        ///     have permission to view this.
        /// </exception>
        /// TODO Edit XML Comment Template for Details
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var trainingResult = await TrainingResultManager.TrainingResults
                    .Include(TrainingResultUser)
                    .SingleOrDefaultAsync(t => t.Id == id);

                var model = trainingResult.AsViewModel();

                if (await User.IsOwnTrainingResult(id.Value))
                {
                    model.IsUserTrainingResult = true;
                }

                if (User.IsInRole("Owner")
                    || User.IsInRole("Administrator")
                    || User.IsInRole("Security")
                    || User.IsInRole("Collaborator")
                    || User.IsInRole("Supervisor")
                    && await User.IsEmployeeTrainingResult(id.Value)
                    || await User.IsOwnTrainingResult(id.Value))
                {
                    return View(model);
                }

                return this.CreateError(
                    HttpStatusCode.Forbidden,
                    new Exception("You are not permitted to access this."));
            }
            catch (UnauthorizedAccessException e)
            {
                return this.CreateError(HttpStatusCode.Forbidden, e);
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
        ///     Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Index
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            if (!User.IsInRole("Administrator")
                && !User.IsInRole("Owner")
                && !User.IsInRole("Security")
                && !User.IsInRole("Collaborator")
                && !User.IsInRole("Supervisor"))
            {
                return RedirectToAction(
                    "UserResults",
                    new
                    {
                        id = User.GetId()
                    });
            }

            var trainingResults =
                await TrainingResultManager.TrainingResults.ToListAsync();

            IList<TrainingResultViewModel> model;

            if (User.IsInRole("Supervisor"))
            {
                model = new List<TrainingResultViewModel>();

                foreach (var trainingResult in trainingResults)
                {
                    if (await User.IsEmployeeTrainingResult(trainingResult.Id))
                    {
                        model.Add(new TrainingResultViewModel(trainingResult));
                    }
                }
            }
            else
            {
                model = trainingResults.AsViewModels();
            }

            
            return View(model);
        }

        /// <summary>
        ///     Users the results.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Parameter
        ///     missing.\nName: 'id'\nType: 'string'
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        ///     You do not
        ///     have permission to view this.
        /// </exception>
        /// TODO Edit XML Comment Template for UserResults
        [HttpGet]
        public async Task<ActionResult> UserResults(string id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (!User.IsInRole("Owner")
                    && !User.IsInRole("Administrator")
                    && !User.IsInRole("Security")
                    && !User.IsInRole("Collaborator")
                    && (!User.IsInRole("Supervisor")
                        || !await User.IsEmployee(id))
                    && User.GetId() != id)
                {
                    return this.CreateError(
                        HttpStatusCode.Forbidden,
                        new Exception("You are not permitted to access this."));
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

                var model = new UserTrainingResultsViewModel(
                    new UserViewModel(user, role, companies, roles, groups),
                    user.GetTrainingResults().AsViewModels());

                return View(model);
            }
            catch (ArgumentNullException e)
            {
                return this.CreateError(HttpStatusCode.BadRequest, e);
            }
            catch (UnauthorizedAccessException e)
            {
                return this.CreateError(HttpStatusCode.Forbidden, e);
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
                TrainingResultManager?.Dispose();
                UserManager?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}