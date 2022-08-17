namespace TravelBook.Core.Events;

public class PhotoAlbumDeletedDomainEvent: PhotoAlbumStateChanged
{
    public PhotoAlbumDeletedDomainEvent(PhotoAlbum album)
    {
        PhotoAlbum = album;
        Travel = album.Travel;
        UserId = album.Travel.UserId;
    }
}
