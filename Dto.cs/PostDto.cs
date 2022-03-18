using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using instagram.Models;
using instagram.DTOs;

namespace instagram.DTOs;

public record PostsDTO
{
    [JsonPropertyName("post_id")]
    public long PostId { get; set; }

    [JsonPropertyName("post_type")]
    public string PostType { get; set; }


    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }


    [JsonPropertyName("user_id")]
    public long UsertId { get; set; }

    [JsonPropertyName("hashtags")]
    public List<HashTagsDTO> HashTags { get; set; }

    [JsonPropertyName("users")]
    public List<UsersDTO> Users { get; set; }

    [JsonPropertyName("likes")]

    public List<LikesDTO> Likes { get; set; }



}

public record PostsCreateDTO
{
    [JsonPropertyName("post_id")]
    public long PostId { get; set; }

    [JsonPropertyName("post_type")]
    public string PostType { get; set; }



}

public record PostsUpdateDTO
{
    [JsonPropertyName("post_id")]
    public long PostId { get; set; }

    [JsonPropertyName("post_type")]
    public string PostType { get; set; }

}