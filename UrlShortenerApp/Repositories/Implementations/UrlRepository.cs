using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Data.DbContexts;
using UrlShortenerApp.Entities;
using UrlShortenerApp.Repositories.Interfaces;

namespace UrlShortenerApp.Repositories.Implementations;

public class UrlRepository : IUrlRepository
{
    private readonly ApplicationDbContext _context;

    public UrlRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Url?> GetByShortenedUrlAsync(string shortUrl)
    {
        return await _context.Urls.FirstOrDefaultAsync(u => u.ShortenedUrl == shortUrl);
    }

    public async Task<Url?> GetByIdAsync(Guid id)
    {
        return await _context.Urls.Include(u => u.CreatedBy).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<Url>> GetAllAsync()
    {
        return await _context.Urls.Include(u => u.CreatedBy).ToListAsync();
    }

    public async Task<bool> ExistsAsync(string originalUrl)
    {
        return await _context.Urls.AnyAsync(u => u.OriginalUrl == originalUrl);
    }

    public async Task AddAsync(Url url)
    {
        await _context.Urls.AddAsync(url);
    }

    public async Task DeleteAsync(Url url)
    {
        _context.Urls.Remove(url);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}