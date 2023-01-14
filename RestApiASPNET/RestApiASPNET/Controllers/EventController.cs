using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
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
        private readonly IDbService<Event> _dbService;
        private readonly ILogger<EventController> _logger;
        private readonly IMapper _mapper;


        public EventController(IDbService<Event> dbService, ILogger<EventController> logger, IMapper mapper)
        {
            _dbService = dbService;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        // [Authorize("read:users")]
        public async Task<JsonResult> GetEvents()
        {
            try
            {
                var dbEvents = await _dbService.GetAll();
                var eventDtoAdmins = dbEvents.Select(@event => _mapper.Map<EventDtoAdmin>(@event)).ToList();
                return new JsonResult(Ok(eventDtoAdmins).Value);
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPost]
        public async Task<JsonResult> PostEvent(EventDtoAdmin newEventDtoAdmin)
        {
            try
            {
                var newEvent = _mapper.Map<Event>(newEventDtoAdmin);
                newEvent.CreateTime = DateTime.Now;
                newEvent.UpdateTime = DateTime.Now;
                newEvent.RowStatusId = (int)StatusEnums.Active;
                newEvent.EventStatusId = 1;
                await _dbService.Create(newEvent);
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