using System.Collections.Generic;
using System.Threading.Tasks;
using colonist_extension.Repositories.Database;
using colonist_extension.Models.Database;

namespace colonist_extension.Repositories
{
    public class GameRepository : IGameRepository
    {
        private IDatabaseConnection _databaseConnection;
        public GameRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task CreateGame(int evtId)
        {
            await _databaseConnection.ExecuteAsync("INSERT INTO game (`EventId`) VALUES (@evtid)", new Dictionary<string, object>{
                 { "@evtid", evtId }
             });
        }

        public async Task AddUserToGame(int gameId, string userId)
        {
            await _databaseConnection.ExecuteAsync("INSERT INTO game_user (`GameId`, `UserId`) VALUES (@gameid, @userid)", new Dictionary<string, object>{
                 { "@gameid", gameId },
                 { "@userid", userId }
             });
        }

        public async Task<IEnumerable<DbEvent>> GetGamesByUsername(string username)
        {
            var result = await _databaseConnection.QueryAsync<DbEvent>("SELECT event.* FROM game_user INNER JOIN user ON user.id = game_user.userid INNER JOIN game ON game_user.gameid = game.id INNER JOIN event ON event.id = game.eventid WHERE user.Username = @username", new Dictionary<string, object>{
                 { "@username", username }
             });

            return result;
        }

        public async Task<IEnumerable<DbEvent>> GetGamesByUserId(string userid)
        {
            var result = await _databaseConnection.QueryAsync<DbEvent>("SELECT event.* FROM game_user INNER JOIN user ON user.id = game_user.userid INNER JOIN game ON game_user.gameid = game.id INNER JOIN event ON event.id = game.eventid WHERE user.id = @id", new Dictionary<string, object>{
                 { "@id", userid }
             });

            return result;
        }
    }
}