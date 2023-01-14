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
    [Route("api/Missions/")]
    public class MissionController : ControllerBase
    {
        private readonly IDbService<Mission> _dbService;
        private readonly ILogger<MissionController> _logger;
        private readonly IMapper _mapper;


        public MissionController(IDbService<Mission> dbService, ILogger<MissionController> logger, IMapper mapper)
        {
            _dbService = dbService;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        // [Authorize("read:users")]
        public async Task<JsonResult> GetMissions()
        {
            try
            {
                var dbMissions = await _dbService.GetAll();
                var missionsAdmin = dbMissions.Select(team => _mapper.Map<MissionDtoAdmin>(team)).ToList();
                return new JsonResult(Ok(missionsAdmin).Value);
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPost]
        public async Task<JsonResult> PostMission(MissionDtoAdmin newMission)
        {
            try
            {
                var nemMission = _mapper.Map<Mission>(newMission);
                nemMission.CreateTime = DateTime.Now;
                nemMission.UpdateTime = DateTime.Now;
                nemMission.RowStatusId = (int)StatusEnums.Active;
                await _dbService.Create(nemMission);
                return new JsonResult(Ok("Team is added"));
            }
            catch(Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
        
        [HttpDelete]
        public async Task<JsonResult> DeleteMission(int missionId)
        {
            try
            { 
                 await _dbService.Delete(missionId);
                return new JsonResult(Ok("Team was deleted"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
            
        }

        [HttpPut]
        public async Task<JsonResult> UpdateTeam(MissionDtoAdmin newMissionDto)
        {
            try
            {
                var mission = _mapper.Map<Mission>(newMissionDto);
                await _dbService.Update(mission.MissionId,mission);
                return new JsonResult(Ok("Update is complete"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
    }
}