using AutoMapper;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
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


        public EventController( ILogger<EventController> logger, IMapper mapper,
            EventRepositories eventRepositories)
        {
            _logger = logger;
            _mapper = mapper;
            _eventRepositories = eventRepositories;
        }

        [HttpGet("Missions/{eventId:int}")]
        
        public async Task<JsonResult> GetEvent(int eventId, int twitterBonus)
        {
            var @event = await _eventRepositories.GetEventMissions(eventId);
            var eventDtoAdmin = _mapper.Map<EventDtoAdmin>(@event);
            return new JsonResult(Ok( eventDtoAdmin));
        }

        [HttpGet]
        // [Authorize("read:users")]
        public async Task<JsonResult> GetEvents()
        {
            try
            {
                var dbEvents = await _eventRepositories.GetAll();
                var eventDtoAdmins = dbEvents.Select(@event => _mapper.Map<EventDtoAdmin>(@event)).ToList();
                return new JsonResult(Ok(eventDtoAdmins));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPost("addEvent")]
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
                return new JsonResult(Ok("Event is added"));
            }
            catch(Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

       
        // [HttpDelete]
        // public async Task<JsonResult> DeleteMission(int missionId)
        // {
        //     try
        //     { 
        //          await _dbService.Delete(missionId);
        //         return new JsonResult(Ok("Team was deleted"));
        //     }
        //     catch (Exception e)
        //     {
        //         return ResponseHelper.HandleException(e);
        //     }
        //     
        // }
        //
        // [HttpPut]
        // public async Task<JsonResult> UpdateTeam(MissionDtoAdmin newMissionDto)
        // {
        //     try
        //     {
        //         var mission = _mapper.Map<Mission>(newMissionDto);
        //         await _dbService.Update(mission.MissionId,mission);
        //         return new JsonResult(Ok("Update is complete"));
        //     }
        //     catch (Exception e)
        //     {
        //         return ResponseHelper.HandleException(e);
        //     }
        // }
    }
}