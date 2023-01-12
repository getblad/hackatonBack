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
    [Route("api/Teams/")]
    public class TeamController : ControllerBase
    {
        private readonly IDbService<Team> _dbService;
        private readonly ILogger<TeamController> _logger;
        private readonly IMapper _mapper;


        public TeamController(IDbService<Team> dbService, ILogger<TeamController> logger, IMapper mapper)
        {
            _dbService = dbService;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        // [Authorize("read:users")]
        public async Task<JsonResult> GetTeams()
        {
            try
            {
                var dbTeams = await _dbService.GetAll();
                var teamAdmins = dbTeams.Select(team => _mapper.Map<TeamDtoAdmin>(team)).ToList();
                return new JsonResult(Ok(teamAdmins).Value);
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPost]
        public async Task<JsonResult> PostTeam(TeamDtoAdmin newTeamDto)
        {
            try
            {
                var newTeam = _mapper.Map<Team>(newTeamDto);
                newTeam.CreateTime = DateTime.Now;
                newTeam.UpdateTime = DateTime.Now;
                newTeam.RowStatusId = (int)StatusEnums.Active;
                await _dbService.Create(newTeam);
                return new JsonResult(Ok("Team is added"));
            }
            catch(Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
        
        [HttpDelete]
        public async Task<JsonResult> DeleteTeam(int teamId)
        {
            try
            { 
                 await _dbService.Delete(teamId);
                return new JsonResult(Ok("Team was deleted"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
            
        }

        [HttpPut]
        public async Task<JsonResult> UpdateTeam(TeamDtoAdmin newTeamDtoAdmin)
        {
            try
            {
                var team = _mapper.Map<Team>(newTeamDtoAdmin);
                await _dbService.Update(team.TeamId,team);
                return new JsonResult(Ok("Update is complete"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
    }
}