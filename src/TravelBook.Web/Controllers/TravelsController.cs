using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Web.ViewModels.TravelViewModels;

namespace TravelBook.Web.Controllers;

public class TravelsController : BaseController
{
    private readonly ILogger<TravelsController> _logger;
    private readonly ITravelRepository _travelRepository;
    public TravelsController(UserManager<IdentityUser> userManager,
                             ILogger<TravelsController> logger,
                             IMapper mapper,
                             IMediator mediator,
                             ITravelRepository travelRepository)
                             : base(userManager, mapper, mediator)
    {
        _logger = logger;
        _travelRepository = travelRepository;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var travels = await _travelRepository.GetAllUserTravels(UserId);
        var travelsModel = _mapper.Map<IEnumerable<TravelViewModel>>(travels);
        return View(travelsModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["userId"] = UserId;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NewTravelViewModel model)
    {
        if (ModelState.IsValid)
        {
            Travel newTravel = _mapper.Map<Travel>(model);
            await _travelRepository.Add(newTravel);
            return RedirectToAction(nameof(All));
        }
        return View(model);
    }

    public async Task<IActionResult> Delete([FromQuery] int travelId)
    {
        return await ControllerAction<int, Travel, NullReferenceException>(

                travelId,

                async (id) => await _travelRepository.GetTravelById(id),

                (Travel travel) =>
                {
                    var travelModel = _mapper.Map<TravelDeleteViewModel>(travel);
                    return View(travelModel);
                });
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromForm] int travelId)
    {

        return await ControllerAction<int, Travel, NullReferenceException>(

                travelId,

                async (id) => await _travelRepository.GetTravelById(id),

                async (Travel travel) =>
                {
                    await _travelRepository.Delete(travel);
                    return RedirectToAction(nameof(All));
                });
    }
}
