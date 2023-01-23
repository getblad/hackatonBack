using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using DataAccessLibrary.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiASPNET.Helpers;
using User = DataAccessLibrary.Models.User;

namespace RestApiASPNET.Controllers;
[ApiController]
[Route("api/management/")]
public class ManagementController:ControllerBase
{
    private readonly IManagementApiClient _managementApiClient;
    private readonly IDbRepositories<User> _dbRepositories;

    public ManagementController(IManagementApiClient managementApiClient, IDbRepositories<User> dbRepositories)
    {
        _managementApiClient = managementApiClient;
        _dbRepositories = dbRepositories;
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

    public async Task<JsonResult> GetSimpleUsers(string? roleId = "rol_J5HE7jbShSwX1f1p")
    {
        try
        {
            var assignedUsers = await _managementApiClient.Roles.GetUsersAsync(roleId);
            var listOfIds = assignedUsers.Select(user => user.UserId).ToList();
            var listOfUsers = await _dbRepositories.Where(a => listOfIds.Contains(a.UserAuth0Id)).GetAll();
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
            return await GetSimpleUsers("rol_XrQtDPjo2Qg7Cl7g");
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
            var claim = (claim2?.FirstOrDefault(a => a.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value.ToString();
            await _managementApiClient.Users.AssignRolesAsync(claim, new AssignRolesRequest(){Roles = new[] { "rol_J5HE7jbShSwX1f1p" }});
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
                new AssignRolesRequest() { Roles = new[] { "rol_XrQtDPjo2Qg7Cl7g" } });
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }

        return new JsonResult(NoContent());
    }
}