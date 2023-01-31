using AutoMapper;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using DataAccessLibrary.Repositories.Interfaces;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiASPNET.Helpers;

namespace RestApiASPNET.Controllers
{
    [ApiController]
    [Route("api/Events/")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IMapper _mapper;
        private readonly EventRepositories _eventRepositories;
        private readonly IDbRepositories<Event> _dbRepositories;
        private readonly UserHelper _userHelper;

        public EventController(IDbRepositories<Event> dbRepositories, ILogger<EventController> logger, IMapper mapper, UserHelper userHelper,
            EventRepositories eventRepositories)
        {
            _dbRepositories = dbRepositories;
            _logger = logger;
            _mapper = mapper;
            _eventRepositories = eventRepositories;
            _userHelper = userHelper;
        }

        [HttpGet("Missions/{eventId:int}")]
        // [Authorize]
        
        public async Task<JsonResult> GetEvent(int eventId)
        {
            try
            {
                var @event = await _eventRepositories.GetEventMissions(eventId);
                var eventDtoAdmin = _mapper.Map<EventDtoAdmin>(@event);
                _logger.LogInformation("Event retrieved from database");
                return new JsonResult(Ok( eventDtoAdmin).Value);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet("Users/{eventId:int}")]
        [Authorize]

        public async Task<JsonResult> GetEventUsers(int eventId)
        {
            try
            {
                var @event = await _eventRepositories.GetEventUsers(eventId);
                var users = @event.EventUsers.Select(a =>
                {
                    a.User.CreateTime = a.CreateTime;
                    return _mapper.Map<UserDtoAdmin>(a.User);
                }).ToList();
                
                _logger.LogInformation($"User  for event:{eventId} retrieved from database");
                return new JsonResult(Ok(users).Value);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }




        [HttpGet("{eventId:int}/teams&users")]
        // [Authorize]
        public async Task<JsonResult> GetEventTeamsUsers(int eventId)
        {
            try
            {
               var events = await _eventRepositories.Get("EventTeams.EventUsers.User", "EventUsers.User")
                   .Where(arg => arg.EventId == eventId).GetAll();
               // var groupedTeams = eventTeams.GroupBy(s => s.EventId).;
               var usersWoTeams = (events.SelectMany(a => a.EventUsers)
                   .Where(eventUser => eventUser.EventTeam == null && eventUser.RowStatusId == (int)StatusEnums.Active)
                   .Select(a => a.User).ToList());
               var eventTeams = events.SelectMany(a => a.EventTeams);
               var teamsWithUsers = eventTeams.Where(a => a.RowStatusId == (int)StatusEnums.Active).Select(team =>
               {
                  var eventTeam = _mapper.Map<EventTeamDto>(team);
                  eventTeam.Users = team.EventUsers.Where(a => a.RowStatusId == (int)StatusEnums.Active).Select(a => _mapper.Map<UserDtoAdmin>(a.User)).ToList()! ;
                  return eventTeam;
               }).ToList();
               teamsWithUsers.Add(new EventTeamDto
               {
                   EventTeamId = 0,
                   EventId = 0,
                   TeamId = null,
                   EventTeamName = null,
                   EventTeamAvatar = null,
                   EventTeamCapitanId = null,
                   TeamTwitterPoint = false,
                   EventTeamPoint = 0,
                   Users = _mapper.Map<List<UserDtoAdmin>>(usersWoTeams)!
               });
               return new JsonResult(Ok(teamsWithUsers).Value);
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        } 
        

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetEvents()
        {
            try
            {
                var dbEvents = await _eventRepositories.Get("EventUsers.User").GetAll();
                var userId = _userHelper.GetAuthId()!;
                var eventDtoAdmins = dbEvents.Select( @event =>
                {
                    var eventDto = _mapper.Map<EventDtoAdmin>(@event);
                    eventDto.IsParticipant = @event.EventUsers.Any(a => a.User.UserAuth0Id == userId);
                    return eventDto;
                }).ToList();
                _logger.LogInformation("Events retrieved from database");
                return new JsonResult(Ok(eventDtoAdmins).Value);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPost("addEvent")]
        [Authorize]
        public async Task<JsonResult> PostEvent(EventDtoAdmin newEventDtoAdmin)
        {
            try
            {
                var newEvent = _mapper.Map<Event>(newEventDtoAdmin);
                newEvent.CreateTime = DateTime.UtcNow;
                newEvent.UpdateTime = DateTime.UtcNow;
                newEvent.EventStatusId = (int)StatusEvent.Created;
                newEvent.RowStatusId = (int)StatusEnums.Active;
                await _eventRepositories.Create(newEvent);
                _logger.LogInformation("Event added");
                return new JsonResult(Ok("Event added"));
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }


        [HttpDelete]
        [Authorize]
        public async Task<JsonResult> DeleteEvent(int eventId)
        {
            try
            {
                await _dbRepositories.Delete(eventId, await _userHelper.GetId());
                _logger.LogInformation("Event deleted");
                return new JsonResult(Ok("Event was deleted"));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }
    }
}