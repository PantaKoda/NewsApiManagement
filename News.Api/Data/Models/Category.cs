using System;
using System.Collections.Generic;

namespace News.Api.Entities;

public partial class Category
{
 public int id { get; set; }
 public string? name { get; set; }
 
 //navigations
 public Website website { get;  set;}
 public int websiteId { get;  set;}
 
 public List<Post> posts { get; set; }
}

