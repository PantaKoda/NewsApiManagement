namespace News.Api.Models;

public class PostTag
{
    public int postId { get; set; }
    public Post post { get; set; }
    public int tagId { get; set; }
    public Tag tag { get; set; }
}