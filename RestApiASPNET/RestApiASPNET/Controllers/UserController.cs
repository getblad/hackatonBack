using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApiASPNET.Controllers
{
    [ApiController]
    [Route("api/Users/")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;
        private readonly IDbService<User> _dbService;


        public UsersController( ILogger<UsersController> logger, IMapper mapper, IDbService<User> dbService)
        {
            _logger = logger;
            _mapper = mapper;
            _dbService = dbService;
        }
        
        [HttpGet]
        // [Authorize]
        public JsonResult GetUsers()
        {
            var userDb = _dbService.GetAll();
            var userAdmins = userDb.Select(user => _mapper.Map<UserDtoAdmin>(user)).ToList();
            return new JsonResult(Ok(userAdmins).Value);
        }

        [HttpGet( "{userId:int}")]
        
        // [Authorize(Roles = "SystemAdmin")]
        public JsonResult GetUserById(int userId)
        {
            try
            {
                var userDb = _dbService.GetOne(userId);
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
                var user = _dbService.Create(newUser);
                return new JsonResult(Ok(user));


            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new JsonResult(BadRequest(e.Message));
            }

            // return new JsonResult(Ok(newUserDto));

        }

        [HttpDelete]
        public JsonResult DeleteUser(int userId)
        {
            
                _dbService.Delete(userId);
                return new JsonResult(Ok("Object was deleted"));
                
        }

        [HttpPut]
        public JsonResult UpdateUser(UserDtoAdmin userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                
                
               _dbService.Update(user.UserId,user);
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