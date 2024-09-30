namespace News.Api.Entities;

public class Post
{
    public int id { get; set; }
    public string? title { get; set; }
    public DateTime publishDateUtc { get; set; }
    public string? publishDateRaw { get; set; }
    public string? description { get; set; } 
    public string? imgUrl  { get; set; } 
    public string? postUrl  { get; set; } 
    public DateTime createdAt { get; set; } =DateTime.UtcNow;
    public string? timezone  { get; set; } 
    public string? utcOffset  { get; set; } 
    
    //navigations
    public Website website { get; set; } 
    public int websiteId { get; set; } 
    
    public int categoryId { get; set; }
    public Category category { get; set; }
    
    public ICollection<PostTag> postTags { get; set; }
}
