using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using colonist_extension.Repositories.Database;
using colonist_extension.Models;
using Newtonsoft.Json;

namespace colonist_extension.Repositories
{
    public class EventRepository : IEventRepository
    {
        private IDatabaseConnection _databaseConnection;
        public EventRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<int> PostEvent(BaseEvent evt)
        {
            var result = await _databaseConnection.QueryAsync<int>("INSERT INTO event (`JSON`) VALUES (@json); SELECT LAST_INSERT_ID();", new Dictionary<string, object>{
                 { "json", JsonConvert.SerializeObject(evt)}
             });
             
             return result.First();
        }
    }
}