namespace TravelBook.Core.ProjectAggregate;

public interface ITravelRepository
{
    Task<IEnumerable<Travel>> GetAllTravelForUser(string userId);
    Task<Travel> GetTravelById(int travelId);
}
