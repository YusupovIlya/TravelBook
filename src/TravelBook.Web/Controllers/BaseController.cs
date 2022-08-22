using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TravelBook.Core.SeedWork;

namespace TravelBook.Web.Controllers;


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


    /// <summary>
    /// Controller action with checking access to data only for the owner 
    /// </summary>
    /// <typeparam name="T">Entity type in repository</typeparam>
    /// <typeparam name="E">Exception type in repository</typeparam>
    /// <param name="id">id for search in repository</param>
    /// <param name="request">Actions for get data from repository</param>
    /// <param name="action">Actions if the user is the owner</param>
    /// <returns>Controller action</returns>
    protected async Task<IActionResult> ControllerAction<T, E> (int id,
                                                          Func<int, Task<(string ownerId, T obj)>> request,
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

    protected async Task<IActionResult> ControllerAction<T, E>(int id,
                                                          Func<int, Task<(string ownerId, T[] obj)>> request,
                                                          Func<T[], IActionResult> action)
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
            return NotFound();
        }
    }


    protected async Task<IActionResult> ControllerAction<T, E>(int id,
                                                               Func<int, Task<(string ownerId, T[] obj)>> request,
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

    protected async Task<IActionResult> ControllerAction<T, E>(int id,
                                                          Func<int, Task<(string ownerId, T obj)>> request,
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

    protected async Task<IActionResult> ControllerAction<T, E>(int id,
                                                          Func<int, Task<(string ownerId, T obj)>> request,
                                                          Func<T, IActionResult> action,
                                                          Func<IActionResult> ifWasException)
                                                          where T : Entity
                                                          where E : Exception
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
            return ifWasException();
        }
    }
}
