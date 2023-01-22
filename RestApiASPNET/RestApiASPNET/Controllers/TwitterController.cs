using AutoMapper;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApiASPNET.Controllers;

[ApiController]
[Route("api/Twitter/")]
public class TwitterController : ControllerBase
{
    private readonly IDbRepositories<Event> _dbRepositories;
    private readonly IMapper _mapper;
    private readonly TwitterRepositories _twitterRepositories;

    public TwitterController(IDbRepositories<Event> dbRepositories, IMapper mapper, TwitterRepositories twitterRepositories)
    {
        _dbRepositories = dbRepositories;
        _mapper = mapper;
        _twitterRepositories = twitterRepositories;
    }
    
    [HttpPost("{eventId:int}/{twitterBonus:int}")]
    [Authorize] 
    public async Task<JsonResult> AddBonusForRepost(int eventId, int twitterBonus, [FromBody]string[] userNames)
    {
            var a =  await _twitterRepositories.GetTeamsByTwitter(eventId, twitterBonus,userNames
            );
            return new JsonResult(Ok());
    }
}