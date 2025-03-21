using UrlShortenerApp.Entities;

namespace UrlShortenerApp.Repositories.Interfaces;

public interface IUrlRepository
{
    Task<Url?> GetByShortenedUrlAsync(string shortUrl);
    Task<Url?> GetByIdAsync(Guid id);
    Task<List<Url>> GetAllAsync();
    Task<bool> ExistsAsync(string originalUrl);
    Task AddAsync(Url url);
    Task DeleteAsync(Url url);
    Task SaveChangesAsync();
}