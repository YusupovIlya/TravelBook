namespace TravelBook.Core.ProjectAggregate;

public class PhotoAlbum: Entity
{
    private List<Photo> _photos = new List<Photo>();
    public IEnumerable<Photo> Photos => _photos.AsReadOnly();
    public string Title { get; set; }
    public int TravelId { get; set; }
    public Travel? Travel { get; set; }

    public PhotoAlbum(string title, int travelId)
    {
        Title = title;
        TravelId = travelId;
    }

    public void AddItem(Photo newPhoto)
    {
        _photos.Add(newPhoto);
    }
    public void RemoveItem(Photo photo)
    {
        _photos.Remove(photo);
    }
}
