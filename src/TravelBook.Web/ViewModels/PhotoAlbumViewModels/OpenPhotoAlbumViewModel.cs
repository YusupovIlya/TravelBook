using TravelBook.Core.ProjectAggregate;

namespace TravelBook.Web.ViewModels.PhotoAlbumViewModels;

public class OpenPhotoAlbumViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Travel { get; set; }
    public List<Photo> Photos { get; set; }
}
