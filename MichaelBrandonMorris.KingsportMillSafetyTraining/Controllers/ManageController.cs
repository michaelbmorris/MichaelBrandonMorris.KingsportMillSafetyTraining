using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers.Helpers;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.Manage;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    /// <summary>
    ///     Class ManageController.
    /// </summary>
    /// <seealso cref="Controller" />
    /// TODO Edit XML Comment Template for ManageController
    [Authorize]
    public class ManageController : Controller
    {
        /// <summary>
        ///     The sign in manager
        /// </summary>
        /// TODO Edit XML Comment Template for _signInManager
        private SignInManager _signInManager;

        /// <summary>
        ///     The user manager
        /// </summary>
        /// TODO Edit XML Comment Template for _userManager
        private UserManager _userManager;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ManageController" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public ManageController()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ManageController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// TODO Edit XML Comment Template for #ctor
        public ManageController(
            UserManager userManager,
            SignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        ///     Gets the sign in manager.
        /// </summary>
        /// <value>The sign in manager.</value>
        /// TODO Edit XML Comment Template for SignInManager
        public SignInManager SignInManager
        {
            get => _signInManager ?? GetSignInManager();
            private set => _signInManager = value;
        }

        /// <summary>
        ///     Gets the user manager.
        /// </summary>
        /// <value>The user manager.</value>
        /// TODO Edit XML Comment Template for UserManager
        public UserManager UserManager
        {
            get => _userManager ?? GetUserManager();
            private set => _userManager = value;
        }

        /// <summary>
        ///     Gets the owin context.
        /// </summary>
        /// <value>The owin context.</value>
        /// TODO Edit XML Comment Template for OwinContext
        private IOwinContext OwinContext => HttpContext.GetOwinContext();

        /// <summary>
        ///     Adds the phone number.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for AddPhoneNumber
        public ActionResult AddPhoneNumber()
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
        ///     Adds the phone number.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(
            AddPhoneNumberViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var code =
                    await UserManager.GenerateChangePhoneNumberTokenAsync(
                        User.GetId(),
                        model.Number);

                if (UserManager.SmsService == null)
                {
                    return RedirectToAction(
                        "VerifyPhoneNumber",
                        new
                        {
                            PhoneNumber = model.Number
                        });
                }

                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };

                await UserManager.SmsService.SendAsync(message);

                return RedirectToAction(
                    "VerifyPhoneNumber",
                    new
                    {
                        PhoneNumber = model.Number
                    });
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Changes the password.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for ChangePassword
        [HttpGet]
        public ActionResult ChangePassword()
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
        ///     Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(
            ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await UserManager.ChangePasswordAsync(
                    User.GetId(),
                    model.OldPassword,
                    model.NewPassword);

                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.GetId());

                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, false, false);
                    }

                    return RedirectToAction(
                        "Index",
                        new
                        {
                            Message = ManageMessageId.ChangePasswordSuccess
                        });
                }

                AddErrors(result);
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
        ///     Disables the two factor authentication.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            try
            {
                await UserManager.SetTwoFactorEnabledAsync(User.GetId(), false);
                var user = await UserManager.FindByIdAsync(User.GetId());

                if (user != null)
                {
                    await SignInManager.SignInAsync(user, false, false);
                }

                return RedirectToAction("Index", "Manage");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Enables the two factor authentication.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            try
            {
                await UserManager.SetTwoFactorEnabledAsync(User.GetId(), true);
                var user = await UserManager.FindByIdAsync(User.GetId());

                if (user == null)
                {
                    return RedirectToAction("Index", "Manage");
                }

                const bool isPersistent = false;
                const bool rememberBrowser = false;

                await SignInManager.SignInAsync(
                    user,
                    isPersistent,
                    rememberBrowser);

                return RedirectToAction("Index", "Manage");
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Indexes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for Index
        [HttpGet]
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            try
            {
                switch (message)
                {
                    case ManageMessageId.AddPhoneSuccess:
                        ViewBag.StatusMessage = "Your phone number was added.";
                        break;
                    case ManageMessageId.ChangePasswordSuccess:
                        ViewBag.StatusMessage =
                            "Your password has been changed.";
                        break;
                    case ManageMessageId.SetTwoFactorSuccess:
                        ViewBag.StatusMessage =
                            "Your two-factor authentication provider has been set.";
                        break;
                    case ManageMessageId.SetPasswordSuccess:
                        ViewBag.StatusMessage = "Your password has been set.";
                        break;
                    case ManageMessageId.RemoveLoginSuccess: goto default;
                    case ManageMessageId.RemovePhoneSuccess:
                        ViewBag.StatusMessage =
                            "Your phone number was removed.";
                        break;
                    case ManageMessageId.Error:
                        ViewBag.StatusMessage = "An error has occurred.";
                        break;
                    case null: goto default;
                    default:
                        ViewBag.StatusMessage = string.Empty;
                        break;
                }

                var userId = User.GetId();

                var model = new IndexViewModel
                {
                    HasPassword = HasPassword(),
                    PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                    TwoFactor =
                        await UserManager.GetTwoFactorEnabledAsync(userId),
                    Logins = await UserManager.GetLoginsAsync(userId),
                    BrowserRemembered =
                        await AuthenticationManager
                            .TwoFactorBrowserRememberedAsync(userId)
                };

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
        ///     Links the login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            try
            {
                return new ChallengeResult(
                    provider,
                    Url.Action("LinkLoginCallback", "Manage"),
                    User.GetId());
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Links the login callback.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for LinkLoginCallback
        [HttpGet]
        public async Task<ActionResult> LinkLoginCallback()
        {
            try
            {
                var loginInfo = await AuthenticationManager
                    .GetExternalLoginInfoAsync(XsrfKey, User.GetId());

                if (loginInfo == null)
                {
                    return RedirectToAction(
                        "ManageLogins",
                        new
                        {
                            Message = ManageMessageId.Error
                        });
                }

                var result =
                    await UserManager.AddLoginAsync(
                        User.GetId(),
                        loginInfo.Login);

                return result.Succeeded
                    ? RedirectToAction("ManageLogins")
                    : RedirectToAction(
                        "ManageLogins",
                        new
                        {
                            Message = ManageMessageId.Error
                        });
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Manages the logins.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for ManageLogins
        [HttpGet]
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            try
            {
                ViewBag.StatusMessage =
                    message == ManageMessageId.RemoveLoginSuccess
                        ? "The external login was removed."
                        : message == ManageMessageId.Error
                            ? "An error has occurred."
                            : "";

                var user = await UserManager.FindByIdAsync(User.GetId());

                if (user == null)
                {
                    return View("Error");
                }

                var userLogins = await UserManager.GetLoginsAsync(User.GetId());

                var otherLogins = AuthenticationManager
                    .GetExternalAuthenticationTypes()
                    .Where(
                        auth => userLogins.All(
                            ul => auth.AuthenticationType != ul.LoginProvider))
                    .ToList();

                ViewBag.ShowRemoveButton =
                    user.PasswordHash != null || userLogins.Count > 1;

                return View(
                    new ManageLoginsViewModel
                    {
                        CurrentLogins = userLogins,
                        OtherLogins = otherLogins
                    });
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Removes the login.
        /// </summary>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="providerKey">The provider key.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(
            string loginProvider,
            string providerKey)
        {
            try
            {
                ManageMessageId? message;

                var result = await UserManager.RemoveLoginAsync(
                    User.GetId(),
                    new UserLoginInfo(loginProvider, providerKey));

                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.GetId());

                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, false, false);
                    }

                    message = ManageMessageId.RemoveLoginSuccess;
                }
                else
                {
                    message = ManageMessageId.Error;
                }

                return RedirectToAction(
                    "ManageLogins",
                    new
                    {
                        Message = message
                    });
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Removes the phone number.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            try
            {
                var result =
                    await UserManager.SetPhoneNumberAsync(User.GetId(), null);

                if (!result.Succeeded)
                {
                    return RedirectToAction(
                        "Index",
                        new
                        {
                            Message = ManageMessageId.Error
                        });
                }

                var user = await UserManager.FindByIdAsync(User.GetId());

                if (user != null)
                {
                    await SignInManager.SignInAsync(user, false, false);
                }

                return RedirectToAction(
                    "Index",
                    new
                    {
                        Message = ManageMessageId.RemovePhoneSuccess
                    });
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Sets the password.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// TODO Edit XML Comment Template for SetPassword
        [HttpGet]
        public ActionResult SetPassword()
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
        ///     Sets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result =
                    await UserManager.AddPasswordAsync(
                        User.GetId(),
                        model.NewPassword);

                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.GetId());

                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, false, false);
                    }

                    return RedirectToAction(
                        "Index",
                        new
                        {
                            Message = ManageMessageId.SetPasswordSuccess
                        });
                }

                AddErrors(result);
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
        ///     Verifies the phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for VerifyPhoneNumber
        [HttpGet]
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            try
            {
                await UserManager.GenerateChangePhoneNumberTokenAsync(
                    User.GetId(),
                    phoneNumber);

                return phoneNumber == null
                    ? View("Error")
                    : View(
                        new VerifyPhoneNumberViewModel
                        {
                            PhoneNumber = phoneNumber
                        });
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e);
            }
        }

        /// <summary>
        ///     Verifies the phone number.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        /// TODO Edit XML Comment Template for VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(
            VerifyPhoneNumberViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await UserManager.ChangePhoneNumberAsync(
                    User.GetId(),
                    model.PhoneNumber,
                    model.Code);

                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.GetId());

                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, false, false);
                    }

                    return RedirectToAction(
                        "Index",
                        new
                        {
                            Message = ManageMessageId.AddPhoneSuccess
                        });
                }

                ModelState.AddModelError("", "Failed to verify phone");
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
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        ///     Gets the sign in manager.
        /// </summary>
        /// <returns>SignInManager.</returns>
        /// TODO Edit XML Comment Template for GetSignInManager
        private SignInManager GetSignInManager()
        {
            return OwinContext.Get<SignInManager>();
        }

        /// <summary>
        ///     Gets the user manager.
        /// </summary>
        /// <returns>UserManager.</returns>
        /// TODO Edit XML Comment Template for GetUserManager
        private UserManager GetUserManager()
        {
            return OwinContext.GetUserManager<UserManager>();
        }

        #region Helpers

        /// <summary>
        ///     The XSRF key
        /// </summary>
        /// TODO Edit XML Comment Template for XsrfKey
        private const string XsrfKey = "XsrfId";

        /// <summary>
        ///     Gets the authentication manager.
        /// </summary>
        /// <value>The authentication manager.</value>
        /// TODO Edit XML Comment Template for AuthenticationManager
        private IAuthenticationManager AuthenticationManager => OwinContext
            .Authentication;

        /// <summary>
        ///     Adds the errors.
        /// </summary>
        /// <param name="result">The result.</param>
        /// TODO Edit XML Comment Template for AddErrors
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        /// <summary>
        ///     Determines whether this instance has password.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance has password;
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for HasPassword
        private bool HasPassword()
        {
            var user = UserManager.FindById(User.GetId());
            return user?.PasswordHash != null;
        }

        /// <summary>
        ///     Enum ManageMessageId
        /// </summary>
        /// TODO Edit XML Comment Template for ManageMessageId
        public enum ManageMessageId
        {
            /// <summary>
            ///     The add phone success
            /// </summary>
            /// TODO Edit XML Comment Template for AddPhoneSuccess
            AddPhoneSuccess,

            /// <summary>
            ///     The change password success
            /// </summary>
            /// TODO Edit XML Comment Template for ChangePasswordSuccess
            ChangePasswordSuccess,

            /// <summary>
            ///     The set two factor success
            /// </summary>
            /// TODO Edit XML Comment Template for SetTwoFactorSuccess
            SetTwoFactorSuccess,

            /// <summary>
            ///     The set password success
            /// </summary>
            /// TODO Edit XML Comment Template for SetPasswordSuccess
            SetPasswordSuccess,

            /// <summary>
            ///     The remove login success
            /// </summary>
            /// TODO Edit XML Comment Template for RemoveLoginSuccess
            RemoveLoginSuccess,

            /// <summary>
            ///     The remove phone success
            /// </summary>
            /// TODO Edit XML Comment Template for RemovePhoneSuccess
            RemovePhoneSuccess,

            /// <summary>
            ///     The error
            /// </summary>
            /// TODO Edit XML Comment Template for Error
            Error
        }

        #endregion
    }
}