using instagram.DTOs;

namespace instagram.Models;


public record Users
{

    public long UserId { get; set; }
    public string Name { get; set; }

    public DateTimeOffset DateOfBirth { get; set; }
    public long Mobile { get; set; }
    public DateTimeOffset CreatedAt { get; set; }


    public UsersDTO asDto => new UsersDTO
    {
        UserId = UserId,
        Name = Name,
        Mobile = Mobile,

    };
}