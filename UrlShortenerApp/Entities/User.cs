using Microsoft.AspNetCore.Identity;

namespace UrlShortenerApp.Entities;

public class User : IdentityUser
{
    public ICollection<Url> Urls { get; set; } = [];
}
