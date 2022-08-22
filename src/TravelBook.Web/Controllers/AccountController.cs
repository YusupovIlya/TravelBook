using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TravelBook.Web.ViewModels.AccountViewModels;
using TravelBook.Core.Events;

namespace TravelBook.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityUser> userManager,
                                 IUserStore<IdentityUser> userStore,
                                 SignInManager<IdentityUser> signInManager,
                                 ILogger<AccountController> logger,
                                 IMapper mapper,
                                 IMediator mediator)
                                 : base(userManager, mapper, mediator)
        {
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
        }


        [AllowAnonymous]
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("AboutMe");
            else
                return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    await _mediator.Publish(new NewUserRegisteredDomainEvent(user.Id, user.UserName));
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(AboutMe));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("AboutMe");
            else
                return View();
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

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
