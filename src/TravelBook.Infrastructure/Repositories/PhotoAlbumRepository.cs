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

    public async Task<int> Add(PhotoAlbum photoAlbum)
    {
        await _context.PhotoAlbums.AddAsync(photoAlbum);
        await _context.SaveChangesAsync();
        return photoAlbum.Id;
    }

    public async Task<bool> Delete(int photoAlbumId)
    {
        bool res;
        PhotoAlbum? album = await GetAlbumById(photoAlbumId);
        _context.PhotoAlbums.Remove(album);
        res = true;
        await _context.SaveChangesAsync();
        return res;
    }

    public async Task AddPhotosToAlbum(int photoAlbumId, params Photo[] photos)
    {
        PhotoAlbum album = await GetAlbumById(photoAlbumId);
        album.AddPhotos(photos);
        await _context.SaveChangesAsync();
    }
    public async Task RemovePhotosFromAlbum(int photoAlbumId, params Photo[] photos)
    {
        PhotoAlbum album = await GetAlbumById(photoAlbumId);
        album.RemovePhotos(photos);
        await _context.SaveChangesAsync();
    }

    public async Task<PhotoAlbum?> GetAlbumById(int photoAlbumId)
    {
        var album = await _context.PhotoAlbums
            .Include(pa => pa.Photos)
            .FirstOrDefaultAsync(p => p.Id == photoAlbumId);
        if (album == null)
            throw new NullReferenceException("Album not found with this id.");
        else
            return album;
    }

    public async Task<PhotoAlbum[]> GetAllAlbums(int travelId)
    {
        var allTravelAlbums = await _context.PhotoAlbums
            .Include(pa => pa.Photos)
            .AsNoTracking()
            .ToArrayAsync();

        return allTravelAlbums;
    }
}
