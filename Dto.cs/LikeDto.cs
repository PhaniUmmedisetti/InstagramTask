using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using instagram.Models;

namespace instagram.DTOs;

public record LikesDTO
{
    [JsonPropertyName("like_id")]
    public long LikeId { get; set; }

    [JsonPropertyName("post_id")]
    public string PostId { get; set; }


}

public record LikesCreateDTO
{
    [JsonPropertyName("like_id")]
    public long LikeId { get; set; }

    [JsonPropertyName("post_id")]
    public string PostId { get; set; }



}

