using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Api.Entities;

namespace News.Api.Data.EntityMappings;

public class CategoryMapping :IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories")
            .HasKey("id");

        builder.Property(cat => cat.name)
            .HasColumnType("text")
            .IsRequired();
    }
}
    
    
    
