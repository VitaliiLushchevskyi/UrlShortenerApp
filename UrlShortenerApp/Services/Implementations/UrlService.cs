using UrlShortenerApp.Entities;
using UrlShortenerApp.Repositories.Interfaces;
using UrlShortenerApp.Services.Interfaces;

namespace UrlShortenerApp.Services.Implementations;

public class UrlService : IUrlService
{
    private readonly IUrlRepository _urlRepository;

    public UrlService(IUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    public async Task<Url> ShortenUrlAsync(string originalUrl, string userId)
    {
        if (await _urlRepository.ExistsAsync(originalUrl))
            throw new Exception("This URL has already been shortened.");

        string shortUrl = GenerateShortUrl();

        var url = new Url
        {
            OriginalUrl = originalUrl,
            ShortenedUrl = shortUrl,
            CreatedById = userId
        };

        await _urlRepository.AddAsync(url);
        await _urlRepository.SaveChangesAsync();

        return url;
    }

    public async Task<Url?> GetByShortenedUrlAsync(string shortUrl)
    {
        return await _urlRepository.GetByShortenedUrlAsync(shortUrl);
    }

    public async Task<Url?> GetByIdAsync(Guid id)
    {
        return await _urlRepository.GetByIdAsync(id);
    }

    public async Task<List<Url>> GetAllUrlsAsync()
    {
        return await _urlRepository.GetAllAsync();
    }

    public async Task<bool> DeleteUrlAsync(Guid id, string userId, bool isAdmin)
    {
        var url = await _urlRepository.GetByIdAsync(id);
        if (url == null) return false;

        if (!isAdmin && url.CreatedById != userId)
            throw new UnauthorizedAccessException("You are not allowed to delete this URL.");

        await _urlRepository.DeleteAsync(url);
        await _urlRepository.SaveChangesAsync();

        return true;
    }

    private static string GenerateShortUrl()
    {
        return Guid.NewGuid().ToString().Substring(0, 8);
    }
}