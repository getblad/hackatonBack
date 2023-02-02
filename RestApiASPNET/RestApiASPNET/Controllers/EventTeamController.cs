using AutoMapper;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories.Interfaces;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiASPNET.Helpers;

namespace RestApiASPNET.Controllers;


    [ApiController]
    [Route("api/Event/Teams")]
public class EventTeamController:ControllerBase
{
    private readonly ILogger<EventTeamController> _logger;
    private readonly IDbRepositories<EventTeam> _dbRepositories;
    private readonly IMapper _mapper;
    private readonly UserHelper _userHelper;

    public EventTeamController(ILogger<EventTeamController> logger, IDbRepositories<EventTeam> dbRepositories, IMapper mapper, UserHelper userHelper)
    {
        _logger = logger;
        _dbRepositories = dbRepositories;
        _mapper = mapper;
        _userHelper = userHelper;
    }

    [HttpGet("{eventId:int}")]
    public async Task<JsonResult> GetAllTeams(int eventId)
    {
        try
        {
            var eventTeams = await _dbRepositories.Where(hackathon => hackathon.EventId == eventId).GetAll();
            var eventTeamsDbo = _mapper.Map<List<EventTeamDto>>(eventTeams);
            _logger.LogInformation("get teams from event");    
            return new JsonResult(Ok(eventTeamsDbo).Value);

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return ResponseHelper.HandleException(e);
        }   
    }

    [HttpGet("{eventId:int}/{teamId:int}")]
    public async Task<JsonResult> GetTeam(int eventId, int teamId )
    {
        try
        {
            var eventTeam = await _dbRepositories.Where(team => team.EventId == eventId && team.TeamId == teamId).GetOne();
            var eventTeamDbo = _mapper.Map<EventTeamDto>(eventTeam);
            _logger.LogInformation("get team from event");
            return new JsonResult(Ok(eventTeamDbo).Value);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return ResponseHelper.HandleException(e);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<JsonResult> AddTeam(EventTeamDto teamDto)
    {
        try
        {
            var team = _mapper.Map<EventTeam>(teamDto);
            team.CreateTime = team.UpdateTime = DateTime.UtcNow;
            team.CreateUserId = team.UpdateUserId = await _userHelper.GetId();
            team.RowStatusId = (int)StatusEnums.Active;
            await _dbRepositories.Create(team);
            _logger.LogInformation("added team");
            return new JsonResult(Ok());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return ResponseHelper.HandleException(e);
        }
    }

    [HttpDelete]
    public async Task<JsonResult> DeleteTeam(int eventId, int teamId)
    {
        try
        {
            var team = await _dbRepositories.Where(a => a.TeamId == teamId && a.EventId == eventId).GetOne();
            await _dbRepositories.Delete(team.EventTeamId, await _userHelper.GetId());
            _logger.LogInformation($"Delete Team for event {eventId}");
            return new JsonResult(Ok());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return ResponseHelper.HandleException(e);
        }
    }

}