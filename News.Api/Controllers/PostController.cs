using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using News.Api.Authentication;
using News.Api.Context;
using News.Api.Data.Dtos;
using News.Api.Mappings;
using News.Api.Models;
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
    public async Task<ActionResult<PostSavedResponse>> RecieveData([FromBody] IncomingDataDto incomingDataDto)
    {
        if (incomingDataDto == null || string.IsNullOrEmpty(incomingDataDto.Url))
        {
            return BadRequest("Incoming data is missing.");
        }

        try
        {
            var result = await _postWriteService.CreatePostAsync(incomingDataDto);

            return CreatedAtAction(nameof(RecieveData), new { id = result.id },
                new PostSavedResponse { title = result.title, website = result.website });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
        }
    }


    /// <summary>
    /// Get a list of the 20 most recent posts for all filter by website anme.
    /// </summary>
    /// <param name="websiteName">Name of website in lowercase</param>
    /// <returns>A list of 20 most recent posts</returns>
    [HttpGet]
    [ApiVersion(1)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<BasicQueryDto>>> GetRecentPostsName(string? websiteName)
    {
      var posts = await _postQueriesService.GetRecentPostsAsync(websiteName);
      
      if (string.IsNullOrEmpty(websiteName))
      {
          return Ok(posts.ToBasicDtoList());
      }

        return Ok(posts.ToBasicWithWebsiteDtoList());
    }
}