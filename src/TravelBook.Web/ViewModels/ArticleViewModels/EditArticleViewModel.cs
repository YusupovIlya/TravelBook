using System.ComponentModel.DataAnnotations;

namespace TravelBook.Web.ViewModels.ArticleViewModels;

public class EditArticleViewModel
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Text { get; set; }
    public int TravelId { get; set; }
}
