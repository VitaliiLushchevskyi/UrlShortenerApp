using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using UrlShortenerApp.Controllers;
using UrlShortenerApp.Entities;
using UrlShortenerApp.Models.Url;
using UrlShortenerApp.Services.Interfaces;

namespace UrlShortenerApp.UnitTests.Controllers;
public class UrlControllerTests
{
    private readonly Mock<IUrlService> _urlServiceMock;
    private readonly UrlController _controller;

    public UrlControllerTests()
    {
        _urlServiceMock = new Mock<IUrlService>();
        _controller = new UrlController(_urlServiceMock.Object);
    }

    [Fact]
    public async Task ShortenUrl_ShouldReturnOk_WhenValidRequest()
    {
        // Arrange
        SetUser("bd559f74-7d6a-4fad-923d-3bd9e8448b3e");
        var request = new UrlRequest { LongUrl = "https://example.com" };
        var url = new Url { OriginalUrl = request.LongUrl, ShortenedUrl = "short123" };

        _urlServiceMock.Setup(service => service.ShortenUrlAsync(request.LongUrl, "bd559f74-7d6a-4fad-923d-3bd9e8448b3e")).ReturnsAsync(url);

        // Act
        var result = await _controller.ShortenUrl(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUrl = Assert.IsType<Url>(okResult.Value);
        Assert.Equal("short123", returnedUrl.ShortenedUrl);
    }

    [Fact]
    public async Task ShortenUrl_ShouldReturnUnauthorized_WhenUserIsNotLoggedIn()
    {
        // Arrange
        _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        var request = new UrlRequest { LongUrl = "https://example.com" };

        // Act
        var result = await _controller.ShortenUrl(request);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task GetOriginalUrl_ShouldReturnOk_WhenUrlExists()
    {
        // Arrange
        string shortUrl = "abc123";
        var url = new Url { ShortenedUrl = shortUrl, OriginalUrl = "https://example.com" };
        _urlServiceMock.Setup(service => service.GetByShortenedUrlAsync(shortUrl)).ReturnsAsync(url);

        // Act
        var result = await _controller.GetOriginalUrl(shortUrl);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUrl = Assert.IsType<Url>(okResult.Value);
        Assert.Equal(shortUrl, returnedUrl.ShortenedUrl);
    }

    [Fact]
    public async Task GetOriginalUrl_ShouldReturnNotFound_WhenUrlDoesNotExist()
    {
        // Arrange
        string shortUrl = "invalid123";
        _urlServiceMock.Setup(service => service.GetByShortenedUrlAsync(shortUrl)).ReturnsAsync((Url)null);

        // Act
        var result = await _controller.GetOriginalUrl(shortUrl);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteUrl_ShouldReturnNoContent_WhenAdminDeletes()
    {
        // Arrange
        SetUser("f297206c-f9b1-454d-95be-2e76c9ebb33d", true);
        Guid urlId = Guid.NewGuid();
        _urlServiceMock.Setup(service => service.DeleteUrlAsync(urlId, "f297206c-f9b1-454d-95be-2e76c9ebb33d", true)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteUrl(urlId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteUrl_ShouldReturnUnauthorized_WhenUserIsNotLoggedIn()
    {
        // Arrange
        _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        Guid urlId = Guid.NewGuid();

        // Act
        var result = await _controller.DeleteUrl(urlId);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    private void SetUser(string userId, bool isAdmin = false)
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }
}