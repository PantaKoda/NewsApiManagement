namespace News.Api.Models;



/// <summary>
/// This is the model tha represents a post in the database.
/// </summary>
public class Post
{
   /// <summary>
   /// Internal id assigned to post
   /// </summary>
    public int id { get; set; }
    public string? title { get; set; }
   /// <summary>
   /// The published date of the article in UTC
   /// </summary>
    public DateTime publishDateUtc { get; set; }
   
   /// <summary>
   /// The published date of the article as extrated from source.
   /// </summary>
   public string? publishDateRaw { get; set; }
    public string? description { get; set; } 
    public string? imgUrl  { get; set; } 
    public string? postUrl  { get; set; } 
   /// <summary>
   /// UTC time when the article was saved in the database.
   /// </summary>
    public DateTime createdAt { get; set; } =DateTime.UtcNow;
   
   /// <summary>
   /// Timezone of the origin city. e.g  EEST
   /// </summary>
   public string? timezone  { get; set; } 
   
   /// <summary>
   /// The UTC offset for the origin country/city, e.g +0300 from Europe/Athens.
   /// </summary>
    public string? utcOffset  { get; set; } 
    
    //navigations
    public Website website { get; set; } 
    public int websiteId { get; set; } 
    
    public int categoryId { get; set; }
    public Category category { get; set; }
    
    public ICollection<PostTag> postTags { get; set; }
}
