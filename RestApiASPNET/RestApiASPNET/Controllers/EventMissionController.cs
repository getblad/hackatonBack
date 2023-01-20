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
        private readonly ILogger<EventMissionController> _logger;
        private readonly IMapper _mapper;
        private readonly EventMissionsRepositories _eventMissionsRepositories;


        public EventMissionController(
            ILogger<EventMissionController> logger, IMapper mapper, EventMissionsRepositories eventMissionsRepositories)
        {
            _logger = logger;
            _mapper = mapper;
            _eventMissionsRepositories = eventMissionsRepositories;
        }
        [HttpPost]
        public async Task<JsonResult> AssignMission(EventMissionDto newEventMission)
        {
            try
            {
                var eventMission = _mapper.Map<EventMission>(newEventMission);
                await _eventMissionsRepositories.AssignMission(eventMission);
                return new JsonResult(Ok("Mission assigned"));

            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
    }
}