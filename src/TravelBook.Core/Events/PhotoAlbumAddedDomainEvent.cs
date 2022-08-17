namespace TravelBook.Core.Events;

public class PhotoAlbumAddedDomainEvent: PhotoAlbumStateChanged
{
    public PhotoAlbumAddedDomainEvent(PhotoAlbum album)
    {
        PhotoAlbum = album;
        Travel = album.Travel;
        UserId = album.Travel.UserId;
    }
}
