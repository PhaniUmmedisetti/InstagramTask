using instagram.DTOs;

namespace instagram.Models;



public record Posts
{

    public long PostId { get; set; }
    public string PostType { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public long UserId { get; set; }

    public PostsDTO asDto => new PostsDTO
    {
        PostId = PostId,
        PostType = PostType,

    };
}