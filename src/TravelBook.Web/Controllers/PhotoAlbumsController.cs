using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBook.Web.ViewModels.PhotoAlbumViewModels;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Core.Events;
using TravelBook.Infrastructure;
using TravelBook.Web.Service;
using MediatR;

namespace TravelBook.Web.Controllers
{
    public class PhotoAlbumsController : BaseController
    {
        private readonly ILogger<TravelController> _logger;
        private readonly IPhotoAlbumRepository _photoAlbumRepository;
        private readonly IFilesService _filesService;
        private readonly IMediator _mediator;
        public PhotoAlbumsController(UserManager<IdentityUser> userManager,
                                     ILogger<TravelController> logger,
                                     IMapper mapper,
                                     IPhotoAlbumRepository photoAlbumRepository,
                                     IFilesService filesService,
                                     IMediator mediator)
                                     : base(userManager, mapper)
        {
            _logger = logger;
            _photoAlbumRepository = photoAlbumRepository;
            _filesService = filesService;
            _mediator = mediator;
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
                    ViewBag.returnUrl = $"{Request.Path}{Request.QueryString}";
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

        [HttpGet]
        public IActionResult UploadPhotosToAlbum(int photoAlbumId)
        {
            ViewData["photoAlbumId"] = photoAlbumId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadPhotosToAlbum([FromForm] IFormFileCollection images, [FromForm] int photoAlbumId)
        {
            try
            {
                (string ownerId, PhotoAlbum album) = await _photoAlbumRepository.GetAlbumById(photoAlbumId);
                if (CheckAccessByUserId(ownerId))
                {
                    Photo[] uploadedPhotos = await _filesService.UploadPhotos(images, UserId, album.Id);
                    await _photoAlbumRepository.AddPhotosToAlbum(album, uploadedPhotos);
                    return RedirectToAction("Open", new { photoAlbumId = photoAlbumId });
                }
                else
                    return Forbid();
            }
            catch (NullReferenceException)
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

        // GET: PhotoAlbums/Delete/5
        public async Task<IActionResult> Delete(int photoAlbumId, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/travels/all");
            try
            {
                (string ownerId, PhotoAlbum album) = await _photoAlbumRepository.GetAlbumById(photoAlbumId);
                if (CheckAccessByUserId(ownerId))
                {
                    ViewBag.returnUrl = returnUrl;
                    var photoAlbumModel = _mapper.Map<PhotoAlbumViewModel>(album);
                    return View(photoAlbumModel);
                }
                else
                    return Forbid();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        // POST: PhotoAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int photoAlbumId, string returnUrl)
        {
            try
            {
                (string ownerId, PhotoAlbum album) = await _photoAlbumRepository.GetAlbumById(photoAlbumId);
                if (CheckAccessByUserId(ownerId))
                {
                    await _photoAlbumRepository.Delete(album);
                    await _mediator.Publish(new PhotoAlbumDeletedDomainEvent(album));
                    return LocalRedirect(returnUrl);
                }
                else
                    return Forbid();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        //private bool PhotoAlbumExists(int id)
        //{
        //    return (_context.PhotoAlbums?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
