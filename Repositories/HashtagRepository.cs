using instagram.Models;
using Dapper;
using instagram.Utilities;
using instagram.Repositories;


namespace instagram.Repositories;

public interface IHashTagsRepository
{
    Task<HashTags> Create(HashTags Item);
    Task<bool> Delete(long HashtagId);
    Task<HashTags> GetById(long HashtagId);
    Task<List<HashTags>> GetList();

    Task<List<HashTags>> GetListByPostId(long PostId);

}
public class HashTagsRepository : BaseRepository, IHashTagsRepository
{

    public HashTagsRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<HashTags> Create(HashTags Item)
    {
        var query = $@"INSERT INTO ""{TableNames.hashtag}"" 
        (hashtag_id, hashtag_name) 
        VALUES (@HashtagId, @HashtagName) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<HashTags>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long HashtagId)
    {
        var query = $@"DELETE FROM ""{TableNames.hashtag}"" 
        WHERE hashtag_id = @HashtagId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { HashtagId });
            return res > 0;
        }
    }


    public async Task<HashTags> GetById(long HashtagId)
    {
        var query = $@"SELECT * FROM ""{TableNames.hashtag}"" 
        WHERE hashtag_id = @HashtagId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<HashTags>(query, new { HashtagId });
    }

    public async Task<List<HashTags>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.hashtag}""";

        List<HashTags> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<HashTags>(query)).AsList();



        return res;
    }

    public async Task<List<HashTags>> GetListByPostId(long PostId)
    {
        var query = $@"SELECT * FROM  ""{TableNames.post_hashtag}"" ph LEFT JOIN ""{TableNames.hashtag}"" h 
	   ON ph.hashtag_id = h.hashtag_id
	   WHERE post_id = @PostId";

        using (var con = NewConnection)
        {
            var res = (await con.QueryAsync<HashTags>(query, new { PostId })).AsList();
            return res;
        }
    }
}