using Microsoft.EntityFrameworkCore;
using News.Api.Context;
using News.Api.Models;


namespace News.Api.Services;

public class PostQueriesService :IPostQueriesService
{
    private readonly NewsDbContext _newsDbContext;

    public PostQueriesService(NewsDbContext newsDbContext)
    {
        _newsDbContext = newsDbContext ?? throw new ArgumentNullException(nameof(newsDbContext));
    }

    public async Task<IEnumerable<Post>> GetPostsByWebsiteNameAsync(string? websiteName)
    {

       var posts = await _newsDbContext.posts
           .Include( p => p.category)
           .Include(p => p.website)
           .Where(p => p.website.name.Contains(websiteName))
           .OrderByDescending(p=>p.publishDateUtc)
           .Take(20)
           .ToListAsync();
       return posts;

    }
    
    public async Task<IEnumerable<Post>> GetRecentPostsAsync() 
    {

        var posts = await _newsDbContext.posts
            .Include( p => p.category)
            .Include(p => p.website)
            .OrderByDescending(p=>p.publishDateUtc)
            .Take(20)
            .ToListAsync();
        return posts;

    }
    public async Task<IEnumerable<Post>> GetRecentPostsAsync(string? websiteName) 
    {
        if (string.IsNullOrEmpty(websiteName))
        {
            return await GetRecentPostsAsync();
        }
        websiteName = websiteName.Trim();
        var posts = await _newsDbContext.posts
            .Include( p => p.category)
            .Include(p => p.website)
            .Where(p => p.website.name.Contains(websiteName))
            .OrderByDescending(p=>p.publishDateUtc)
            .Take(20)
            .ToListAsync();
        return posts;

    }
    
    
    
}