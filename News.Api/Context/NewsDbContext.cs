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
            /*optionsBuilder.UseNpgsql(
                "Host=188.245.61.197;Port=5432;Database=news-v2;Username=Themis-DB;Password=M8EDAZXpojolxoG5W1sQpar5CASa8RTmPkD0FnGu9VZAIahNmtGS0GSm6wTcfnZs;");*/
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
