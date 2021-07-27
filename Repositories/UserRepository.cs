using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using colonist_extension.Repositories.Database;
using colonist_extension.Models.Database;

namespace colonist_extension.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IDatabaseConnection _databaseConnection;
        public UserRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task CreateUser(string id, string username, bool isLoggedIn)
        {
            await _databaseConnection.ExecuteAsync("INSERT INTO user (`Id`, `Username`, `IsLoggedIn`) VALUES (@id, @username, @isloggedin)", new Dictionary<string, object>{
                 { "@id", id },
                 { "@username", username },
                 { "@isloggedin", isLoggedIn }
             });
        }

        public async Task<DbUser> GetUserById(string id)
        {
            var result = await _databaseConnection.QueryAsync<DbUser>("SELECT * FROM user WHERE Id = @id", new Dictionary<string, object>{
                 { "@id", id }
             });

            return result.FirstOrDefault();
        }

        public async Task<DbUser> GetUserByUsername(string username)
        {
            var result = await _databaseConnection.QueryAsync<DbUser>("SELECT * FROM user WHERE Username = @username", new Dictionary<string, object>{
                 { "@username", username }
             });

            return result.FirstOrDefault();
        }

        
    }
}