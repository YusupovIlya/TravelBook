
namespace TravelBook.Core.ProjectAggregate;

public interface IPhotoAlbumRepository
{
    Task<(string ownerId, PhotoAlbum[] albums)> GetAllUserPhotoAlbums(string userId);
    Task<(string ownerId, PhotoAlbum[] albums)> GetTravelPhotoAlbums(int travelId);
    Task<(string ownerId, PhotoAlbum album)> GetAlbumById(int photoAlbumId);
    Task Add(PhotoAlbum photoAlbum);
    Task Delete(PhotoAlbum photoAlbum);
    Task AddPhotosToAlbum(PhotoAlbum album, params Photo[] photos);
    Task<(string ownerId, Photo photo)> GetPhotoById(int photoId);
    Task EditPhoto(Photo photo);
    Task RemovePhotosFromAlbum(int photoAlbumId, params Photo[] photos);
}
