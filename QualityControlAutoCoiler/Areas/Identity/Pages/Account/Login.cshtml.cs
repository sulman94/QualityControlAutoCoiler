using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjectX.Controllers;
using Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectX.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    public class LoginModel : PageModel
    {
        private readonly UserManager<QualityControlAutoCoilerUser> _userManager;
        private readonly SignInManager<QualityControlAutoCoilerUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IUserAccessService _userAccessService;
        public LoginModel(SignInManager<QualityControlAutoCoilerUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<QualityControlAutoCoilerUser> userManager, IUserAccessService userAccessService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userAccessService = userAccessService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            //[EmailAddress]
            [Display(Name = "Username/Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                QualityControlAutoCoilerUser xUser;
                if (Input.Email.Contains("@"))
                    xUser = await _userManager.FindByEmailAsync(Input.Email);
                else
                    xUser = await _userManager.FindByNameAsync(Input.Email);
                if (xUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
                if (xUser.Status == 0)
                {
                    ModelState.AddModelError(string.Empty, "User InActive - Please contact your Administrator");
                    return Page();
                }
                var result = await _signInManager.PasswordSignInAsync(xUser.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    List<UserPermissionsModel> permissions = await _userAccessService.GetUserPermissionsByRoleIdAsync(xUser.RoleTemplateID);
                    var GetMenu = await _userAccessService.GetMenu(xUser.RoleTemplateID);
                    var serializedpermissions = JsonSerializer.Serialize(permissions);
                    var SerializedMenu = JsonSerializer.Serialize(GetMenu);
                    identity.AddClaim(new Claim("Name", xUser.FirstName + " " + xUser.LastName));
                    identity.AddClaim(new Claim("UserId", xUser.Id.ToString()));
                    identity.AddClaim(new Claim("RoleId", xUser.RoleTemplateID.ToString()));
                    HttpContext.Session.SetString("RoleId", xUser.RoleTemplateID.ToString());
                    HttpContext.Session.SetString("UserId", xUser.Id.ToString());
                    HttpContext.Session.SetString("UserName", xUser.UserName);
                    HttpContext.Session.SetString("DynamicMenu", SerializedMenu);
                    HttpContext.Session.SetString("UserPermissions", serializedpermissions);
                    HttpContext.Session.SetString("userEmail", xUser.Email);
                    _logger.LogInformation("User logged in.");
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = Input.RememberMe });
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
