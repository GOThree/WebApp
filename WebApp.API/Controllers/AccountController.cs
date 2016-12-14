using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using OpenIddict.Core;
using WebApp.API.Models.AccountViewModels;
using WebApp.API.Models.Db;
using WebApp.API.Services;

namespace WebApp.API.Controllers
{
    [Authorize]
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            IAccountService accountService,
            IEmailSender emailSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _accountService = accountService;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        // POST: /Account/Register
        [HttpPost("register")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                AddErrors(result);
            }

            // If we got this far, something failed.
            return BadRequest(ModelState);
        }

        // POST: /Account/ChangePassword
        [HttpPost("changePassword")]
        [Produces("application/json")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return NotFound("User was no found!");
                }
                IdentityResult result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok();
                }
                AddErrors(result);
            }

            // If we got this far, something failed.
            return BadRequest(ModelState);
        }

        [Produces("application/json")]
        [HttpPost("forgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    string callbackUrl = _accountService.GenerateResetUrl(code);
                    //TODO: Move to service/generator
                    string emailBody = $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>";
                    await _emailSender.SendEmailAsync(model.Email, "Reset Password", emailBody);
                }
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // POST: /Account/ResetPassword
        [Produces("application/json")]
        [HttpPost("resetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return Ok();
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            AddErrors(result);
            return BadRequest(ModelState);
        }

        [HttpGet("userinfo")]
        public async Task<IActionResult> Userinfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(OpenIdConnectConstants.Errors.InvalidGrant, "The user profile is no longer available.");
                return BadRequest(ModelState);
            }

            var claims = new JObject();

            // Note: the "sub" claim is a mandatory claim and must be included in the JSON response.
            claims[OpenIdConnectConstants.Claims.Subject] = user.Id.ToString();

            if (User.HasClaim(OpenIdConnectConstants.Claims.Scope, OpenIdConnectConstants.Scopes.Email))
            {
                claims[OpenIdConnectConstants.Claims.Email] = user.Email;
            }

            if (User.HasClaim(OpenIdConnectConstants.Claims.Scope, OpenIdConnectConstants.Scopes.Phone))
            {
                claims[OpenIdConnectConstants.Claims.PhoneNumber] = user.PhoneNumber;
            }

            if (User.HasClaim(OpenIdConnectConstants.Claims.Scope, OpenIdConnectConstants.Scopes.Profile))
            {
                claims[OpenIdConnectConstants.Claims.Name] = user.Firstname;
                claims[OpenIdConnectConstants.Claims.FamilyName] = user.Lastname;
            }

            if (User.HasClaim(OpenIdConnectConstants.Claims.Scope, OpenIddictConstants.Scopes.Roles))
            {
                claims[OpenIddictConstants.Claims.Roles] = JArray.FromObject(await _userManager.GetRolesAsync(user));
            }

            // Note: the complete list of standard claims supported by the OpenID Connect specification
            // can be found here: http://openid.net/specs/openid-connect-core-1_0.html#StandardClaims

            return Json(claims);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}
