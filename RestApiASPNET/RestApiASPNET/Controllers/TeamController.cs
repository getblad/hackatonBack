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


        public TeamController(ITeamService teamService, ILogger<TeamController> logger)
        {
            _teamService = teamService;
            _logger = logger;
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
        //     [HttpPost]
        //     public JsonResult PostUser(UserAdmin newUser)
        //     {
        //         try
        //         {
        //             _userService.AddUser(newUser);
        //             return new JsonResult(Ok("User is added"));
        //
        //
        //         }
        //         catch(Exception e)
        //         {
        //             Console.WriteLine(e);
        //         }
        //
        //         return new JsonResult(Ok(newUser));
        //
        //     }
        //
        //     [HttpDelete]
        //     public JsonResult DeleteUser(int userId)
        //     {
        //         
        //             _userService.DeleteUser(userId);
        //             return new JsonResult(Ok("Object was deleted"));
        //         
        //
        //         
        //     }
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