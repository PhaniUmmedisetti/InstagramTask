using instagram.DTOs;

namespace instagram.Models;


public record HashTags
{

    public long HashtagId { get; set; }
    public string HashtagName { get; set; }


    public HashTagsDTO asDto => new HashTagsDTO
    {
        HashtagId = HashtagId,
        HashtagName = HashtagName,

    };
}