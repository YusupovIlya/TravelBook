using TravelBook.Core.ProjectAggregate;

namespace TravelBook.Web.ViewModels.TravelViewModels;

public class TravelViewModel
{
    public int Id { get; set; }
    public Place Place { get; private set; }
    public DateTime? DateStartTravel { get; set; }
    public DateTime? DateFinishTravel { get; set; }
}
