namespace TravelBook.Core.ProjectAggregate;
public class Photo: Entity
{
    public string ImagePath { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Place Place { get; set; }
    public int PhotoAlbumId { get; set; }
    public PhotoAlbum? PhotoAlbum { get; set; }

    public Photo(string imagePath, string title, int photoAlbumId)
    {
        ImagePath = imagePath;
        Title = title;
        PhotoAlbumId = photoAlbumId;
    }

    public string GetAlbumName()
    {
        string result = this.PhotoAlbum == null ? "Album isn't defined": PhotoAlbum.Title;
        return result;
    }
}
