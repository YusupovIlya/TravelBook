namespace TravelBook.Core.ProjectAggregate;
public class Photo: Entity
{
    public string ImagePath { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Place { get; set; }
    public int PhotoAlbumId { get; set; }
    public PhotoAlbum? PhotoAlbum { get; set; }

    public Photo(){}
    public Photo(string imagePath)
    {
        ImagePath = imagePath;
    }

    public Photo(string imagePath, string title, int photoAlbumId)
        :this(imagePath)
    {
        Title = title;
        PhotoAlbumId = photoAlbumId;
    }

    public string GetAlbumName()
    {
        string result = this.PhotoAlbum == null ? "Album isn't defined": PhotoAlbum.Title;
        return result;
    }
}
