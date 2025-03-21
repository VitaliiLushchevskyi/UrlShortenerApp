namespace UrlShortenerApp.Entities;

public class Url
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string OriginalUrl { get; set; }
    public string ShortenedUrl { get; set; }
    public string CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
