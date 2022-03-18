
using instagram.DTOs;
using instagram.Models;
using instagram.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace instagram.Controllers;

[ApiController]
[Route("api/hash_tags")]
public class HashTagsController : ControllerBase
{
    private readonly ILogger<HashTagsController> _logger;
    private readonly IHashTagsRepository _hashtag;
    private readonly IPostsRepository _posts;

    public HashTagsController(ILogger<HashTagsController> logger,
    IHashTagsRepository hashtag, IPostsRepository posts)
    {
        _logger = logger;
        _hashtag = hashtag;

        _posts = posts;
    }




    [HttpGet]
    public async Task<ActionResult<List<HashTagsDTO>>> GetList()
    {
        var usersList = await _hashtag.GetList();


        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{hashtag_id}")]
    public async Task<ActionResult<HashTagsDTO>> GetById([FromRoute] long hashtag_id)
    {
        var hashtag = await _hashtag.GetById(hashtag_id);

        if (hashtag is null)
            return NotFound("No hashtag found with given hash id");

        var dto = hashtag.asDto;

        dto.Posts = (await _posts.GetListByHashTagsId(hashtag_id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<HashTagsDTO>> CreateHashTags([FromBody] HashTagsCreateDTO Data)
    {


        var toCreateHashTags = new HashTags
        {
            HashtagName = Data.HashtagName.Trim(),

        };

        var createdHashTags = await _hashtag.Create(toCreateHashTags);

        return StatusCode(StatusCodes.Status201Created, createdHashTags.asDto);
    }



    [HttpDelete("{hashtag_id}")]
    public async Task<ActionResult> DeleteHashTags([FromRoute] long hashtag_id)
    {
        var existing = await _hashtag.GetById(hashtag_id);
        if (existing is null)
            return NotFound("No hashtag found with given hash id");

        var didDelete = await _hashtag.Delete(hashtag_id);

        return NoContent();
    }
}