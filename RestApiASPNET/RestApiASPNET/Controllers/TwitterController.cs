using AutoMapper;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace RestApiASPNET.Controllers;

[ApiController]
[Route("api/Twitter/")]
public class TwitterController : ControllerBase
{
    private readonly IDbRepositories<Event> _dbRepositories;
    private readonly IMapper _mapper;
    private readonly EventRepositories _eventRepositories;

    public TwitterController(IDbRepositories<Event> dbRepositories, IMapper mapper, EventRepositories eventRepositories)
    {
        _dbRepositories = dbRepositories;
        _mapper = mapper;
        _eventRepositories = eventRepositories;
    }
    
    [HttpPost("{eventId:int}/{twitterBonus:int}")]
    
    public async Task<JsonResult> AddBonusForRepost(int eventId, int twitterBonus, [FromBody]string[] userNames)
    {
            var a =  await _eventRepositories.GetTeamsByTwitter(eventId, twitterBonus,userNames
            );
            return new JsonResult(Ok());
    }
}