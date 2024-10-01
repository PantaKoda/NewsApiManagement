using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Api.Models;

namespace News.Api.Data.EntityMappings;

public class WebsiteMapping :IEntityTypeConfiguration<Website>
{
    public void Configure(EntityTypeBuilder<Website> builder)
    {
        builder.ToTable("websites")
            .HasKey(w => w.id);

        
        builder.Property(website => website.name)
            .HasColumnType("text")
            .IsRequired();

        builder.HasMany(website => website.categories)
            .WithOne(category => category.website)
            .HasForeignKey(category => category.websiteId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(website => website.posts)
            .WithOne(post => post.website)
            .HasForeignKey(post => post.websiteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(website => website.tags)
            .WithOne(tag => tag.website)
            .HasPrincipalKey(tag => tag.id)
            .HasForeignKey(tag => tag.websiteId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(website => website.mainPageArticles)
            .WithOne(mainpage => mainpage.website)
            .HasForeignKey(mainpage => mainpage.websiteId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}