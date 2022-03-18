using instagram.DTOs;
using instagram.Models;
using instagram.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace instagram.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly ILogger<PostsController> _logger;
    private readonly IPostsRepository _post;

    private readonly IHashTagsRepository _hashtag;

    private readonly IUsersRepository _user;

    private readonly ILikesRepository _like;


    public PostsController(ILogger<PostsController> logger,
    IPostsRepository post, IHashTagsRepository hashtag, IUsersRepository user, ILikesRepository like)
    {
        _logger = logger;
        _post = post;
        _hashtag = hashtag;
        _user = user;
        _like = like;
    }

    [HttpGet]
    public async Task<ActionResult<List<PostsDTO>>> GetList()
    {
        var postsList = await _post.GetList();

        var dtoList = postsList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{post_id}")]
    public async Task<ActionResult<PostsDTO>> GetById([FromRoute] long post_id)
    {
        var posts = await _post.GetById(post_id);

        if (posts is null)
            return NotFound("No post found with given post id");

        var dto = posts.asDto;

        dto.HashTags = (await _hashtag.GetListByPostId(post_id)).Select(x => x.asDto).ToList();
        dto.Users = (await _user.GetListByPostId(post_id)).Select(x => x.asDto).ToList();
        dto.Likes = (await _like.GetListByPostId(post_id)).Select(x => x.asDto).ToList();

        return Ok(dto);


    }

    [HttpPost]
    public async Task<ActionResult<PostsDTO>> CreatePosts([FromBody] PostsCreateDTO Data)
    {

        var toCreatePosts = new Posts
        {
            PostType = Data.PostType.Trim(),

        };

        var createdPosts = await _post.Create(toCreatePosts);

        return StatusCode(StatusCodes.Status201Created, createdPosts.asDto);
    }




    [HttpDelete("{post_id}")]
    public async Task<ActionResult> DeletePosts([FromRoute] long post_id)
    {
        var existing = await _post.GetById(post_id);
        if (existing is null)
            return NotFound("No post found with given post number");

        var didDelete = await _post.Delete(post_id);

        return NoContent();
    }
}