using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
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
        private readonly IDbService<User> _dbService;

        public UsersController(ILogger<UsersController> logger, IMapper mapper, IDbService<User> dbService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _dbService = dbService;
        }

        [HttpGet]
        // [Authorize]
        public async Task<JsonResult> GetUsers()
        {
            var userDb = await _dbService.GetAll();
            var userAdmins = userDb.Select(user => _mapper.Map<UserDtoAdmin>(user)).ToList();
            return new JsonResult(Ok(userAdmins).Value);
        }

        [HttpGet("{userId:int}")]

        // [Authorize(Roles = "SystemAdmin")]
        public async Task<JsonResult> GetUserById(int userId)
        {
            try
            {
                var userDb = await _dbService.GetOne(userId);
                var user = _mapper.Map<UserDtoAdmin>(userDb);
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
                var user = await _dbService.Create(newUser);
                return new JsonResult(Ok(user));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
            // return new JsonResult(Ok(newUserDto));
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteUser(int userId)
        {

            await _dbService.Delete(userId);
            return new JsonResult(Ok("Object was deleted"));

        }

        [HttpPut]
        public async Task<JsonResult> UpdateUser(UserDtoAdmin userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);


                await _dbService.Update(user.UserId, user);
                return new JsonResult(Ok("Update is complete"));
            }
            catch (Exception e)
            {
                return ResponseHelper.HandleException(e);
            }
            
        }
    }
}
