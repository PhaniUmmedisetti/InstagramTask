using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using instagram.Models;

namespace instagram.DTOs;

public record UsersDTO
{
    [JsonPropertyName("User_id")]
    public long UserId { get; set; }

    [JsonPropertyName("User_name")]
    public string Name { get; set; }


    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("posts")]
    public List<PostsDTO> Posts { get; set; }



}

public record UsersCreateDTO
{


    [JsonPropertyName("User_name")]
    public string Name { get; set; }


    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

}

public record UsersUpdateDTO
{


    [JsonPropertyName("User_name")]
    public string Name { get; set; }


    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

}