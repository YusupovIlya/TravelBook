using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Core.Events;
using TravelBook.Web.ViewModels.PhotoAlbumViewModels;
using TravelBook.Web.Service;
using Microsoft.AspNetCore.Authorization;
using MediatR;

namespace TravelBook.Web.Controllers
{
    [Authorize]
    public class PhotosController : BaseController
    {
        private readonly ILogger<TravelController> _logger;
        private readonly IPhotoAlbumRepository _photoAlbumRepository;
        private readonly IFilesService _filesService;
        private readonly IMediator _mediator;
        public PhotosController(UserManager<IdentityUser> userManager,
                                ILogger<TravelController> logger,
                                IMapper mapper,
                                IFilesService filesService,
                                IMediator mediator,
                                IPhotoAlbumRepository photoAlbumRepository)
                                : base(userManager, mapper)
        {
            _logger = logger;
            _photoAlbumRepository = photoAlbumRepository;
            _mediator = mediator;
            _filesService = filesService;
        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery] int photoId)
        {
            try
            {
                (string ownerId, Photo photo) = await _photoAlbumRepository.GetPhotoById(photoId);
                if (CheckAccessByUserId(ownerId))
                {
                    var photoModel = _mapper.Map<EditPhotoViewModel>(photo);
                    return View(photoModel);
                }
                else
                    return Forbid();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditPhotoViewModel model)
        {
            Photo updatedPhoto = _mapper.Map<Photo>(model);
            await _photoAlbumRepository.UpdatePhoto(updatedPhoto);
            return RedirectToAction(nameof(PhotoAlbumsController.Open),
                                    nameof(PhotoAlbumsController).CutController(),
                                    new { photoAlbumId = model.PhotoAlbumId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromQuery] int photoId)
        {
            try
            {
                (string ownerId, Photo photo) = await _photoAlbumRepository.GetPhotoById(photoId);

                if (CheckAccessByUserId(ownerId))
                {
                    await _photoAlbumRepository.RemovePhotosFromAlbum(photo.PhotoAlbumId, photo);

                    await _mediator.Publish(new PhotoDeletedDomainEvent(photo));

                    return RedirectToAction(nameof(PhotoAlbumsController.Open),
                        nameof(PhotoAlbumsController).CutController(),
                        new { photoAlbumId = photo.PhotoAlbumId });
                }
                else
                    return Forbid();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
