using UrlShortenerApp.Models.Auth;

namespace UrlShortenerApp.Services.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(LoginModel model);
}
