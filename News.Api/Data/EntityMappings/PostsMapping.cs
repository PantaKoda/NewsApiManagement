using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Api.Entities;

namespace News.Api.Data.EntityMappings;

public class PostsMapping: IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts")
            .HasKey(p => p.id);

        builder.Property(post => post.title)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(post => post.id)
            .HasColumnName("id");

        builder.Property(post => post.postUrl)
            .HasColumnType("text")
            .IsRequired();
        builder.Property(post => post.description)
            .HasColumnType("text")
            .IsRequired();
        builder.Property(post => post.timezone)
            .HasColumnType("text")
            .IsRequired();
        builder.Property(post => post.imgUrl)
            .HasColumnType("text")
            .IsRequired();
        builder.Property(post => post.utcOffset)
            .HasColumnType("text")
            .IsRequired();
        builder.Property(post => post.publishDateRaw)
            .HasColumnType("text")
            .IsRequired();
        builder.Property(post => post.createdAt)
            .HasColumnType("timestamptz")
            .IsRequired();
        builder.Property(post => post.publishDateUtc)
            .HasColumnType("timestamptz")
            .IsRequired();
        
    }
}