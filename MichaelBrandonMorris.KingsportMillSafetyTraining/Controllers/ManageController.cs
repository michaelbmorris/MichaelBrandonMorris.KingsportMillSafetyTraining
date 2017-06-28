using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.PrincipalExtensions;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Identity.Manage;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? GetSignInManager();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? GetUserManager();
            private set => _userManager = value;
        }

        private IOwinContext OwinContext => HttpContext.GetOwinContext();

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            try
            {
                return new AccountController.ChallengeResult(
                    provider,
                    Url.Action("LinkLoginCallback", "Manage"),
                    User.GetId());
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

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
                    e.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        private ApplicationSignInManager GetSignInManager()
        {
            return OwinContext.Get<ApplicationSignInManager>();
        }

        private ApplicationUserManager GetUserManager()
        {
            return OwinContext.GetUserManager<ApplicationUserManager>();
        }

        #region Helpers

        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => OwinContext
            .Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.GetId());
            return user?.PasswordHash != null;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}