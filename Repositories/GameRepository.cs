using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using colonist_extension.Repositories.Database;
using colonist_extension.Models;
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

        public async Task<IEnumerable<Game>> GetGamesByUsername(string username)
        {
            // Get Games
            var result = await _databaseConnection.QueryAsync<DbEvent>("SELECT event.* FROM game_user INNER JOIN user ON user.id = game_user.userid INNER JOIN game ON game_user.gameid = game.id INNER JOIN event ON event.id = game.eventid WHERE user.Username = @username", new Dictionary<string, object>{
                 { "@username", username }
             });

            return await TransformToGame(result);
        }

        public async Task<IEnumerable<Game>> GetGamesByUserId(string userid)
        {
            var result = await _databaseConnection.QueryAsync<DbEvent>("SELECT event.* FROM game_user INNER JOIN user ON user.id = game_user.userid INNER JOIN game ON game_user.gameid = game.id INNER JOIN event ON event.id = game.eventid WHERE user.id = @id", new Dictionary<string, object>{
                 { "@id", userid }
             });

            return await TransformToGame(result);
        }

        public async Task<IEnumerable<Game>> GetLastGames(int rows)
        {
            var result = await _databaseConnection.QueryAsync<DbEvent>("SELECT event.* FROM event ORDER BY CreatedAt DESC LIMIT @limit", new Dictionary<string, object>{
                 { "@limit", rows }
             });

            return await TransformToGame(result);
        }

        
        /**
        * TransformToGame
        * Transform a dbEvent to a Game object (also search for extra information in the Db like user and the winner)
        */
        public async Task<IEnumerable<Game>> TransformToGame(IEnumerable<DbEvent> result)
        {
            var players = await _databaseConnection.QueryAsync<DbGameUser>("SELECT user.id, user.username, game_user.winner, game_user.gameid FROM game_user INNER JOIN user ON user.id = game_user.userid WHERE game_user.gameid IN @gameid", new Dictionary<string, object>{
                 { "@gameid", result.Select(el => el.Id) }
             });

            return result.Select(el => new Game
            {
                Id = el.Id,
                JSON = el.JSON,
                CreatedAt = el.CreatedAt,
                Players = players
                    .Where(pl => pl.GameId == el.Id)
                    .Select(pl => new GamePlayer
                    {
                        Id = pl.Id,
                        Username = pl.Username,
                        Winner = pl.Winner
                    })
            });
        }
    }
}