using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Web.ViewModels.TravelViewModels;

namespace TravelBook.Web.Controllers;

[Authorize]
public class TravelController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<TravelController> _logger;
    private readonly IMapper _mapper;
    private readonly ITravelRepository _travelRepository;
    public TravelController(UserManager<IdentityUser> userManager,
                            ILogger<TravelController> logger,
                            IMapper mapper,
                            ITravelRepository travelRepository)
    {
        _userManager = userManager;
        _logger = logger;
        _mapper = mapper;
        _travelRepository = travelRepository;
    }
    public async Task<IActionResult> Travels()
    {
        var userId = await GetUserId();
        var travels = await _travelRepository.GetAllTravelForUser(userId);
        var travelsModel = _mapper.Map<IEnumerable<TravelViewModel>>(travels);
        return View(travelsModel);
    }

    public async Task<string> GetUserId()
    {
        var user = await _userManager.GetUserAsync(User);
        return user.Id;
    }
}
