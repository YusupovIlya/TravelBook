using TravelBook.Core.ProjectAggregate;

namespace TravelBook.Web.Service;

public class FilesService: IFilesService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IConfiguration _configuration;
    public FilesService(IWebHostEnvironment environment, IConfiguration configuration)
    {
        _environment = environment;
        _configuration = configuration;
    }

    public void CreateAlbumFolder(string UserId, int photoAlbumId)
    {
        string albumFolderPath = GetAlbumFolderPath(UserId, photoAlbumId);
        if (!Directory.Exists(albumFolderPath))
            Directory.CreateDirectory(albumFolderPath);
    }

    public void CreateUserFolder(string UserId)
    {
        string userFolderPath = GetUserFolderPath(UserId);
        if(!Directory.Exists(userFolderPath))
            Directory.CreateDirectory(userFolderPath);
    }

    public void DeleteAlbumFolder(string UserId, int photoAlbumId)
    {
        string albumFolderPath = GetAlbumFolderPath(UserId, photoAlbumId);
        if (Directory.Exists(albumFolderPath))
            Directory.Delete(albumFolderPath, true);
    }

    public void DeletePhoto(string relativePath)
    {
        relativePath = relativePath.Substring(1, relativePath.Length - 1);
        relativePath = relativePath.Replace("/", "\\");
        string fullPath = Path.Combine(_environment.WebRootPath, relativePath);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
        else
            throw new FileNotFoundException();
    }

    public string GetAlbumFolderPath(string UserId, int photoAlbumId)
    {
        string userFolderPath = GetUserFolderPath(UserId);
        string albumFolderPath = Path.Combine(userFolderPath, photoAlbumId.ToString());
        return albumFolderPath;
    }

    public string GetUserFolderPath(string UserId)
    {
        string dataFolderName = _configuration
                                    .GetSection("Files")
                                    .GetValue<string>("UsersDataFolderName");

        string rootFolderPath = _environment.WebRootPath;
        string combinedPath = Path.Combine(rootFolderPath, dataFolderName, UserId);

        return combinedPath;
    }

    public async Task<Photo[]> UploadPhotos(IFormFileCollection images, string UserId, int photoAlbumId)
    {
        Photo[] uploadedPhotos = new Photo[images.Count];
        string albumFolderPath = GetAlbumFolderPath(UserId, photoAlbumId);
        for(int i = 0; i < images.Count; i++)
        {
            string imgName = Path.ChangeExtension(Path.GetRandomFileName(), ".jpg");
            string absImgPath = Path.Combine(albumFolderPath, imgName);
            string relativePath = "/" + Path.GetRelativePath(_environment.WebRootPath, absImgPath).Replace("\\", "/");
            using (var stream = File.Create(absImgPath))
            {
                await images[i].CopyToAsync(stream);
            }
            uploadedPhotos[i] = new Photo(relativePath);
        }
        return uploadedPhotos;
    }
}
