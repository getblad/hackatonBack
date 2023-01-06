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
        // [Authorize("read:users")]
        public List<UserAdmin> GetUsers()
        {
            // var json = new JsonResult(_userService.GetUsersSys());
            return _userService.GetUsersSys();
        }

        [HttpGet( "{userId:int}")]
        
        // [Authorize("read:user")]
        public JsonResult GetUserById(int userId)
        {
            try
            {
                var user = _userService.SingleUser(userId);
                return new JsonResult(Ok(user).Value);

            }
            catch (Exception e)
            {
                return new JsonResult(NotFound(e.Message));
                
            }

        }

        [HttpPost]
        public JsonResult PostUser(UserAdmin newUser)
        {
            try
            {
                _userService.AddUser(newUser);
                return new JsonResult(Ok("User is added"));


            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return new JsonResult(Ok(newUser));

        }

        [HttpDelete]
        public JsonResult DeleteUser(int userId)
        {
            
                _userService.DeleteUser(userId);
                return new JsonResult(Ok("Object was deleted"));
            

            
        }

        [HttpPut]
        public JsonResult UpdateUser(UserAdmin user)
        {
            _userService.UpdateUser(user);
            return new JsonResult(Ok("Update is complete"));
        }
    }
}