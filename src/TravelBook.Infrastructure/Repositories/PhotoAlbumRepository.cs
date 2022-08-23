using Microsoft.EntityFrameworkCore;
using TravelBook.Core.ProjectAggregate;


namespace TravelBook.Infrastructure.Repositories;

public class PhotoAlbumRepository: IPhotoAlbumRepository
{
    private readonly AppDbContext _context;

    public PhotoAlbumRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(PhotoAlbum photoAlbum)
    {
        await _context.PhotoAlbums.AddAsync(photoAlbum);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(PhotoAlbum photoAlbum)
    {
        _context.PhotoAlbums.Remove(photoAlbum);
        await _context.SaveChangesAsync();
    }

    public async Task AddPhotosToAlbum(PhotoAlbum album, params Photo[] photos)
    {
        album.AddPhotos(photos);
        await _context.SaveChangesAsync();
    }
    public async Task EditPhoto(Photo photo)
    {
        _context.Entry(photo).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task RemovePhotosFromAlbum(int photoAlbumId, params Photo[] photos)
    {
        (string ownerId, PhotoAlbum album) = await GetAlbumById(photoAlbumId);
        album.RemovePhotos(photos);
        await _context.SaveChangesAsync();
    }

    public async Task<(string ownerId, PhotoAlbum album)> GetAlbumById(int photoAlbumId)
    {
        var album = await _context.PhotoAlbums
            .Include(pa => pa.Photos)
            .Include(pa => pa.Travel)
            .FirstOrDefaultAsync(p => p.Id == photoAlbumId);

        if (album == null)
            throw new NullReferenceException("Photoalbum not found with this id.");

        var ownerId = album.Travel.UserId;

        return (ownerId, album);
    }

    public async Task<(string ownerId, PhotoAlbum[] albums)> GetTravelPhotoAlbums(int travelId)
    {
        var allTravelAlbums = await _context.PhotoAlbums
            .Include(pa => pa.Photos)
            .Include(pa => pa.Travel)
            .Where(pa => pa.TravelId == travelId)
            .AsNoTracking()
            .ToArrayAsync();

        if (allTravelAlbums.Length == 0)
            throw new ArgumentNullException("Not found albums for with this travelId");

        var ownerId = allTravelAlbums.First().Travel.UserId;

        return (ownerId, allTravelAlbums);
    }

    public async Task<(string ownerId, Photo photo)> GetPhotoById(int photoId)
    {
        var photo = await _context.Photos
            .Include(p => p.PhotoAlbum)
                .ThenInclude(pa => pa.Travel)
            .FirstOrDefaultAsync(p => p.Id == photoId);

        if (photo == null)
            throw new NullReferenceException("Photo not found with this id.");

        var ownerId = photo.PhotoAlbum.Travel.UserId;

        return (ownerId, photo);
    }

    public async Task<(string ownerId, PhotoAlbum[] albums)> GetAllUserPhotoAlbums(string userId)
    {
        var allAlbumsForUser = await _context.PhotoAlbums
            .Include(p => p.Travel)
            .Include(p => p.Photos)
            .Where(p => p.Travel.UserId == userId)
            .AsNoTracking()
            .ToArrayAsync();

        if (allAlbumsForUser.Length == 0)
            throw new ArgumentNullException("This user doesn't have photoalbums.");

        return (userId, allAlbumsForUser);
    }
}
