using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Api.Entities;

namespace News.Api.Data.EntityMappings;

public class MainPageArticleMapping :IEntityTypeConfiguration<MainPageArticle>
{
    public void Configure(EntityTypeBuilder<MainPageArticle> builder)
    {
        builder.ToTable("mainPageArticles")
            .HasKey(m => m.id);

        builder.Property(m => m.createdAt)
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.HasOne(mainpage => mainpage.website)
            .WithMany(website => website.mainPageArticles)
            .HasForeignKey(maipage => maipage.websiteId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mainpage => mainpage.post)
            .WithOne()
            .HasForeignKey<MainPageArticle>(mainpage => mainpage.postId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

    }
}