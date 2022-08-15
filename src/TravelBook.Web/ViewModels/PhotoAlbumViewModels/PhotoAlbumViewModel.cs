using System.ComponentModel.DataAnnotations;

namespace TravelBook.Web.ViewModels.PhotoAlbumViewModels;

public class PhotoAlbumViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Travel { get; set; }
    [Display(Name = "Quantity")]
    public int NumPhotos { get; set; }
}
