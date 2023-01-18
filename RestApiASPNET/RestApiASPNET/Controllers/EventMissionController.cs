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
    [Route("api/Event/Mission")]
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
        [HttpGet("{eventId:int}")]
        
        public async Task<JsonResult> GetEvent(int eventId)
        {
            try
            {
                var eventMissions = (await _dbRepositories.Get(a => a.Event, b => b.Mission).Where(@event => @event.EventId == eventId).GetAll())
                    .GroupBy(a => a.EventId).FirstOrDefault();
                var @event = eventMissions!.Select(a => a.Event).First();
                var eventsDto = _mapper.Map<EventDtoAdmin>(@event);
                var missions = eventMissions!.Select(a =>
                {
                    var temp = _mapper.Map<MissionDtoAdmin>(a.Mission);
                    temp.MissionPoint = (a.EventMissionPoint ?? temp.MissionPoint);
                    temp.MissionLanguage = (a.EventMissionLanguage ?? temp.MissionLanguage);
                    temp.MissionExecutionTime = a.EventMissionExecutionTime ?? temp.MissionExecutionTime;
                    temp.MissionStepPointFine = a.EventMissionStepPointFine ?? temp.MissionStepPointFine;
                    temp.MissionStepTimeFine = a.EventMissionStepTimeFine ?? temp.MissionStepTimeFine;
                    return temp;

                }).ToList();
                eventsDto.Missions = missions.Where(missionDtoAdmin =>
                        eventMissions!.Any(c => c.MissionId == missionDtoAdmin.MissionId)).ToList();
                // var @event = await _eventRepositories.GetEvent(eventId);
                // var result = @event.FirstOrDefault()!.Select(a => _mapper.Map<EventDtoAdmin>(a));
                // var @event1 = await _dbRepositories.GetWithEveryPropertyInc().
                    // Where(a => a.Event.EventId == eventId).GetAll();
                return new JsonResult(eventsDto);
                // return new JsonResult(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseHelper.HandleException(e);
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