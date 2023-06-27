using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Core.DTO;

namespace SurveyApp.Web.UI.Controllers
{
    public class UserController(SignInManager<IdentityUser> signInManager) : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request, string? ReturnUrl)
        {
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Login", "Invalid Email or Password.");
                return View(request);
            }

            if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                return LocalRedirect(ReturnUrl);
            
            return RedirectToAction("Index","Home");
        }
        // TODO: Can add Register, Logout, ForgotPassword, ResetPassword, ChangePassword, etc. but it destroys the purpose of MVP, since login is the only thing that matters and registeration is done using web api.
    }
}
