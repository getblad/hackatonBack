using AutoMapper;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories.Interfaces;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiASPNET.Helpers;
using MissionType = DataAccessLibrary.Enums.MissionType;

namespace RestApiASPNET.Controllers
{
    [ApiController]
    [Route("api/Missions/")]
    public class MissionController : ControllerBase
    {
        private readonly IDbRepositories<Mission> _dbRepositories;
        private readonly ILogger<MissionController> _logger;
        private readonly IMapper _mapper;
        private readonly UserHelper _userHelper;


        public MissionController(IDbRepositories<Mission> dbRepositories, ILogger<MissionController> logger, IMapper mapper, UserHelper userHelper )
        {
            _dbRepositories = dbRepositories;
            _logger = logger;
            _mapper = mapper;
            _userHelper = userHelper;
        }

        [HttpGet("mains")]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> GetMainMissions()
        {
            try
            {
                var dbMissions = await _dbRepositories.Where(a => a.MissionTypeId == (int)MissionType.Main).GetAll();
                var missionsAdmin = dbMissions.Select(mission => _mapper.Map<MissionDtoAdmin>(mission)).ToList();
                return new JsonResult(Ok(missionsAdmin).Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> GetMissions()
        {
            try
            {
                var dbMissions = await _dbRepositories.GetAll();
                var missionsAdmin = dbMissions.Select(team => _mapper.Map<MissionDtoAdmin>(team)).ToList();
                return new JsonResult(Ok(missionsAdmin).Value);
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPost]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> PostMission(MissionDtoAdmin newMission)
        {
            try
            {
                var nemMission = _mapper.Map<Mission>(newMission);
                nemMission.CreateTime = DateTime.UtcNow;
                nemMission.UpdateTime = DateTime.UtcNow;
                nemMission.RowStatusId = (int)StatusEnums.Active;
                await _dbRepositories.Create(nemMission);
                return new JsonResult(Ok("Mission is added"));
            }
            catch(Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
        
        [HttpDelete]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> DeleteMission(int missionId)
        {
            try
            { 
                await _dbRepositories.Delete(missionId, await _userHelper.GetId());
                
                return new JsonResult(Ok("Mission was deleted"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
            
        }

        [HttpPut]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> UpdateMission(MissionDtoAdmin newMissionDto)
        {
            try
            {
                var mission = _mapper.Map<Mission>(newMissionDto);
                await _dbRepositories.Update(mission.MissionId,mission);
                return new JsonResult(Ok("Update is complete"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
    }
}