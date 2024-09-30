using News.Api.Data.Dtos;

namespace News.Api.Services;

public interface IPostQueriesService
{
    Task<IEnumerable<BasicQueryDto>> GetPostsByWebsiteNameAsync(string websiteName);
}