using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerApp.Models.Auth;
using UrlShortenerApp.Services.Interfaces;

namespace UrlShortenerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var token = await _authService.LoginAsync(model);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Invalid username or password.");
        }
        return Ok(new { Token = token });
    }
}