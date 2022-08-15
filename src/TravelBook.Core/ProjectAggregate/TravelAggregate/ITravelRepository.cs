namespace TravelBook.Core.ProjectAggregate;

public interface ITravelRepository
{
    Task<Travel[]> GetAllTravelForUser(string userId);
    Task<Travel> GetTravelById(int travelId);
    Task<(string ownerId, Article)> GetArticleById(int articleId);
    Task<(string ownerId, Article[])> GetArticlesByTravelId(int travelId);
    Task<int> Add(Travel travel);
    Task<bool> Delete(int travelId);
}
