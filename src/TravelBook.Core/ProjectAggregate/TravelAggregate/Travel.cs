namespace TravelBook.Core.ProjectAggregate;

public class Travel: Entity, IAggregateRoot
{
    public string? GetUserId => _userId;
    private string? _userId;
    public Place Place { get; private set; }
    public DateTime DateStartTravel { get; set; }
    public DateTime DateFinishTravel { get; set; }

    private List<PhotoAlbum> _photoAlbums = new List<PhotoAlbum>();
    public IEnumerable<PhotoAlbum> Albums => _photoAlbums.AsReadOnly();

    private List<Article> _articles = new List<Article>();
    public IEnumerable<Article> Articles => _articles.AsReadOnly();

    public Travel(string? userId)
    {
        _userId = userId;
    }

    public Travel(string? userId, Place place, DateTime dateStartTravel, DateTime dateFinishTravel,
                  List<PhotoAlbum> photoAlbums, List<Article> articles)
    {
        _userId = userId;
        Place = place;
        DateStartTravel = dateStartTravel;
        DateFinishTravel = dateFinishTravel;
        _photoAlbums = photoAlbums;
        _articles = articles;
    }

    public void AddAlbum(PhotoAlbum newPhotoAlbum)
    {
        _photoAlbums.Add(newPhotoAlbum);
    }
    public void RemoveAlbum(PhotoAlbum photoAlbum)
    {
        _photoAlbums.Remove(photoAlbum);
    }
    public void AddArticle(Article newArticle)
    {
        _articles.Add(newArticle);
    }
    public void RemoveArticle(Article article)
    {
        _articles.Remove(article);
    }
}

