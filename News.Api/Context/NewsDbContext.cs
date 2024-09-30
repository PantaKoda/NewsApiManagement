using Microsoft.EntityFrameworkCore;
using News.Api.Data.EntityMappings;
using News.Api.Entities;

namespace News.Api.Context;

public class NewsDbContext : DbContext
{
    private IConfiguration _configuration;

    protected NewsDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public NewsDbContext()
    {
    }

    public NewsDbContext(DbContextOptions<NewsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post> posts => Set<Post>();
    public DbSet<Website> websites => Set<Website>();
    public DbSet<Tag> tags => Set<Tag>();
    public DbSet<Category> categories => Set<Category>();
    public DbSet<PostTag> postsTags => Set<PostTag>();
    public DbSet<MainPageArticle> mainPageArticles => Set<MainPageArticle>();
    



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            _configuration.GetValue<string>("ConnectionString:DB_URL"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasIndex(post => new { post.websiteId, post.publishDateUtc })
            .IsDescending();
        modelBuilder.ApplyConfiguration(new PostsMapping());
        modelBuilder.ApplyConfiguration(new PostTagMapping());
        modelBuilder.ApplyConfiguration(new CategoryMapping());
        modelBuilder.ApplyConfiguration(new MainPageArticleMapping());
        modelBuilder.ApplyConfiguration(new WebsiteMapping());
        modelBuilder.ApplyConfiguration(new TagsMapping());
    }
    
    
}
