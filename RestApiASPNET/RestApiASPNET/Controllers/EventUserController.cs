using System;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using RestApiASPNET.Helpers;

namespace RestApiASPNET.Controllers;
[ApiController]
[Route("event/user")]
public class EventUserController:ControllerBase
{
    private readonly EventUserRepositories _eventUserRepositories;
    private readonly IMapper _mapper;
    private readonly UserHelper _userHelper;

    public EventUserController(EventUserRepositories eventUserRepositories, IMapper mapper, UserHelper userHelper)
    {
        _eventUserRepositories = eventUserRepositories;
        _mapper = mapper;
        _userHelper = userHelper;
    }

    [HttpPost]
    public async Task<JsonResult> AddingUser(EventUserDto eventUserDto)
    {
        try
        {
            var eventUser = _mapper.Map<EventUser>(eventUserDto);
            eventUser.CreateTime = DateTime.Now;
            eventUser.UpdateTime = DateTime.Now;
            var userId = await _userHelper.GetId();
            eventUser.CreateUserId = userId;
            eventUser.UpdateUserId = userId;
            eventUser.RowStatusId = (int)StatusEnums.Active;
            var user = await _eventUserRepositories.Create(eventUser);
            return new JsonResult(Ok(user).Value);
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }
    }

    [HttpDelete]
    public async Task<JsonResult> DeleteUserFromEvent(int userId)
    {
        try
        {
            await _eventUserRepositories.Delete(userId, await _userHelper.GetId());
            return new JsonResult(Ok());
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }
    }
}