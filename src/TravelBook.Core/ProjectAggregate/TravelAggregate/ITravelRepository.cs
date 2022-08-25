namespace TravelBook.Core.ProjectAggregate;

public interface ITravelRepository
{
    Task<Travel[]> GetAllUserTravels(string userId);
    Task<(string ownerId, Travel travel)> GetTravelById(int travelId);
    Task<(string ownerId, Article[] articles)> GetAllUserArticles(string userId);
    Task<(string ownerId, Article article)> GetArticleById(int articleId);
    Task<(string ownerId, Article[] articles)> GetArticlesByTravelId(int travelId);
    Task Add(Travel travel);
    Task Delete(Travel travel);
    Task AddArticle(Article article);
    Task EditArticle(Article article);
    Task RemoveArticle(Article article);
}
