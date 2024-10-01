using System;
using System.Collections.Generic;

namespace News.Api.Models;

public  class Tag
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    //navigations
    public Website website { get; set; }
    public int websiteId { get; set; }
    
    public ICollection<PostTag> postTags { get; set; }
    
    
    
}
