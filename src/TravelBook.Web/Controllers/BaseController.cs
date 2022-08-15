using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace TravelBook.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public BaseController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public string UserId => _userManager.GetUserId(User);

        public bool CheckAccessByUserId(string ownerId) => UserId == ownerId;
    }
}
