namespace TravelBook.Core.Events;

public class PhotoDeletedDomainEvent: PhotoAlbumStateChanged
{
    public Photo Photo { get; private set; }
    public PhotoDeletedDomainEvent(Photo photo)
    {
        Photo = photo;
        UserId = photo.PhotoAlbum.Travel.UserId;
    }
}
