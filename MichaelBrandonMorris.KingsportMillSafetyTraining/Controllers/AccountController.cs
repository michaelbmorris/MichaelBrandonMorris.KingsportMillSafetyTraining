using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class AccountController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for AccountController
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        ///     The sign in manager
        /// </summary>
        /// TODO Edit XML Comment Template for _signInManager
        private SignInManager _signInManager;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AccountController" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public AccountController()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// TODO Edit XML Comment Template for #ctor
        public AccountController(SignInManager signInManager)
        {
            SignInManager = signInManager;
        }

        /// <summary>
        ///     Gets the sign in manager.
        /// </summary>
        /// <value>The sign in manager.</value>
        /// TODO Edit XML Comment Template for SignInManager
        public SignInManager SignInManager
        {
            get => _signInManager ?? OwinContext.Get<SignInManager>();
            private set => _signInManager = value;
        }

        /// <summary>
        ///     Gets the authentication manager.
        /// </summary>
        /// <value>The authentication manager.</value>
        /// TODO Edit XML Comment Template for AuthenticationManager
        private IAuthenticationManager AuthenticationManager => HttpContext
            .GetOwinContext()
            .Authentication;

        private CompanyManager CompanyManager =>
            OwinContext.Get<CompanyManager>();

        private IOwinContext OwinContext => HttpContext.GetOwinContext();

        private UserManager UserManager => OwinContext.Get<UserManager>();

        /// <summary>
        ///     Forgots the password.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for ForgotPassword
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotPassword()
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

        /// <summary>
        ///     Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for ForgotPassword
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(
            ForgotPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await UserManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    return View("ForgotPasswordConfirmation");
                }

                var code =
                    await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                if (Request.Url == null)
                {
                    throw new Exception();
                }

                var callbackUrl = Url.Action(
                    "ResetPassword",
                    "Account",
                    new
                    {
                        userId = user.Id,
                        code
                    },
                    Request.Url.Scheme);

                using (var smtpClient = new SmtpClient())
                {
                    using (var mailMessage = new MailMessage
                    {
                        Body =
                            $"Please reset your password by clicking <a href=\"{callbackUrl}\">here</a>",
                        IsBodyHtml = true,
                        Subject = "Reset Password"
                    })
                    {
                        var toAddress = new MailAddress(
                            user.Email,
                            $"{user.FirstName} {user.LastName}");

                        mailMessage.To.Add(toAddress);
                        smtpClient.Send(mailMessage);
                    }
                }

                return RedirectToAction(
                    "ForgotPasswordConfirmation",
                    "Account");
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Forgots the password confirmation.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for ForgotPasswordConfirmation
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotPasswordConfirmation()
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

        /// <summary>
        ///     Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for Login
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(
            LoginViewModel model,
            string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await SignInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    false);

                switch (result)
                {
                    case SignInStatus.Success:
                        await UserManager.SetLatestLogOnDateTime(model.Email);
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut: return View("Lockout");
                    case SignInStatus.RequiresVerification: goto default;
                    case SignInStatus.Failure: goto default;
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Logs the off.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                AuthenticationManager.SignOut(
                    DefaultAuthenticationTypes.ApplicationCookie);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Registers this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Register
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            try
            {
                var companies = await CompanyManager.Companies
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                var model = new RegisterViewModel(companies);
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for Register
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = new User
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    OtherCompanyName = model.OtherCompanyName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email
                };

                var result =
                    await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    user = await UserManager.FindByEmailAsync(user.Email);
                    await UserManager.AddToRoleAsync(user.Id, "User");
                    var company =
                        await CompanyManager.FindByIdAsync(model.CompanyId);
                    await UserManager.SetCompany(user.Id, company);
                    return RedirectToAction("Index", "Training");
                }

                model.Companies = await CompanyManager.Companies
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                AddErrors(result);
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Resets the password.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for ResetPassword
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ResetPassword(string code)
        {
            try
            {
                return code == null ? View("Error") : View();
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for ResetPassword
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(
            ResetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await UserManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    return RedirectToAction(
                        "ResetPasswordConfirmation",
                        "Account");
                }

                var result = await UserManager.ResetPasswordAsync(
                    user.Id,
                    model.Code,
                    model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(
                        "ResetPasswordConfirmation",
                        "Account");
                }

                AddErrors(result);
                return View();
            }
            catch (Exception e)
            {
                return this.CreateError(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///     Resets the password confirmation.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for ResetPasswordConfirmation
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ResetPasswordConfirmation()
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
                UserManager?.Dispose();

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        ///     Adds the errors.
        /// </summary>
        /// <param name="result">The result.</param>
        /// TODO Edit XML Comment Template for AddErrors
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        /// <summary>
        ///     Redirects to local.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for RedirectToLocal
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}