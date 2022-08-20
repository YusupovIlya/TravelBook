namespace TravelBook.Core.Events;

public class PhotoAlbumAddedDomainEvent: PhotoAlbumStateChanged
{
    public PhotoAlbumAddedDomainEvent(PhotoAlbum album, string userId)
    {
        PhotoAlbum = album;
        UserId = userId;
    }
}
