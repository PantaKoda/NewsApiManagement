using System.Text.Json.Serialization;

namespace News.Api.Data.Dtos;

public class IncomingDataDto
{
    [JsonPropertyName("URL")]
    public string Url { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    [JsonPropertyName("ImageURL")]
    public string ImageUrl { get; set; }
    public string PublishDateRaw { get; set; }
    public DateTime PublishDate { get; set; }
    public string Category { get; set; }
    public List<string> Tags { get; set; }
    public string Zone { get; set; }
    public string Offset { get; set; }
    public bool IsMain { get; set; }
}


