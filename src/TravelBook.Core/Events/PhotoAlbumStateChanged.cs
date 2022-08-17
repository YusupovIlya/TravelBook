namespace TravelBook.Core.Events;

public abstract class PhotoAlbumStateChanged: INotification
{
    public Travel Travel { get; protected set; }
    public PhotoAlbum PhotoAlbum { get; protected set; }
    public string UserId { get; protected set; }
}
