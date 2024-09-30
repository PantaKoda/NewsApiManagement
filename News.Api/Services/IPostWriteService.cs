using News.Api.Data.Dtos;
using News.Api.Entities;

namespace News.Api.Services;

public interface IPostWriteService
{
    Task<PostSavedResponse> CreatePostAsync(IncomingDataDto incomingDataDto);
}