using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBook.Web.ViewModels.PhotoAlbumViewModels;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Infrastructure;

namespace TravelBook.Web.Controllers
{
    public class PhotoAlbumsController : BaseController
    {
        private readonly ILogger<TravelController> _logger;
        private readonly IMapper _mapper;
        private readonly IPhotoAlbumRepository _photoAlbumRepository;

        public PhotoAlbumsController(UserManager<IdentityUser> userManager,
                                ILogger<TravelController> logger,
                                IMapper mapper,
                                IPhotoAlbumRepository photoAlbumRepository) : base(userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _photoAlbumRepository = photoAlbumRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Open(int photoAlbumId)
        {
            try
            {
                (string ownerId, PhotoAlbum album) = await _photoAlbumRepository.GetAlbumById(photoAlbumId);
                if (CheckAccessByUserId(ownerId))
                {
                    var photoAlbumModel = _mapper.Map<OpenPhotoAlbumViewModel>(album);
                    return View(photoAlbumModel);
                }
                else
                    return Forbid();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> TravelPhotoAlbums(int travelId)
        {
            try
            {
                (string ownerId, PhotoAlbum[] albums) = await _photoAlbumRepository.GetTravelPhotoAlbums(travelId);
                if (CheckAccessByUserId(ownerId))
                {
                    var photoAlbumsModel = _mapper.Map<IEnumerable<PhotoAlbumViewModel>>(albums);
                    return View("~/Views/PhotoAlbums/ListPhotoAlbums.cshtml", photoAlbumsModel);
                }
                else
                    return Forbid();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        //// GET: PhotoAlbums
        //public async Task<IActionResult> Index()
        //{
        //    var appDbContext = _context.PhotoAlbums.Include(p => p.Travel);
        //    return View(await appDbContext.ToListAsync());
        //}

        //// GET: PhotoAlbums/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.PhotoAlbums == null)
        //    {
        //        return NotFound();
        //    }

        //    var photoAlbum = await _context.PhotoAlbums
        //        .Include(p => p.Travel)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (photoAlbum == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(photoAlbum);
        //}

        //// GET: PhotoAlbums/Create
        //public IActionResult Create()
        //{
        //    ViewData["TravelId"] = new SelectList(_context.Travels, "Id", "UserId");
        //    return View();
        //}

        //// POST: PhotoAlbums/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Title,TravelId,Id")] PhotoAlbum photoAlbum)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(photoAlbum);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["TravelId"] = new SelectList(_context.Travels, "Id", "UserId", photoAlbum.TravelId);
        //    return View(photoAlbum);
        //}

        //// GET: PhotoAlbums/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.PhotoAlbums == null)
        //    {
        //        return NotFound();
        //    }

        //    var photoAlbum = await _context.PhotoAlbums.FindAsync(id);
        //    if (photoAlbum == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["TravelId"] = new SelectList(_context.Travels, "Id", "UserId", photoAlbum.TravelId);
        //    return View(photoAlbum);
        //}

        //// POST: PhotoAlbums/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Title,TravelId,Id")] PhotoAlbum photoAlbum)
        //{
        //    if (id != photoAlbum.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(photoAlbum);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PhotoAlbumExists(photoAlbum.Id))
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
        //    ViewData["TravelId"] = new SelectList(_context.Travels, "Id", "UserId", photoAlbum.TravelId);
        //    return View(photoAlbum);
        //}

        //// GET: PhotoAlbums/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.PhotoAlbums == null)
        //    {
        //        return NotFound();
        //    }

        //    var photoAlbum = await _context.PhotoAlbums
        //        .Include(p => p.Travel)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (photoAlbum == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(photoAlbum);
        //}

        //// POST: PhotoAlbums/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.PhotoAlbums == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.PhotoAlbums'  is null.");
        //    }
        //    var photoAlbum = await _context.PhotoAlbums.FindAsync(id);
        //    if (photoAlbum != null)
        //    {
        //        _context.PhotoAlbums.Remove(photoAlbum);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PhotoAlbumExists(int id)
        //{
        //  return (_context.PhotoAlbums?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
