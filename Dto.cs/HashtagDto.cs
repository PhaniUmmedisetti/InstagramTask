using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using instagram.Models;

namespace instagram.DTOs;

public record HashTagsDTO
{
    [JsonPropertyName("hashtag_id")]
    public long HashtagId { get; set; }

    [JsonPropertyName("hashtag_name")]
    public string HashtagName { get; set; }


    [JsonPropertyName("posts")]
    public List<PostsDTO> Posts { get; set; }


}

public record HashTagsCreateDTO
{
    [JsonPropertyName("hashtag_id")]
    public long HashtagId { get; set; }

    [JsonPropertyName("hashtag_name")]
    public string HashtagName { get; set; }


}

