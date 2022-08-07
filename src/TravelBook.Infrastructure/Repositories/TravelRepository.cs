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

    public async Task<Travel> GetTravelById(int travelId)
    {
        var travel = await _context.Travels
            .Include(t => t.Articles)
            .Include(t => t.Albums)
                .ThenInclude(pa => pa.Photos)
            .FirstOrDefaultAsync(t => t.Id == travelId);
        if (travel == null)
            throw new NullReferenceException("Travel not found with this id.");
        else
            return travel;
    }
}
