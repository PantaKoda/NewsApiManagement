using Microsoft.EntityFrameworkCore;
using News.Api.Context;
using News.Api.Data.Dtos;
using News.Api.Entities;

namespace News.Api.Services;

public class PostQueriesService :IPostQueriesService
{
    private readonly NewsDbContext _newsDbContext;

    public PostQueriesService(NewsDbContext newsDbContext)
    {
        _newsDbContext = newsDbContext ?? throw new ArgumentNullException(nameof(newsDbContext));
    }

    public async Task<IEnumerable<BasicQueryDto>> GetPostsByWebsiteNameAsync(string websiteName)
    {

       var posts = await _newsDbContext.posts
           .Include( p => p.category)
           .Include(p => p.website)
           .Where(p => p.website.name.Contains(websiteName))
           .OrderByDescending(p=>p.publishDateUtc)
           .ToListAsync();
       
       return posts.Select(post => new BasicQueryDto
       {
           title = post.title,
           publishDateUtc = post.publishDateUtc,
           imgUrl = post.imgUrl,
           websiteName = post.website.name,
           categoryName = post.category.name,
           postUrl = post.postUrl
       });

    }
}