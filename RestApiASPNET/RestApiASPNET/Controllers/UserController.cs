using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApiASPNET.Controllers
{
    [ApiController]
    [Route("api/Users/")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;


        public UsersController(IUserService userService , ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        


     

        [HttpGet]
        [Authorize]
        public JsonResult GetUsers()
        {

            return new JsonResult(Ok(_userService.GetUsers()));
        }

        [HttpGet( "{userId:int}")]
        
        public JsonResult GetUserById(int userId)
        {
            var user = _userService.SingleUser(userId);
            return new JsonResult(Ok(user).Value);
        }

        [HttpPost]
        public JsonResult PostUser(User newUser)
        {
            try
            {
                _userService.UpdateUser(newUser);

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return new JsonResult(Ok(newUser));

        }
    }
}