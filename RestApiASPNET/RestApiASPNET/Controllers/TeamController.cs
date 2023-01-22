using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using DataAccessLibrary.Repositories.Interfaces;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestApiASPNET.Helpers;

namespace RestApiASPNET.Controllers
{
    [ApiController]
    [Route("api/Teams/")]
    public class TeamController : ControllerBase
    {
        private readonly IDbRepositories<Team> _dbRepositories;
        private readonly ILogger<TeamController> _logger;
        private readonly IMapper _mapper;
        private readonly UserHelper _userHelper;


        public TeamController(IDbRepositories<Team> dbRepositories, ILogger<TeamController> logger, IMapper mapper, UserHelper userHelper)
        {
            _dbRepositories = dbRepositories;
            _logger = logger;
            _mapper = mapper;
            _userHelper = userHelper;
        }


        [HttpGet("getOne")]
        public async Task<JsonResult> GetTeamByName(string name)
        {
            try
            {
                var team = _mapper.Map<TeamDtoAdmin>(await _dbRepositories.Where(team => team.TeamName == name)
                    .GetAll());
                return new JsonResult(Ok(team));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
        [HttpGet]
        // [Authorize("read:users")]
        public async Task<JsonResult> GetTeams()
        {
            try
            {
                var dbTeams = await _dbRepositories.GetAll();
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
                await _dbRepositories.Create(newTeam);
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
                await _dbRepositories.Delete(teamId, await _userHelper.GetId());
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
                await _dbRepositories.Update(team.TeamId,team);
                return new JsonResult(Ok("Update is complete"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }
    }
}