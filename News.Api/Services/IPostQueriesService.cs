using News.Api.Models;

namespace News.Api.Services;

public interface IPostQueriesService
{
    Task<IEnumerable<Post>> GetPostsByWebsiteNameAsync(string? websiteName);
    Task<IEnumerable<Post>> GetRecentPostsAsync();
    Task<IEnumerable<Post>> GetRecentPostsAsync(string? websiteName);
}