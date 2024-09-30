using System;
using System.Collections.Generic;

namespace News.Api.Entities;

public  class MainPageArticle
{
public int id { get; set; }
public DateTime createdAt { get; set; } = DateTime.UtcNow;


//navigations
public Website website { get; set; }
public int websiteId { get; set; }

public Post post { get; set; }
public int postId { get; set; }


}
