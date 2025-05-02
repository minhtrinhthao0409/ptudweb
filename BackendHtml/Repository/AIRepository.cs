using BackendHtml.Models;
using Dapper;
using WebApp.Models;

namespace BackendHtml.Models;

public class AIRepository : RepositoryBase
{
    public AIRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<AIContent> GetAIContentById(int id)
    {
        var sql = "SELECT * FROM AIcontent WHERE Id = @Id";
        var parameters = new { id };
        return await connection.QueryFirstOrDefaultAsync<AIContent>(sql, parameters);
    }

    public async Task<IEnumerable<AIContent>> GetAllAIContents()
    {
        var sql = "SELECT * FROM AIcontent";
        return await connection.QueryAsync<AIContent>(sql);
    }

    public async Task<AIContent> InsertAIContent(AIContent aiContent)
    {
        var sql = @"
        INSERT INTO AIContent (content, userid)
        VALUES (@content, @userid)
        RETURNING Id, Content, UserID;";

        var parameters = new { aiContent.Content, aiContent.UserID };
        var insertedContent = await connection.QuerySingleAsync<AIContent>(sql, parameters);
        return insertedContent;
    }

    public async Task<int> UpdateAIContent(AIContent aiContent)
    {
        var sql = "UPDATE AIcontent SET Content = @Content WHERE Id = @Id";
        var parameters = new { aiContent.Content, aiContent.Id };
        return await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> DeleteAIContent(int id)
    {
        var sql = "DELETE FROM AIcontent WHERE Id = @Id";
        var parameters = new { Id = id };
        return await connection.ExecuteAsync(sql, parameters);
    }

}