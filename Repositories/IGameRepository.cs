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
        Task<IEnumerable<DbEvent>> GetGamesByUsername(string username);
        Task<IEnumerable<DbEvent>> GetGamesByUserId(string userid);
    }
}