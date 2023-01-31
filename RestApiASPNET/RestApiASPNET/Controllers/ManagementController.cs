using System.Security.Claims;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiASPNET.Helpers;
using RestApiASPNET.Services.Management;

namespace RestApiASPNET.Controllers;
[ApiController]
[Route("api/management/")]
public class ManagementController:ControllerBase
{
    private readonly IManagementApiClient _managementApiClient;
    private readonly IConfiguration _configuration;
    private readonly ManagementAuth0 _managementAuth0;

    public ManagementController(IManagementApiClient managementApiClient,IConfiguration configuration,
        ManagementAuth0 managementAuth0)
    {
        _managementApiClient = managementApiClient;
        _configuration = configuration;
        _managementAuth0 = managementAuth0;
    }

    [HttpGet]
    [Authorize(Roles = "SystemAdmin")]
    public async Task<JsonResult> GetUsers()
    {
        try
        {
            var users = await _managementApiClient
                .Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
            return new JsonResult(users);
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }
    }
    [HttpGet("simple_users")]
    [Authorize(Roles = "SystemAdmin")]

    public async Task<JsonResult> GetSimpleUsers(string? roleId )
    {
        try
        {
            roleId ??= _configuration["Roles:Simple_User"];
            var listOfUsers = await _managementAuth0.GetUsersByRole(roleId!);
            return new JsonResult(listOfUsers);
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }
    }

    [HttpGet("moderators")]
    [Authorize(Roles = "SystemAdmin")]
    public async Task<JsonResult> GetModerators()
    {
        try
        {
            var listOfUsers = await _managementAuth0.GetUsersByRole(_configuration["Roles:Moderator"]!);
            return new JsonResult(listOfUsers);
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }
    }
    [HttpDelete("deleteRoles/{userId}")]
    [Authorize(Roles = "SystemAdmin")]
    public async Task<JsonResult> DeleteUserRolesByUserId(string userId)
    {
        try
        {
            var rolesToDelete = await _managementApiClient.Users.GetRolesAsync(
                userId, new PaginationInfo()
            );
            var rolesIdToDeleteStr = rolesToDelete.Select(x => x.Id).ToArray();
            await _managementApiClient.Users.RemoveRolesAsync(
                userId, new AssignRolesRequest() { Roles = rolesIdToDeleteStr }
            );
            return new JsonResult(NoContent());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return ResponseHelper.HandleException( true);
        }
    }
    [HttpPost("autoAssign")]
    [Authorize]
    public async Task<JsonResult> AssignRole()
    {
        try
        {
            var claim2 = HttpContext.User.Claims;
            var claim = (claim2?.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier))?.Value.ToString();
            await _managementApiClient.Users.AssignRolesAsync(claim, new AssignRolesRequest(){Roles = new[] { _configuration["Roles:Simple_User"] }});
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }

        return new JsonResult(Ok());
    }
    [HttpPost("assignModerator")]
    [Authorize(Roles = "SystemAdmin")]
    public async Task<JsonResult> AssignModerator(string userId)
    
    {
        try
        {
            await DeleteUserRolesByUserId(userId);
            await _managementApiClient.Users.AssignRolesAsync(userId,
                new AssignRolesRequest() { Roles = new[] { _configuration["Roles:Moderator"] } });
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }

        return new JsonResult(NoContent());
    }
}