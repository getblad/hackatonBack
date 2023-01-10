using AutoMapper;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApiASPNET.Controllers
{
    [ApiController]
    [Route("api/Teams/")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ILogger<TeamController> _logger;
        private readonly IMapper _mapper;


        public TeamController(ITeamService teamService, ILogger<TeamController> logger, IMapper mapper)
        {
            _teamService = teamService;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        // [Authorize("read:users")]
        public JsonResult GetTeams()
        {

            return new JsonResult(_teamService.GetTeams());
        }

        //     [HttpGet( "{userId:int}")]
        //     
        //     // [Authorize("read:user")]
        //     public JsonResult GetUserById(int userId)
        //     {
        //         try
        //         {
        //             var user = _userService.SingleUser(userId);
        //             return new JsonResult(Ok(user).Value);
        //
        //         }
        //         catch (Exception e)
        //         {
        //             return new JsonResult(NotFound(e.Message));
        //             
        //         }
        //
        //     }
        //
        [HttpPost]
        public JsonResult PostTeam(TeamDtoAdmin newTeamDto)
        {
            try
            {
                var newTeam = _mapper.Map<Team>(newTeamDto);
                newTeam.CreateTime = DateTime.Now;
                newTeam.UpdateTime = DateTime.Now;
                _teamService.AddTeam(newTeam);
                return new JsonResult(Ok("Team is added"));
        
        
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        
            return new JsonResult(Ok(newTeamDto));
        
        }
        
        [HttpDelete]
        public JsonResult DeleteUser(int teamId)
        {
            
                _teamService.DeleteTeam(teamId);
                return new JsonResult(Ok("Team was deleted"));
            
        
            
        }
        //
        //     [HttpPut]
        //     public JsonResult UpdateUser(UserAdmin user)
        //     {
        //         _userService.UpdateUser(user);
        //         return new JsonResult(Ok("Update is complete"));
        //     }
        // }
    }
}