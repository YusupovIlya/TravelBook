namespace TravelBook.Web.ViewModels.PhotoAlbumViewModels;

public class EditPhotoViewModel
{
    public int Id { get; set; }
    public string ImagePath { get; set; }
    public string? Title { get; set; }
    public string? Place { get; set; }
    public int PhotoAlbumId { get; set; }
}
