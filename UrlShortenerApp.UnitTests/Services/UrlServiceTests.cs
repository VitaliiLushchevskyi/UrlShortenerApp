using Moq;
using UrlShortenerApp.Entities;
using UrlShortenerApp.Repositories.Interfaces;
using UrlShortenerApp.Services.Implementations;

namespace UrlShortenerApp.UnitTests.Services;
public class UrlServiceTests
{
    private readonly Mock<IUrlRepository> _urlRepositoryMock;
    private readonly UrlService _urlService;

    public UrlServiceTests()
    {
        _urlRepositoryMock = new Mock<IUrlRepository>();
        _urlService = new UrlService(_urlRepositoryMock.Object);
    }
    

    [Fact]
    public async Task ShortenUrlAsync_ShouldReturnShortenedUrl_WhenValid()
    {
        // Arrange
        string originalUrl = "https://example.com";
        string userId = "6e7a384e-f0eb-4a51-95a9-c5d2710dbe28";
        _urlRepositoryMock.Setup(repo => repo.ExistsAsync(originalUrl)).ReturnsAsync(false);
        _urlRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Url>())).Returns(Task.CompletedTask);
        _urlRepositoryMock.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _urlService.ShortenUrlAsync(originalUrl, userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(originalUrl, result.OriginalUrl);
        Assert.Equal(userId, result.CreatedById);
        Assert.NotEmpty(result.ShortenedUrl);
    }

    [Fact]
    public async Task GetByShortenedUrlAsync_ShouldReturnUrl_WhenUrlExists()
    {
        // Arrange
        string shortUrl = "abc123";
        var url = new Url { ShortenedUrl = shortUrl, OriginalUrl = "https://example.com" };
        _urlRepositoryMock.Setup(repo => repo.GetByShortenedUrlAsync(shortUrl)).ReturnsAsync(url);

        // Act
        var result = await _urlService.GetByShortenedUrlAsync(shortUrl);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(shortUrl, result.ShortenedUrl);
    }

    [Fact]
    public async Task DeleteUrlAsync_ShouldReturnFalse_WhenUrlNotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _urlRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Url)null);

        // Act
        var result = await _urlService.DeleteUrlAsync(id, "bd559f74-7d6a-4fad-923d-3bd9e8448b3e", false);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteUrlAsync_ShouldThrowException_WhenUserIsNotOwner()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var url = new Url { Id = id, CreatedById = "f297206c-f9b1-454d-95be-2e76c9ebb33d" };
        _urlRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(url);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _urlService.DeleteUrlAsync(id, "bd559f74-7d6a-4fad-923d-3bd9e8448b3e", false));
    }

}
