using System.Runtime.CompilerServices;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Api.Authentication;
using News.Api.Context;
using News.Api.Data.Dtos;
using News.Api.Entities;
using News.Api.Services;

namespace News.Api.Controllers;


[ApiController]
[Route("api/v{version:apiVersion}/posts")]
public class PopulateDBController : ControllerBase
{
    private readonly IPostWriteService _postWriteService;
    private readonly IPostQueriesService _postQueriesService;
   

    public PopulateDBController(IPostWriteService postWriteService, IPostQueriesService postQueriesService)
    {
        _postWriteService = postWriteService;
        _postQueriesService = postQueriesService;
    }

    [HttpPost]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    [ApiVersion(1)]
    private async Task<ActionResult<PostSavedResponse>> RecieveData([FromBody] IncomingDataDto incomingDataDto)
    {
       
       
        if (incomingDataDto == null || string.IsNullOrEmpty(incomingDataDto.Url))
        {
            return BadRequest("Incoming data is missing.");
        }

        try
        {
            var result = await _postWriteService.CreatePostAsync(incomingDataDto);
            
            return CreatedAtAction(nameof(RecieveData), new { id = result.id }, new PostSavedResponse{title = result.title,website = result.website});
        }
        catch (Exception ex)
        {
       
            Console.WriteLine(ex.Message);
            
            return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
        }
    }


    [HttpGet]
    [ApiVersion(1)]

    public async Task<ActionResult<IEnumerable<BasicQueryDto>>> GetPostsByWebsiteName(string websiteName)
    {
        var posts = await _postQueriesService.GetPostsByWebsiteNameAsync(websiteName);
        var results = new List<BasicQueryDto>();

        foreach (var post in posts)
        {
            results.Add(new BasicQueryDto
            {
                title = post.title,
                publishDateUtc = post.publishDateUtc,
                imgUrl = post.imgUrl,
                postUrl = post.postUrl,
                websiteName = post.websiteName,
                categoryName = post.categoryName
            });
        }

        return Ok(results);
    }
    
}

