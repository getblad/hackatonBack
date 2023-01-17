using System.Security.Claims;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<JsonResult> AssignRole()
    {
        try
        {
            var claims = ClaimsPrincipal.Current?.Identity as ClaimsIdentity;
            var claim = (claims?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.ToString();
            await _managementApiClient.Users.AssignRolesAsync(claim, new AssignRolesRequest(){Roles = new[] { "rol_J5HE7jbShSwX1f1p" }});
        }
        catch (Exception e)
        {
           return ResponseHelper.HandleException(e);
        }

        return null!;
    }
}