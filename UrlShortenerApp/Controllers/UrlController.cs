using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrlShortenerApp.Models.Url;
using UrlShortenerApp.Services.Interfaces;

namespace UrlShortenerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;

    public UrlController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    [Authorize]
    [HttpPost("shorten")]
    public async Task<IActionResult> ShortenUrl([FromBody] UrlRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        try
        {
            var url = await _urlService.ShortenUrlAsync(request.LongUrl, userId);
            return Ok(url);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{shortUrl}")]
    public async Task<IActionResult> GetOriginalUrl(string shortUrl)
    {
        var url = await _urlService.GetByShortenedUrlAsync(shortUrl);
        if (url == null) return NotFound();

        return Ok(url);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUrls()
    {
        var urls = await _urlService.GetAllUrlsAsync();
        return Ok(urls);
    }

    [HttpGet("info/{id}")]
    public async Task<IActionResult> GetUrlById(Guid id)
    {
        var url = await _urlService.GetByIdAsync(id);
        if (url == null) return NotFound();

        return Ok(url);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUrl(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var isAdmin = User.IsInRole("Admin");
        var deleted = await _urlService.DeleteUrlAsync(id, userId, isAdmin);

        if (!deleted) return NotFound();
        return NoContent();
    }
}
