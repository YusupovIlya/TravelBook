using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TravelBook.Web.ViewModels.AccountViewModels;

namespace TravelBook.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 ILogger<AccountController> logger,
                                 IMapper mapper)
                                 : base(userManager, mapper)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("AboutMe");
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
                else {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"User - {model.Email} logged in.");
                        return RedirectToAction(nameof(AboutMe));
                    }
                    else
                    {
                        _logger.LogWarning($"User - {model.Email} failed login!");
                        ModelState.AddModelError(string.Empty, "Invalid password attempt.");
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AboutMe()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _mapper.Map<AboutMeViewModel>(user);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
