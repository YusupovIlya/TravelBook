using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Web.ViewModels.TravelViewModels;

namespace TravelBook.Web.Controllers
{
    public class TravelsController : BaseController
    {
        private readonly ILogger<TravelsController> _logger;
        private readonly ITravelRepository _travelRepository;
        public TravelsController(UserManager<IdentityUser> userManager,
                                 ILogger<TravelsController> logger,
                                 IMapper mapper,
                                 ITravelRepository travelRepository)
                                 : base(userManager, mapper)
        {
            _logger = logger;
            _travelRepository = travelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var travels = await _travelRepository.GetAllTravelForUser(UserId);
            var travelsModel = _mapper.Map<IEnumerable<TravelViewModel>>(travels);
            return View(travelsModel);
        }

        //// GET: Travels/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //    try
        //    {
        //        var travel = await _travelRepository.GetTravelById(id);
        //    }
        //    catch (NullReferenceException)
        //    {
        //        return NotFound();
        //    }
        //    var travelModel = _mapper.Map<Task>(Task);
        //    return View(travelModel);
        //}

        //// GET: Travels/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Travels/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("UserId,DateStartTravel,DateFinishTravel,Id")] Travel travel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(travel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(travel);
        //}

        //// GET: Travels/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Travels == null)
        //    {
        //        return NotFound();
        //    }

        //    var travel = await _context.Travels.FindAsync(id);
        //    if (travel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(travel);
        //}

        //// POST: Travels/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("UserId,DateStartTravel,DateFinishTravel,Id")] Travel travel)
        //{
        //    if (id != travel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(travel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TravelExists(travel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(travel);
        //}

        //// GET: Travels/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Travels == null)
        //    {
        //        return NotFound();
        //    }

        //    var travel = await _context.Travels
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (travel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(travel);
        //}

        //// POST: Travels/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Travels == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.Travels'  is null.");
        //    }
        //    var travel = await _context.Travels.FindAsync(id);
        //    if (travel != null)
        //    {
        //        _context.Travels.Remove(travel);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TravelExists(int id)
        //{
        //  return (_context.Travels?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
