namespace TravelBook.Core.ProjectAggregate;

public class Travel: Entity, IAggregateRoot
{
    public string UserId { get; private set; }
    public Place Place { get; set; }
    public DateTime? DateStartTravel { get; set; }
    public DateTime? DateFinishTravel { get; set; }

    private List<PhotoAlbum> _photoAlbums = new List<PhotoAlbum>();
    public IEnumerable<PhotoAlbum> PhotoAlbums => _photoAlbums.AsReadOnly();

    private List<Article> _articles = new List<Article>();
    public IEnumerable<Article> Articles => _articles.AsReadOnly();

    public Travel() {}

    public Travel(string userId, Place place, DateTime dateStartTravel, DateTime dateFinishTravel)
    {
        UserId = userId;
        Place = place;
        DateStartTravel = dateStartTravel;
        DateFinishTravel = dateFinishTravel;
    }
}

