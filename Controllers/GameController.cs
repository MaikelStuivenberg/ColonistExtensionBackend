
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using colonist_extension.Repositories;
using colonist_extension.Models;
using Newtonsoft.Json.Linq;

namespace colonist_extension.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private IEventRepository _eventRepository;
        private IUserRepository _userRepository;
        private IGameRepository _gameRepository;

        public GameController(IEventRepository eventRepository, IUserRepository userRepository, IGameRepository gameRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGamesByUsername([FromQuery] string username)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if(user == null)
                return NotFound();

            var games = await _gameRepository.GetGamesByUsername(username);

            return Ok(games);
        }
    }
}
