using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiASPNET.Helpers;

namespace RestApiASPNET.Controllers
{
    [ApiController]
    [Route("api/Events/Mission")]
    public class EventMissionController : ControllerBase
    {
        private readonly IDbRepositories<EventMission> _dbRepositories;
        private readonly ILogger<EventMissionController> _logger;
        private readonly IMapper _mapper;
        private readonly EventRepositories _eventRepositories;


        public EventMissionController(IDbRepositories<EventMission> dbRepositories, ILogger<EventMissionController> logger, IMapper mapper, EventRepositories eventRepositories)
        {
            _dbRepositories = dbRepositories;
            _logger = logger;
            _mapper = mapper;
            _eventRepositories = eventRepositories;
        }


        // [Authorize("read:users")]
        [HttpGet]
        public async Task<JsonResult> GetEvent(int eventId)
        {
            try
            {

                var @event = await _eventRepositories.GetEvent(eventId);
                
                return new JsonResult(_mapper.Map<EventDtoAdmin>(@event));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public async Task<JsonResult> AssignMission(EventMissionDto newEventMission)
        {
            try
            {
                var eventMission = _mapper.Map<EventMission>(newEventMission);
                await _eventRepositories.AssignMission(eventMission);
                return new JsonResult(Ok("Mission assigned"));

            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
        // [HttpPost]
        // public async Task<JsonResult> PostEvent(EventMissionDto newEventDtoAdmin)
        // {
        //     try
        //     {
        //         var newEvent = _mapper.Map<Event>(newEventDtoAdmin);
        //         newEvent.CreateTime = DateTime.Now;
        //         newEvent.UpdateTime = DateTime.Now;
        //         newEvent.RowStatusId = (int)StatusEnums.Active;
        //         await _dbService.Create(newEvent);
        //         return new JsonResult(Ok("Team is added"));
        //     }
        //     catch(Exception e)
        //     {
        //         return ResponseHelper.HandleException(e);
        //     }
        // }
        //
        // [HttpPost]
        // public async Task<JsonResult> AssignMission(EventMissionDto newEventMission)
        // {
        //     try
        //     {
        //         _dbService.Create<EventMissionDto>(newEventMission);
        //         await _dbService.Create()
        //     }
        //     catch (Exception e)
        //     {
        //         return ResponseHelper.HandleException(e);
        //     }
        // }
        //
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