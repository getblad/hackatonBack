using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Mvc;
using RestApiASPNET.Helpers;

namespace RestApiASPNET.Controllers;
[ApiController]
[Route("api/management/")]
public class ManagementController:ControllerBase
{
    private readonly IManagementApiClient _managementApiClient;

    public ManagementController(IManagementApiClient managementApiClient)
    {
        _managementApiClient = managementApiClient;
    }

    [HttpGet]
    public async Task<JsonResult> GetUsers()
    {
        try
        {
            var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
            return new JsonResult(users);
        }
        catch (Exception e)
        {
            return ResponseHelper.HandleException(e);
        }
    }

    [HttpPost]
    public async Task<JsonResult> AssignRole(string userId, string roles)
    {
        try
        {
            await _managementApiClient.Users.AssignRolesAsync(userId, new AssignRolesRequest(){Roles = new[] { roles }});
        }
        catch (Exception e)
        {
           return ResponseHelper.HandleException(e);
        }

        return null!;
    }
}