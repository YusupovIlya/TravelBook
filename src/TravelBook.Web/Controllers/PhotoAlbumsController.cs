using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBook.Web.ViewModels.PhotoAlbumViewModels;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Core.Events;

namespace TravelBook.Web.Controllers;

public class PhotoAlbumsController : BaseController
{
    private readonly ILogger<PhotoAlbumsController> _logger;
    private readonly IPhotoAlbumRepository _photoAlbumRepository;
    private readonly IFilesService _filesService;
    public PhotoAlbumsController(UserManager<IdentityUser> userManager,
                                 ILogger<PhotoAlbumsController> logger,
                                 IMapper mapper,
                                 IPhotoAlbumRepository photoAlbumRepository,
                                 IFilesService filesService,
                                 IMediator mediator)
                                 : base(userManager, mapper, mediator)
    {
        _logger = logger;
        _photoAlbumRepository = photoAlbumRepository;
        _filesService = filesService;
    }

    [HttpGet]
    public async Task<IActionResult> Open([FromQuery] int photoAlbumId,
                                          [FromQuery] string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/travels/all");

        return await ControllerAction<int, PhotoAlbum, NullReferenceException>(

                photoAlbumId,

                async (id) => await _photoAlbumRepository.GetAlbumById(id),

                (PhotoAlbum album) =>
                {
                    ViewBag.returnUrl = returnUrl;
                    var photoAlbumModel = _mapper.Map<OpenPhotoAlbumViewModel>(album);
                    return View(photoAlbumModel);
                });
    }

    [HttpGet]
    public async Task<IActionResult> TravelPhotoAlbums([FromQuery] int travelId)
    {
        ViewBag.TravelId = travelId;
        ViewBag.returnUrl = $"{Request.Path}{Request.QueryString}";

        return await ControllerAction<int, PhotoAlbum, ArgumentNullException>(

                travelId,

                async (id) => await _photoAlbumRepository.GetTravelPhotoAlbums(id),

                (PhotoAlbum[] albums) =>
                {
                    ViewBag.IsEmpty = false;
                    var photoAlbumsModel = _mapper.Map<IEnumerable<PhotoAlbumViewModel>>(albums);
                    return View("~/Views/PhotoAlbums/ListPhotoAlbums.cshtml", photoAlbumsModel);
                },

                () => {
                    ViewBag.IsEmpty = true;
                    return View("~/Views/PhotoAlbums/ListPhotoAlbums.cshtml");
                });
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        ViewBag.returnUrl = $"{Request.Path}{Request.QueryString}";
        return await ControllerAction<string, PhotoAlbum, ArgumentNullException>(

                UserId,

                async (id) => await _photoAlbumRepository.GetAllUserPhotoAlbums(id),

                (PhotoAlbum[] albums) =>
                {
                    ViewBag.IsEmpty = false;
                    var photoAlbumsModel = _mapper.Map<IEnumerable<PhotoAlbumViewModel>>(albums);
                    return View("~/Views/PhotoAlbums/ListPhotoAlbums.cshtml", photoAlbumsModel);
                },

                () => {
                    ViewBag.IsEmpty = true;
                    return View("~/Views/PhotoAlbums/ListPhotoAlbums.cshtml");
                });
    }


    [HttpGet]
    public IActionResult UploadPhotosToAlbum([FromQuery] int photoAlbumId)
    {
        ViewData["photoAlbumId"] = photoAlbumId;
        return View();
    }
   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadPhotosToAlbum([FromForm] IFormFileCollection images,
                                                         [FromForm] int photoAlbumId)
    {
        return await ControllerAction<int, PhotoAlbum, NullReferenceException>(

                photoAlbumId,

                async (id) => await _photoAlbumRepository.GetAlbumById(id),

                async (PhotoAlbum album) =>
                {
                    Photo[] uploadedPhotos = await _filesService.UploadPhotos(images, UserId, album.Id);
                    await _photoAlbumRepository.AddPhotosToAlbum(album, uploadedPhotos);
                    return RedirectToAction(nameof(Open), new { photoAlbumId = photoAlbumId });
                });
    }

    [HttpGet]
    public IActionResult Create([FromQuery] int travelId, 
                                [FromQuery] string? returnUrl = null)
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

        return await ControllerAction<int, PhotoAlbum, NullReferenceException>(

                photoAlbumId,

                async (id) => await _photoAlbumRepository.GetAlbumById(id),

                (PhotoAlbum album) =>
                {
                    ViewBag.returnUrl = returnUrl;
                    var photoAlbumModel = _mapper.Map<PhotoAlbumViewModel>(album);
                    return View(photoAlbumModel);
                });
    }


    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int photoAlbumId, string returnUrl)
    {
        return await ControllerAction<int, PhotoAlbum, NullReferenceException>(

                photoAlbumId,

                async (id) => await _photoAlbumRepository.GetAlbumById(id),

                async (PhotoAlbum album) =>
                {
                    await _photoAlbumRepository.Delete(album);
                    await _mediator.Publish(new PhotoAlbumDeletedDomainEvent(album));
                    return LocalRedirect(returnUrl);
                });
    }

}
