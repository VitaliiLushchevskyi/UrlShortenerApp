using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Entities;

namespace UrlShortenerApp.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Urls)
               .WithOne(url => url.CreatedBy)
               .HasForeignKey(url => url.CreatedById);
    }
}
