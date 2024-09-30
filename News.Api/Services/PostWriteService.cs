using Microsoft.EntityFrameworkCore;
using News.Api.Context;
using News.Api.Data.Dtos;
using News.Api.Entities;

namespace News.Api.Services;

public class PostWriteService : IPostWriteService
{
    private readonly NewsDbContext _newsDbContext;

    public PostWriteService(NewsDbContext newsDbContext)
    {
        _newsDbContext = newsDbContext;
    }

    public async Task<PostSavedResponse> CreatePostAsync(IncomingDataDto incomingDataDto)
    {
        using (var transaction = await _newsDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var website = await GetOrCreateWebsite(incomingDataDto.Url);
                var category = await GetOrCreateCategory(incomingDataDto.Category, website.id);

                var post = await CreatePost(incomingDataDto, website, category);

                if (incomingDataDto.IsMain)
                {
                    var mainPage = new MainPageArticle { postId = post.id, websiteId = website.id };
                    _newsDbContext.mainPageArticles.Add(mainPage);
                }

                await HandleTags(incomingDataDto, website.id, post.id);

                await _newsDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return new PostSavedResponse
                {
                    title = post.title,
                    id = post.id,
                    website = website.name
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }


    private async Task<Website> GetOrCreateWebsite(string websiteUrl)
    {

        System.Console.WriteLine(websiteUrl);
        var websiteName = new Uri(websiteUrl).Authority.Split(".")[1];
        
        var website = await _newsDbContext.websites.FirstOrDefaultAsync(w => w.name == websiteName);
        if (website == null)
        {
            website = new Website { name = websiteName };
            _newsDbContext.websites.Add(website);
            await _newsDbContext.SaveChangesAsync();
        }
        return website;
    }
    private async Task<Category> GetOrCreateCategory(string categoryName, int websiteId)
    {
        var category = await _newsDbContext.categories.FirstOrDefaultAsync(c => c.name == categoryName && c.websiteId == websiteId);
        if (category == null)
        {
            category = new Category { name = categoryName, websiteId = websiteId };
            _newsDbContext.categories.Add(category);
            await _newsDbContext.SaveChangesAsync();
        }
        return category;
    }

    private async Task HandleTags(IncomingDataDto incomingDataDto, int websiteId, int postId)
    {
        var existingTags = await _newsDbContext.tags
            .Where(t => incomingDataDto.Tags.Contains(t.name) && t.websiteId == websiteId)
            .ToListAsync();

        foreach (var tagName in incomingDataDto.Tags)
        {
            // Check if the tag already exists
            var savedTag = existingTags.FirstOrDefault(t => t.name == tagName);
            if (savedTag == null)
            {
                
                savedTag = new Tag { name = tagName, websiteId = websiteId };
                _newsDbContext.tags.Add(savedTag); 
                await _newsDbContext.SaveChangesAsync(); 
                existingTags.Add(savedTag); 
            }

            // Create the PostTag relationship
            _newsDbContext.postsTags.Add(new PostTag { postId = postId, tagId = savedTag.id });
        }
    }


    private async Task<Post> CreatePost(IncomingDataDto incomingDataDto, Website website, Category category)
    {
        var post = new Post
        {
            title = incomingDataDto.Title,
            publishDateUtc = incomingDataDto.PublishDate,
            publishDateRaw = incomingDataDto.PublishDateRaw,
            description = incomingDataDto.Description,
            imgUrl = incomingDataDto.ImageUrl,
            postUrl = incomingDataDto.Url,
            timezone = incomingDataDto.Zone,
            utcOffset = incomingDataDto.Offset,
            websiteId = website.id,
            categoryId = category.id
        };

        _newsDbContext.posts.Add(post);
        await _newsDbContext.SaveChangesAsync();
        return post;
    }

    

}