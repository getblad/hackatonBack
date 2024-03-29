﻿using AutoMapper;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiASPNET.Helpers;

namespace RestApiASPNET.Controllers;
[ApiController]
[Route("api/event/user")]
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
            eventUser.CreateTime = DateTime.UtcNow;
            eventUser.UpdateTime = DateTime.UtcNow;
            var userId = await _userHelper.GetId();
            eventUser.CreateUserId = userId;
            eventUser.UpdateUserId = userId;
            eventUser.RowStatusId = (int)StatusEnums.Active;
            var user = await _eventUserRepositories.Create(eventUser);
            return new JsonResult(Ok());
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<JsonResult> DeleteUserFromEvent(EventUserDto eventUserDto)
    {
        try
        {
            await _eventUserRepositories.Delete(eventUserDto.EventId, eventUserDto.UserId, await _userHelper.GetId());
            return new JsonResult(Ok());
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }
    }
}