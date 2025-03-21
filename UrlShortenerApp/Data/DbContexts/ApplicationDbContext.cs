using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Data.Configurations;
using UrlShortenerApp.Entities;

namespace UrlShortenerApp.Data.DbContexts;

public class ApplicationDbContext : IdentityDbContext<User>
{
    private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Url> Urls { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UrlConfiguration());

    }
}