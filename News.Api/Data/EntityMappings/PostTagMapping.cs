using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Api.Entities;

namespace News.Api.Data.EntityMappings;

public class PostTagMapping :IEntityTypeConfiguration<PostTag>
{
    public void Configure(EntityTypeBuilder<PostTag> builder)
    {
        builder.ToTable("poststags")
            .HasKey(pt => new { pt.postId, pt.tagId });

        builder.HasOne(pt => pt.post)
            .WithMany(post => post.postTags)
            .HasForeignKey(pt => pt.postId);
        
        builder.HasOne(pt => pt.tag)
            .WithMany(tag => tag.postTags)  // Reference to the collection of PostTags
            .HasForeignKey(pt => pt.tagId);
    }
}