namespace News.Api.Data.Dtos;

public class BasicQueryDto
{

    public string? title { get; set; }
    public DateTime publishDateUtc { get; set; }
    public string? imgUrl  { get; set; } 
    public string? postUrl  { get; set; } 
    public string websiteName { get; set; } 
    public string categoryName { get; set; }
}