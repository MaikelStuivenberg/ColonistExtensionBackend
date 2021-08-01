using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using colonist_extension.Models;
using colonist_extension.Models.Database;

namespace colonist_extension.Repositories
{
    public interface IGameRepository
    {
        Task CreateGame(int evtId);
        Task AddUserToGame(int gameId, string userId);
        Task<IEnumerable<Game>> GetGamesByUsername(string username);
        Task<IEnumerable<Game>> GetGamesByUserId(string userid);
        Task<IEnumerable<Game>> GetLastGames(int rows);
    }
}