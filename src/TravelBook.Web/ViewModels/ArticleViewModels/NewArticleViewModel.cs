using System.ComponentModel.DataAnnotations;

namespace TravelBook.Web.ViewModels.ArticleViewModels;

public class NewArticleViewModel
{
    [Required]
    public string Title { get; set; }
    public string Text { get; set; }
    public int TravelId { get; set; }
}
