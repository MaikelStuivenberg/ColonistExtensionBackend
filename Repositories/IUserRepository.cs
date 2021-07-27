using System.Threading.Tasks;
using colonist_extension.Models.Database;

namespace colonist_extension.Repositories
{
    public interface IUserRepository
    {
        Task<DbUser> GetUserById(string id);
        Task CreateUser(string id, string username, bool isLoggedIn);
        Task<DbUser> GetUserByUsername(string username);
    }
}