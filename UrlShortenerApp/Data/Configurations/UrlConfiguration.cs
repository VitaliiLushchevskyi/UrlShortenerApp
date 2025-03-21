using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Entities;

namespace UrlShortenerApp.Data.Configurations;

public class UrlConfiguration : IEntityTypeConfiguration<Url>
{
    public void Configure(EntityTypeBuilder<Url> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.ShortenedUrl)
               .IsUnique();

        builder.HasOne(u => u.CreatedBy)
               .WithMany(user => user.Urls)
               .HasForeignKey(u => u.CreatedById);

        builder.Property(u => u.OriginalUrl)
               .IsRequired();

        builder.Property(u => u.ShortenedUrl)
               .IsRequired();
    }
}
