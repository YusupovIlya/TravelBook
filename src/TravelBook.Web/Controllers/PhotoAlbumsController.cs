using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBook.Web.ViewModels.PhotoAlbumViewModels;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Core.Events;

namespace TravelBook.Web.Controllers
{
    public class PhotoAlbumsController : BaseController
    {
        private readonly ILogger<PhotoAlbumsController> _logger;
        private readonly IPhotoAlbumRepository _photoAlbumRepository;
        private readonly IFilesService _filesService;
        private readonly IMediator _mediator;
        public PhotoAlbumsController(UserManager<IdentityUser> userManager,
                                     ILogger<PhotoAlbumsController> logger,
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
        public async Task<IActionResult> Open([FromQuery] int photoAlbumId, [FromQuery] string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/travels/all");
            try
            {
                (string ownerId, PhotoAlbum album) = await _photoAlbumRepository.GetAlbumById(photoAlbumId);
                if (CheckAccessByUserId(ownerId))
                {
                    ViewBag.returnUrl = returnUrl;
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
        public async Task<IActionResult> TravelPhotoAlbums([FromQuery] int travelId)
        {
            ViewBag.TravelId = travelId;
            ViewBag.returnUrl = $"{Request.Path}{Request.QueryString}";
            try
            {
                (string ownerId, PhotoAlbum[] albums) = await _photoAlbumRepository.GetTravelPhotoAlbums(travelId);
                if (CheckAccessByUserId(ownerId))
                {
                    ViewBag.IsEmpty = false;
                    var photoAlbumsModel = _mapper.Map<IEnumerable<PhotoAlbumViewModel>>(albums);
                    return View("~/Views/PhotoAlbums/ListPhotoAlbums.cshtml", photoAlbumsModel);
                }
                else
                    return Forbid();
            }
            catch (ArgumentNullException)
            {
                ViewBag.IsEmpty = true;
                return View("~/Views/PhotoAlbums/ListPhotoAlbums.cshtml");
            }
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            ViewBag.returnUrl = $"{Request.Path}{Request.QueryString}";
            try
            {
                ViewBag.IsEmpty = false;
                var photoAlbums = await _photoAlbumRepository.GetAllPhotoAlbumsForUser(UserId);
                var photoAlbumsModel = _mapper.Map<IEnumerable<PhotoAlbumViewModel>>(photoAlbums);
                return View("~/Views/PhotoAlbums/ListPhotoAlbums.cshtml", photoAlbumsModel);
            }
            catch (ArgumentNullException)
            {
                ViewBag.IsEmpty = true;
                return View("~/Views/PhotoAlbums/ListPhotoAlbums.cshtml");
            }
        }


        [HttpGet]
        public IActionResult UploadPhotosToAlbum(int photoAlbumId)
        {
            ViewData["photoAlbumId"] = photoAlbumId;
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> UploadPhotosToAlbum([FromForm] IFormFileCollection images,
                                                             [FromForm] int photoAlbumId)
        {
            try
            {
                (string ownerId, PhotoAlbum album) = await _photoAlbumRepository.GetAlbumById(photoAlbumId);
                if (CheckAccessByUserId(ownerId))
                {
                    Photo[] uploadedPhotos = await _filesService.UploadPhotos(images, UserId, album.Id);
                    await _photoAlbumRepository.AddPhotosToAlbum(album, uploadedPhotos);
                    return RedirectToAction(nameof(Open), new { photoAlbumId = photoAlbumId });
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
        public IActionResult Create([FromQuery] int travelId, [FromQuery] string returnUrl)
        {
            ViewBag.TravelId = travelId;
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] string title, [FromForm] int travelId)
        {
            PhotoAlbum newAlbum = new PhotoAlbum(title, travelId);
            await _photoAlbumRepository.Add(newAlbum);
            await _mediator.Publish(new PhotoAlbumAddedDomainEvent(newAlbum, UserId));
            return RedirectToAction(nameof(TravelPhotoAlbums), new { travelId = travelId });
        }


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

    }
}
