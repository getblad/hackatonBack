using AutoMapper;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace RestApiASPNET.Controllers;

[ApiController]
[Route("api/Users/")]
public class TwitterController : ControllerBase
{
    private readonly IDbRepositories<Event> _dbRepositories;
    private readonly IMapper _mapper;

    public TwitterController(IDbRepositories<Event> dbRepositories, IMapper mapper)
    {
        _dbRepositories = dbRepositories;
        _mapper = mapper;
    }
    
    // [HttpGet]
    //
    // public Task<JsonResult> AddBonusForRepost([FromBody]string[] userNames)
    // {
    //     var teams = _dbRepositories.Get("EventTeams.Team.Users")
    //         .Where(a => a.EventStatusId == (int)StatusEvent.Started).Selector<ICollection<EventTeam>>(a => a.EventTeams.)
    // }
}