using Microsoft.EntityFrameworkCore;
using News.Api.Data.EntityMappings;
using News.Api.Models;
namespace News.Api.Context;

public class NewsDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    
    public NewsDbContext(DbContextOptions<NewsDbContext> options,IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public DbSet<Post> posts => Set<Post>();
    public DbSet<Website> websites => Set<Website>();
    public DbSet<Tag> tags => Set<Tag>();
    public DbSet<Category> categories => Set<Category>();
    public DbSet<PostTag> postsTags => Set<PostTag>();
    public DbSet<MainPageArticle> mainPageArticles => Set<MainPageArticle>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) // Only configure if it hasn't been done already
        {
            optionsBuilder.UseNpgsql(_configuration.GetValue<string>("ConnectionString:DB_URL"));
            base.OnConfiguring(optionsBuilder);
        }
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
