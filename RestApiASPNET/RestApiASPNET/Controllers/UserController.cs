using AutoMapper;
using DataAccessLibrary;
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
        private readonly IMapper _mapper;


        public UsersController(IUserService userService , ILogger<UsersController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet]
        // [Authorize]
        public JsonResult GetUsers()
        {
            
            var userDb = _userService.GetUsersSys();
            var userAdmins = userDb.Select(user => _mapper.Map<UserDtoAdmin>(user)).ToList();
            return new JsonResult(Ok(userAdmins).Value);
        }

        [HttpGet( "{userId:int}")]
        
        // [Authorize(Policy = "Administrator")]
        public JsonResult GetUserById(int userId)
        {
            try
            {
                var userDb = _userService.SingleUser(userId);
                var user = _mapper.Map<UserDtoAdmin>(userDb);
                return new JsonResult(Ok(user).Value);

            }
            catch (Exception e)
            {
                return new JsonResult(NotFound(e.Message));
                
            }

        }

        [HttpPost]
        public JsonResult PostUser(UserDtoAdmin newUserDto)
        {
            try
            {
                var newUser = _mapper.Map<User>(newUserDto);
                newUser.CreateTime = DateTime.Now;
                newUser.UpdateTime = DateTime.Now;
                newUser.RowStatusId = (int)StatusEnums.Active;
                _userService.AddUser(newUser);
                return new JsonResult(Ok("User is added"));


            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new JsonResult(BadRequest(e.Message));
            }

            return new JsonResult(Ok(newUserDto));

        }

        [HttpDelete]
        public JsonResult DeleteUser(int userId)
        {
            
                _userService.DeleteUser(userId);
                return new JsonResult(Ok("Object was deleted"));
                
        }

        [HttpPut]
        public JsonResult UpdateUser(UserDtoAdmin userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                
                
                _userService.UpdateUser(user);
                return new JsonResult(Ok("Update is complete"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new JsonResult(BadRequest(e.Message));
            }
        }
    }
}