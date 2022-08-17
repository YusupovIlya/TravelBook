using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace TravelBook.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserManager<IdentityUser> _userManager;
        protected readonly IMapper _mapper;
        public BaseController(UserManager<IdentityUser> userManager,
                              IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public string UserId => _userManager.GetUserId(User);

        public bool CheckAccessByUserId(string ownerId) => UserId == ownerId;
    }
}
