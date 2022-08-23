using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelBook.Core.ProjectAggregate;
using TravelBook.Core.Events;
using TravelBook.Web.ViewModels.PhotoAlbumViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TravelBook.Web.Controllers
{
    [Authorize]
    public class PhotosController : BaseController
    {
        private readonly ILogger<PhotosController> _logger;
        private readonly IPhotoAlbumRepository _photoAlbumRepository;
        public PhotosController(UserManager<IdentityUser> userManager,
                                ILogger<PhotosController> logger,
                                IMapper mapper,
                                IMediator mediator,
                                IPhotoAlbumRepository photoAlbumRepository)
                                : base(userManager, mapper, mediator)
        {
            _logger = logger;
            _photoAlbumRepository = photoAlbumRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery] int photoId)
        {
            return await ControllerAction<int, Photo, NullReferenceException>(

                    photoId,

                    async (id) => await _photoAlbumRepository.GetPhotoById(id),

                    (Photo photo) =>
                    {
                        var photoModel = _mapper.Map<EditPhotoViewModel>(photo);
                        return View(photoModel);
                    });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditPhotoViewModel model)
        {
            Photo updatedPhoto = _mapper.Map<Photo>(model);
            await _photoAlbumRepository.EditPhoto(updatedPhoto);
            return RedirectToAction(nameof(PhotoAlbumsController.Open),
                                    nameof(PhotoAlbumsController).CutController(),
                                    new { photoAlbumId = model.PhotoAlbumId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromQuery] int photoId)
        {
            return await ControllerAction<int, Photo, NullReferenceException>(

                    photoId,

                    async (id) => await _photoAlbumRepository.GetPhotoById(id),

                    async (Photo photo) =>
                    {
                        await _mediator.Publish(new PhotoDeletedDomainEvent(photo));

                        await _photoAlbumRepository.RemovePhotosFromAlbum(photo.PhotoAlbumId, photo);

                        return RedirectToAction(nameof(PhotoAlbumsController.Open),
                            nameof(PhotoAlbumsController).CutController(),
                            new { photoAlbumId = photo.PhotoAlbumId });
                    });
        }
    }
}
