using instagram.DTOs;
using instagram.Models;
using instagram.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace instagram.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUsersRepository _user;
    private readonly IPostsRepository _post;

    public UsersController(ILogger<UsersController> logger,
    IUsersRepository user, IPostsRepository post)
    {
        _logger = logger;
        _user = user;
        _post = post;
    }

    [HttpGet]
    public async Task<ActionResult<List<UsersDTO>>> GetList()
    {
        var usersList = await _user.GetList();


        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{user_id}")]
    public async Task<ActionResult<UsersDTO>> GetById([FromRoute] long user_id)
    {
        var users = await _user.GetById(user_id);

        if (users is null)
            return NotFound("No user found with given user id");

        var dto = users.asDto;

        dto.Posts = (await _post.GetListByUserId(user_id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<UsersDTO>> CreateUsers([FromBody] UsersCreateDTO Data)
    {

        var toCreateUsers = new Users
        {
            Name = Data.Name.Trim(),
            Mobile = Data.Mobile,

        };

        var createdUsers = await _user.Create(toCreateUsers);

        return StatusCode(StatusCodes.Status201Created, createdUsers.asDto);
    }

    [HttpPut("{user_id}")]
    public async Task<ActionResult> UpdateUsers([FromRoute] long user_id,
    [FromBody] UsersUpdateDTO Data)
    {
        var existing = await _user.GetById(user_id);
        if (existing is null)
            return NotFound("No user found with given user id");

        var toUpdateUsers = existing with
        {
            Name = Data.Name?.Trim()?.ToLower() ?? existing.Name,
            Mobile = existing.Mobile,

        };

        var didUpdate = await _user.Update(toUpdateUsers);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

        return NoContent();
    }


}