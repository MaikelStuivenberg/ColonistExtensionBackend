
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using colonist_extension.Repositories;
using colonist_extension.Models;
using Newtonsoft.Json.Linq;

namespace colonist_extension.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private IEventRepository _eventRepository;
        private IUserRepository _userRepository;
        private IGameRepository _gameRepository;

        public EventsController(IEventRepository eventRepository, IUserRepository userRepository, IGameRepository gameRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
        }

        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] JObject evt)
        {
            if (((int)evt.GetValue("id")) != 60)
                return BadRequest("Only 'End Game Event' (60) is currently supported"); //400

            var endGameEvent = evt.ToObject<EndGameEvent>();

            // Save the JSON of this event
            var evtId = await _eventRepository.PostEvent(endGameEvent);
            
            // Create Game
            await _gameRepository.CreateGame(evtId);

            // Check if user is existing or create User
            endGameEvent.Data.Players.ForEach(async user =>
            {
                var usrObj = await _userRepository.GetUserById(user.Player.Id);

                if (usrObj == null)
                {
                    await _userRepository.CreateUser(user.Player.Id, user.Player.UserState.Username, user.Player.UserState.IsLoggedIn);
                    usrObj = await _userRepository.GetUserById(user.Player.Id);
                }

                await _gameRepository.AddUserToGame(evtId, usrObj.Id);
            });


            return Ok();
        }
    }
}
