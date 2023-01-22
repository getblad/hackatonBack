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
        [Authorize]
        
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
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetEvents()
        {
            try
            {
                var dbEvents = await _eventRepositories.GetAll();
                var eventDtoAdmins = dbEvents.Select(@event => _mapper.Map<EventDtoAdmin>(@event)).ToList();
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
                newEvent.CreateTime = DateTime.Now;
                newEvent.UpdateTime = DateTime.Now;
                newEvent.EventStatusId = (int)StatusEvent.Created;
                newEvent.RowStatusId = (int)StatusEnums.Active;
                await _eventRepositories.Create(newEvent);
                _logger.LogInformation("Event added");
                return new JsonResult(Ok("Event is added"));
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