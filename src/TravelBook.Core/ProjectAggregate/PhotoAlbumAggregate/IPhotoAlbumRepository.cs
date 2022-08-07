
namespace TravelBook.Core.ProjectAggregate;

public interface IPhotoAlbumRepository
{
    Task<PhotoAlbum[]> GetAllAlbums(int travelId);
    Task<PhotoAlbum?> GetAlbumById(int photoAlbumId);
    Task<int> Add(PhotoAlbum photoAlbum);
    Task<bool> Delete(int photoAlbumId);
    Task AddPhotosToAlbum(int photoAlbumId, params Photo[] photos);
    Task RemovePhotosFromAlbum(int photoAlbumId, params Photo[] photos);
}
