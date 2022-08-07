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

    public void AddPhotos(params Photo[] newPhotos)
    {
        _photos.AddRange(newPhotos);
    }
    public void RemovePhotos(params Photo[] photos)
    {
        foreach (Photo photo in photos)
            _photos.Remove(photo);
    }
}
