using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Api.Models;

namespace News.Api.Data.EntityMappings;

public class TagsMapping:IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags")
            .HasKey(tag => tag.id);
        
        builder.Property(m => m.name)
            .HasColumnType("text")
            .IsRequired();
    }
}