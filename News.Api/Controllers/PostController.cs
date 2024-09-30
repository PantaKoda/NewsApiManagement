using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Api.Authentication;
using News.Api.Context;
using News.Api.Data.Dtos;
using News.Api.Entities;
using News.Api.Services;

namespace News.Api.Controllers;


[ApiController]
[Route("api/v1/posts")]
public class PopulateDBController : ControllerBase
{
    private readonly IPostWriteService _postWriteService;
   
    public PopulateDBController(IPostWriteService postWriteService) 
    {
        _postWriteService = postWriteService; 
    }

    [HttpPost]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<ActionResult<PostSavedResponse>> RecieveData([FromBody] IncomingDataDto incomingDataDto)
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
}

