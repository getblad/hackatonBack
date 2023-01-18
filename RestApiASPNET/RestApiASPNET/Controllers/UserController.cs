using System.Security.Claims;
using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public UsersController(ILogger<UsersController> logger, IMapper mapper, IDbRepositories<User> dbRepositories
            )
        {
            _logger = logger;
            _mapper = mapper;
            _dbRepositories = dbRepositories;
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetUsers()
        {
            var userDb = await _dbRepositories.Get(a => a.Team!).GetAll();
            var userAdmins = userDb.Select(user =>
            {
                var a = _mapper.Map<UserDtoAdmin>(user);
                a.TeamName = user.Team?.TeamName;
                return a;
            }).ToList();
            
            return new JsonResult(Ok(userAdmins).Value);
        }

        [HttpGet("{userId:int}")]

        // [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> GetUserById(int userId)
        {
            try
            {
                var userDb = await _dbRepositories.Get(a => a.Team!).GetOne(userId);
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
        public async Task<JsonResult> PostUser(UserDtoAdmin newUserDto)
        {
            try
            {
                var newUser = _mapper.Map<User>(newUserDto);
                newUser.CreateTime = DateTime.Now;
                newUser.UpdateTime = DateTime.Now;
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
        public async Task<JsonResult> DeleteUser(int userId)
        {

            await _dbRepositories.Delete(userId);
            return new JsonResult(Ok("Object was deleted"));

        }

        [HttpGet("getId")]
        [Authorize]
        public async Task<JsonResult> GetUserIdByAuth0()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(a => 
                a.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            var id = await _dbRepositories.Where(a => a.UserAuth0Id == claim).GetOne();
            return new JsonResult(Ok(id.UserId));

        }

        [HttpGet("getTeam")]
        [Authorize]
        public async Task<JsonResult> GetTeam()
        {
            try
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(a =>
                    a.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                var user = await _dbRepositories.Where(a => a.UserAuth0Id == claim).Get(a => a.Team!).GetOne();
                return new JsonResult(Ok(user.Team?.TeamName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseHelper.HandleException(e);
            }
        }

        [HttpPut]
        public async Task<JsonResult> UpdateUser(UserDtoAdmin userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                user.UpdateTime = DateTime.Now;
                await _dbRepositories.Update(user.UserId, user);
                return new JsonResult(Ok("Update is complete"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
            
        }
        // public async Task<JsonResult> UpdateTeamUser(UserDtoAdmin userDto)
        //  {
        //         try
        //         {
        //             var user = _mapper.Map<User>(userDto);
        //
        //
        //             await _dbRepositories.Update(user.UserId, user);
        //             return new JsonResult(Ok("Update is complete"));
        //         }
        //         catch (Exception e)
        //         {
        //             return ResponseHelper.HandleException(e);
        //         }
        //             
        // }
        
    }
}
