using AutoMapper;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories.Interfaces;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiASPNET.Helpers;

namespace RestApiASPNET.Controllers
{
    [ApiController]
    [Route("api/Users/")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;
        private readonly IDbRepositories<User> _dbRepositories;
        private readonly UserHelper _userHelper;

        public UsersController(ILogger<UsersController> logger, IMapper mapper, IDbRepositories<User> dbRepositories,
           UserHelper userHelper )
        {
            _logger = logger;
            _mapper = mapper;
            _dbRepositories = dbRepositories;
            _userHelper = userHelper;
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetUsers()
        {
            try
            {
                var userDb = await _dbRepositories.Get(a => a.Team!).GetAll();
                var userAdmins = userDb.Select(user =>
                {
                    var a = _mapper.Map<UserDtoAdmin>(user);
                    a.TeamName = user.Team?.TeamName;
                    return a;
                }).ToList();
                _logger.LogInformation($"Users retrieved by user:{_userHelper.GetId()}");
                return new JsonResult(Ok(userAdmins).Value);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet("{userId:int}")]
        [Authorize]
        // [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> GetUserById(int userId)
        {
            try
            {
                var userDb = await _dbRepositories.Get(a => a.Team!).Where(b => b.UserId == userId).GetOne();
                var user = _mapper.Map<UserDtoAdmin>(userDb);
                user.TeamName = userDb.Team?.TeamName;
                return new JsonResult(Ok(user).Value);

            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> PostUser(UserDtoAdmin newUserDto)
        {
            try
            {
                var newUser = _mapper.Map<User>(newUserDto);
                newUser.CreateTime = DateTime.UtcNow;
                newUser.UpdateTime = DateTime.UtcNow;
                newUser.RowStatusId = (int)StatusEnums.Active;
                var user = await _dbRepositories.Create(newUser);
                return new JsonResult(Ok(_mapper.Map<UserDtoAdmin>(user)));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> DeleteUser(int userId)
        {

            try
            {
                await _dbRepositories.Delete(userId, await _userHelper.GetId() );
                return new JsonResult(Ok("Object was deleted"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet("getId")]
        [Authorize]
        public async Task<JsonResult> GetUserIdByAuth0()
        {
            try
            {
                return new JsonResult(await _userHelper.GetId());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpGet("getTeam")]
        [Authorize]
        public async Task<JsonResult> GetTeam()
        {
            try
            {
                var authId = _userHelper.GetAuthId();
                var user = await _dbRepositories.Where(a => a.UserAuth0Id == authId).Get(a => a.Team!).GetOne();
                return new JsonResult(Ok(user.Team?.TeamName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<JsonResult> UpdateUser(UserDtoAdmin userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                user.UpdateTime = DateTime.UtcNow;
                await _dbRepositories.Update(user.UserId, user);
                return new JsonResult(Ok("Update is complete"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
            
        }
    }
}
