
namespace TravelBook.Core.ProjectAggregate.PhotoAlbumAggregate;

public interface IPhotoAlbumRepository
{
    Task<IEnumerable<PhotoAlbum>> GetAllAlbums(int travelId);
    Task<PhotoAlbum> GetAlbumById(int photoAlbumId);
}
