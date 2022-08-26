using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TravelBook.Core.SeedWork;

namespace TravelBook.Web.Controllers;

[Authorize]
public class BaseController : Controller
{
    protected readonly UserManager<IdentityUser> _userManager;
    protected readonly IMapper _mapper;
    protected readonly IMediator _mediator;
    public BaseController(UserManager<IdentityUser> userManager,
                          IMapper mapper,
                          IMediator mediator)
    {
        _userManager = userManager;
        _mapper = mapper;
        _mediator = mediator;
    }

    protected string UserId => _userManager.GetUserId(User);

    protected bool CheckAccessByUserId(string ownerId) => UserId == ownerId;

    protected async Task<IActionResult> ControllerAction<D, T, E> (D id,
                                                                  Func<D, Task<(string ownerId, T obj)>> request,
                                                                  Func<T, IActionResult> action)
                                                                  where T: Entity
                                                                  where E: Exception
    {
        try
        {
            (string ownerId, T obj) = await request(id);
            if (CheckAccessByUserId(ownerId))
            {
                return action(obj);
            }
            else
                return Forbid();
        }
        catch (E)
        {
            return NotFound();
        }
    }

    protected async Task<IActionResult> ControllerAction<D, T, E>(D id,
                                                                  Func<D, Task<(string ownerId, T[] obj)>> request,
                                                                  Func<T[], IActionResult> action,
                                                                  Func<IActionResult> ifWasException)
                                                                  where T : Entity
                                                                  where E : Exception
    {
        try
        {
            (string ownerId, T[] obj) = await request(id);
            if (CheckAccessByUserId(ownerId))
            {
                return action(obj);
            }
            else
                return Forbid();
        }
        catch (E)
        {
            return ifWasException();
        }
    }

    protected async Task<IActionResult> ControllerAction<D, T, E>(D id,
                                                                  Func<D, Task<(string ownerId, T obj)>> request,
                                                                  Func<T, Task<IActionResult>> action)
                                                                  where T : Entity
                                                                  where E : Exception
    {
        try
        {
            (string ownerId, T obj) = await request(id);
            if (CheckAccessByUserId(ownerId))
            {
                return await action(obj);
            }
            else
                return Forbid();
        }
        catch (E)
        {
            return NotFound();
        }
    }
}
