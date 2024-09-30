using System;
using System.Collections.Generic;

namespace News.Api.Entities;

public class Website
{
    public int id { get; set; }

    public string? name { get; set; }
    //navigations
    public List<Post> posts { get; set; }
    public List<Category> categories { get; set; }
    
    public List<Tag> tags { get; set; }
    
    public List<MainPageArticle> mainPageArticles { get; set; }
    
}
