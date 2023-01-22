using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using DataAccessLibrary.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiASPNET.Helpers;

namespace RestApiASPNET.Controllers;

[ApiController]
[Route("api/Twitter/")]
public class TwitterController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly EventUserRepositories _eventUserRepositories;

    public TwitterController(IMapper mapper, EventUserRepositories eventUserRepositories)
    {
        _mapper = mapper;
        _eventUserRepositories = eventUserRepositories;
    }
    
    [HttpPost("{eventId:int}/{twitterBonus:int}")]
    [Authorize] 
    public async Task<JsonResult> AddBonusForRepost(int eventId, int twitterBonus, [FromBody]string[] userNames)
    {
        try
        {
            var eventTeams =  await _eventUserRepositories.GetTeamsByTwitter(eventId, twitterBonus,userNames
            );
            return new JsonResult(Ok(eventTeams.Select(a => a?.Team.TeamName)));
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }
    }
}