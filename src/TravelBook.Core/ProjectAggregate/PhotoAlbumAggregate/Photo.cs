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

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Photo))
            return false;

        if (Object.ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        Photo item = (Photo)obj;

        if (item.ImagePath == this.ImagePath &&
           item.Place == this.Place &&
           item.PhotoAlbumId == this.PhotoAlbumId &&
           item.Title == this.Title)
            return true;
        else
            return false;
    }
}
