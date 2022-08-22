using System.ComponentModel.DataAnnotations;
using TravelBook.Core.ProjectAggregate;

namespace TravelBook.Web.ViewModels.TravelViewModels;

public class TravelDeleteViewModel
{
    public int Id { get; set; }
    public string Place { get; set; }

    [Display(Name = "Travel start date: ")]
    public DateTime? DateStartTravel { get; set; }

    [Display(Name = "Travel finish date: ")]
    public DateTime? DateFinishTravel { get; set; }

    [Display(Name = "Albums: ")]
    public int NumAlbums { get; set; }

    [Display(Name = "Articles: ")]
    public int NumArticles { get; set; }
}
