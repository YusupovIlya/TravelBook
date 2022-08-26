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

    public async Task Add(Travel travel)
    {
        await _context.Travels.AddAsync(travel);
        await _context.SaveChangesAsync();
    }

    public async Task AddArticle(Article article)
    {
        await _context.Articles.AddAsync(article);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Travel travel)
    {
        _context.Travels.Remove(travel);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveArticle(Article article)
    {
        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
    }

    public async Task EditArticle(Article article)
    {
        _context.Entry(article).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<Travel[]> GetAllUserTravels(string userId)
    {
        var allTravelForUser = await _context.Travels
            .Where(t => t.UserId == userId)
            .AsNoTracking()
            .ToArrayAsync();

        return allTravelForUser;
    }

    public async Task<(string ownerId, Article article)> GetArticleById(int articleId)
    {
        var article = await _context.Articles
            .Include(a => a.Travel)
            .FirstOrDefaultAsync(a => a.Id == articleId);
        if (article == null)
            throw new NullReferenceException("Article not found with this id.");
        var ownerId = article.Travel.UserId;
        return (ownerId, article);
    }

    public async Task<(string ownerId, Article[] articles)> GetArticlesByTravelId(int travelId)
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

    public async Task<(string ownerId, Travel travel)> GetTravelById(int travelId)
    {
        var travel = await _context.Travels
            .Include(t => t.Articles)
            .Include(t => t.PhotoAlbums)
                .ThenInclude(pa => pa.Photos)
            .FirstOrDefaultAsync(t => t.Id == travelId);

        if (travel == null)
            throw new NullReferenceException("Travel not found with this id.");

        var ownerId = travel.UserId;

        return (ownerId, travel);
    }

    public async Task<(string ownerId, Article[] articles)> GetAllUserArticles(string userId)
    {
        var articles = await _context.Articles
            .Include(a => a.Travel)
            .Where(a => a.Travel.UserId == userId)
            .AsNoTracking()
            .ToArrayAsync();

        if (articles.Length == 0)
            throw new ArgumentNullException("Not found articles for this user");

        return (userId, articles);
    }
}
