
namespace TravelBook.Core.ProjectAggregate;

public interface IPhotoAlbumRepository
{
    Task<(string ownerId, PhotoAlbum[] albums)> GetTravelPhotoAlbums(int travelId);
    Task<(string ownerId, PhotoAlbum album)> GetAlbumById(int photoAlbumId);
    Task<int> Add(PhotoAlbum photoAlbum);
    Task Delete(PhotoAlbum photoAlbum);
    Task AddPhotosToAlbum(PhotoAlbum album, params Photo[] photos);
    //Task RemovePhotosFromAlbum(int photoAlbumId, params Photo[] photos);
}
