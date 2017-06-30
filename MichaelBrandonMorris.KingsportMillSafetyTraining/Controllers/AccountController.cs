using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers.Helpers;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Db.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    /// Class AccountController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for AccountController
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// The sign in manager
        /// </summary>
        /// TODO Edit XML Comment Template for _signInManager
        private ApplicationSignInManager _signInManager;
        /// <summary>
        /// The user manager
        /// </summary>
        /// TODO Edit XML Comment Template for _userManager
        private ApplicationUserManager _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public AccountController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// TODO Edit XML Comment Template for #ctor
        public AccountController(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        /// Gets the sign in manager.
        /// </summary>
        /// <value>The sign in manager.</value>
        /// TODO Edit XML Comment Template for SignInManager
        public ApplicationSignInManager SignInManager
        {
            get => _signInManager
                   ?? HttpContext.GetOwinContext()
                       .Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        /// <value>The user manager.</value>
        /// TODO Edit XML Comment Template for UserManager
        public ApplicationUserManager UserManager
        {
            get => _userManager
                   ?? HttpContext.GetOwinContext()
                       .GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        /// <summary>
        /// Confirms the email.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for ConfirmEmail
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                if (userId == null
                    || code == null)
                {
                    return View("Error");
                }

                var result = await UserManager.ConfirmEmailAsync(userId, code);
                return View(result.Succeeded ? "ConfirmEmail" : "Error");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Externals the login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for ExternalLogin
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            try
            {
                return new ChallengeResult(
                    provider,
                    Url.Action(
                        "ExternalLoginCallback",
                        "Account",
                        new
                        {
                            ReturnUrl = returnUrl
                        }));
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Externals the login callback.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for ExternalLoginCallback
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            try
            {
                var loginInfo = await AuthenticationManager
                    .GetExternalLoginInfoAsync();

                if (loginInfo == null)
                {
                    return RedirectToAction("Login");
                }

                var result =
                    await SignInManager.ExternalSignInAsync(loginInfo, false);

                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut: return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction(
                            "SendCode",
                            new
                            {
                                ReturnUrl = returnUrl,
                                RememberMe = false
                            });
                    case SignInStatus.Failure: goto default;
                    default:
                        ViewBag.ReturnUrl = returnUrl;
                        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                        return View(
                            "ExternalLoginConfirmation",
                            new ExternalLoginConfirmationViewModel
                            {
                                Email = loginInfo.Email
                            });
                }
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Externals the login confirmation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for ExternalLoginConfirmation
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(
            ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Manage");
                }

                if (ModelState.IsValid)
                {
                    var info =
                        await AuthenticationManager.GetExternalLoginInfoAsync();

                    if (info == null)
                    {
                        return View("ExternalLoginFailure");
                    }

                    var user = new User
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };

                    var result = await UserManager.CreateAsync(user);

                    if (result.Succeeded)
                    {
                        result =
                            await UserManager.AddLoginAsync(user.Id, info.Login);

                        if (result.Succeeded)
                        {
                            await SignInManager.SignInAsync(user, false, false);
                            return RedirectToLocal(returnUrl);
                        }
                    }

                    AddErrors(result);
                }

                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }      
        }

        /// <summary>
        /// Externals the login failure.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for ExternalLoginFailure
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ExternalLoginFailure()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

        /// <summary>
        /// Forgots the password.
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
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }         
        }

        /// <summary>
        /// Forgots the password.
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

                if (user == null
                    || !await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    return View("ForgotPasswordConfirmation");
                }

                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }          
        }

        /// <summary>
        /// Forgots the password confirmation.
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
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }       
        }

        /// <summary>
        /// Logins the specified return URL.
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
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }     
        }

        /// <summary>
        /// Logins the specified model.
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
                    case SignInStatus.Success: return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut: return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction(
                            "SendCode",
                            new
                            {
                                ReturnUrl = returnUrl,
                                model.RememberMe
                            });
                    case SignInStatus.Failure: goto default;
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }           
        }

        /// <summary>
        /// Logs the off.
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
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }     
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for Register
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }        
        }

        /// <summary>
        /// Registers the specified model.
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
                    CompanyName = model.CompanyName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false, false);
                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }          
        }

        /// <summary>
        /// Resets the password.
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
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }        
        }

        /// <summary>
        /// Resets the password.
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
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }

                var result = await UserManager.ResetPasswordAsync(
                    user.Id,
                    model.Code,
                    model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }

                AddErrors(result);
                return View();
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }          
        }

        /// <summary>
        /// Resets the password confirmation.
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
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }        
        }

        /// <summary>
        /// Sends the code.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for SendCode
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> SendCode(
            string returnUrl,
            bool rememberMe)
        {
            try
            {
                var userId = await SignInManager.GetVerifiedUserIdAsync();

                if (userId == null)
                {
                    return View("Error");
                }

                var userFactors =
                    await UserManager.GetValidTwoFactorProvidersAsync(userId);

                var factorOptions = userFactors.Select(
                        purpose => new SelectListItem
                        {
                            Text = purpose,
                            Value = purpose
                        })
                    .ToList();

                return View(
                    new SendCodeViewModel
                    {
                        Providers = factorOptions,
                        ReturnUrl = returnUrl,
                        RememberMe = rememberMe
                    });
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }         
        }

        /// <summary>
        /// Sends the code.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for SendCode
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (!await SignInManager.SendTwoFactorCodeAsync(
                    model.SelectedProvider))
                {
                    return View("Error");
                }

                return RedirectToAction(
                    "VerifyCode",
                    new
                    {
                        Provider = model.SelectedProvider,
                        model.ReturnUrl,
                        model.RememberMe
                    });
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }        
        }

        /// <summary>
        /// Verifies the code.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for VerifyCode
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> VerifyCode(
            string provider,
            string returnUrl,
            bool rememberMe)
        {
            try
            {
                if (!await SignInManager.HasBeenVerifiedAsync())
                {
                    return View("Error");
                }

                return View(
                    new VerifyCodeViewModel
                    {
                        Provider = provider,
                        ReturnUrl = returnUrl,
                        RememberMe = rememberMe
                    });
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }        
        }

        /// <summary>
        /// Verifies the code.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for VerifyCode
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await SignInManager.TwoFactorSignInAsync(
                    model.Provider,
                    model.Code,
                    model.RememberMe,
                    model.RememberBrowser);

                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(model.ReturnUrl);
                    case SignInStatus.LockedOut: return View("Lockout");
                    case SignInStatus.RequiresVerification: goto default;
                    case SignInStatus.Failure: goto default;
                    default:
                        ModelState.AddModelError("", "Invalid code.");
                        return View(model);
                }
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            } 
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        /// TODO Edit XML Comment Template for Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
        
        /// <summary>
        /// Gets the authentication manager.
        /// </summary>
        /// <value>The authentication manager.</value>
        /// TODO Edit XML Comment Template for AuthenticationManager
        private IAuthenticationManager AuthenticationManager => HttpContext
            .GetOwinContext()
            .Authentication;

        /// <summary>
        /// Adds the errors.
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
        /// Redirects to local.
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