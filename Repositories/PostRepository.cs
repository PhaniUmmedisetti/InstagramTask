using instagram.Models;
using Dapper;
using instagram.Utilities;
using instagram.Repositories;


namespace instagram.Repositories;

public interface IPostsRepository
{
    Task<Posts> Create(Posts Item);
    //  Task <bool> Update(Posts Item);
    Task<bool> Delete(long PostId);
    Task<Posts> GetById(long PostId);
    Task<List<Posts>> GetList();

    Task<List<Posts>> GetListByUserId(long UserId);
    Task<List<Posts>> GetListByHashTagsId(long HashtagId);


}
public class PostsRepository : BaseRepository, IPostsRepository
{
    public PostsRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Posts> Create(Posts Item)
    {
        var query = $@"INSERT INTO ""{TableNames.post}"" 
        (post_id, post_type) 
        VALUES (@PostId, @PostType) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Posts>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long PostId)
    {
        var query = $@"DELETE FROM ""{TableNames.post}"" 
        WHERE post_id = @PostId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { PostId });
            return res > 0;
        }
    }


    public async Task<Posts> GetById(long PostId)
    {
        var query = $@"SELECT * FROM ""{TableNames.post}"" 
        WHERE post_id = @PostId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Posts>(query, new { PostId });
    }

    public async Task<List<Posts>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.post}""";

        List<Posts> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Posts>(query)).AsList();



        return res;
    }

    public async Task<List<Posts>> GetListByHashTagsId(long HashtagId)
    {
        var query = $@"SELECT * FROM  ""{TableNames.post_hashtag}"" ph LEFT JOIN ""{TableNames.post}"" p 
	   ON ph.post_id = p.post_id
	   WHERE hashtag_id = @HashtagId";

        using (var con = NewConnection)
        {
            var res = (await con.QueryAsync<Posts>(query, new { HashtagId })).AsList();
            return res;
        }
    }

    public async Task<List<Posts>> GetListByUserId(long PostId)
    {

        var query = $@"SELECT * FROM ""{TableNames.post}""
       
       WHERE post_id = @PostId";

        using (var con = NewConnection)
        {
            var res = (await con.QueryAsync<Posts>(query, new { PostId })).AsList();
            return res;
        }
    }

}