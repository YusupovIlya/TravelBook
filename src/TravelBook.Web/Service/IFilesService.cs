using TravelBook.Core.ProjectAggregate;

namespace TravelBook.Web.Service;

public interface IFilesService
{
    void CreateUserFolder(string UserId);
    string GetUserFolderPath(string UserId);
    void CreateAlbumFolder(string UserId, int photoAlbumId);
    string GetAlbumFolderPath(string UserId, int photoAlbumId);
    void DeleteAlbumFolder(string UserId, int photoAlbumId);
    Task<Photo[]> UploadPhotos(IFormFileCollection images, string UserId, int photoAlbumId);

}
