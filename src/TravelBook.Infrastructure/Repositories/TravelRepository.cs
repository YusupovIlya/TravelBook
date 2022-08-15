using Microsoft.EntityFrameworkCore;
using TravelBook.Core.ProjectAggregate;

namespace TravelBook.Infrastructure.Repositories;

public class TravelRepository : ITravelRepository
{
    private readonly AppDbContext _context;

    public TravelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Add(Travel travel)
    {
        await _context.Travels.AddAsync(travel);
        await _context.SaveChangesAsync();
        return travel.Id;
    }

    public async Task<bool> Delete(int travelId)
    {
        bool res;
        Travel? travel = await GetTravelById(travelId);
        _context.Travels.Remove(travel);
        res = true;
        await _context.SaveChangesAsync();
        return res;
    }

    public async Task<Travel[]> GetAllTravelForUser(string userId)
    {
        var allTravelForUser = await _context.Travels
            .Where(t => t.UserId == userId)
            .AsNoTracking()
            .ToArrayAsync();

        return allTravelForUser;
    }

    public async Task<(string ownerId, Article)> GetArticleById(int articleId)
    {
        var article = await _context.Articles
            .Include(a => a.Travel)
            .FirstOrDefaultAsync(a => a.Id == articleId);
        if (article == null)
            throw new NullReferenceException("Article not found with this id.");
        var ownerId = article.Travel.UserId;
        return (ownerId, article);
    }

    public async Task<(string ownerId, Article[])> GetArticlesByTravelId(int travelId)
    {
        var articles = await _context.Articles
            .Include(a => a.Travel)
            .Where(a => a.TravelId == travelId)
            .AsNoTracking()
            .ToArrayAsync();

        if (articles.Length == 0)
            throw new ArgumentNullException("Not found articles for with this travelId");

        var ownerId = articles.First().Travel.UserId; 

        return (ownerId, articles);
    }

    public async Task<Travel> GetTravelById(int travelId)
    {
        var travel = await _context.Travels
            .Include(t => t.Articles)
            .Include(t => t.PhotoAlbums)
                .ThenInclude(pa => pa.Photos)
            .FirstOrDefaultAsync(t => t.Id == travelId);
        if (travel == null)
            throw new NullReferenceException("Travel not found with this id.");
        else
            return travel;
    }
}
