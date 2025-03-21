using UrlShortenerApp.Entities;

namespace UrlShortenerApp.Services.Interfaces;

public interface IUrlService
{
    Task<Url> ShortenUrlAsync(string originalUrl, string userId);
    Task<Url?> GetByShortenedUrlAsync(string shortUrl);
    Task<Url?> GetByIdAsync(Guid id);
    Task<List<Url>> GetAllUrlsAsync();
    Task<bool> DeleteUrlAsync(Guid id, string userId, bool isAdmin);
}
