using News.Api.Data.Dtos;

namespace News.Api.Services;

public interface IPostWriteService
{
    Task<PostSavedResponse> CreatePostAsync(IncomingDataDto incomingDataDto);
}