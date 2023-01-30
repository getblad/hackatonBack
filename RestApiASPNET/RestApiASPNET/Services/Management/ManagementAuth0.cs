using Auth0.ManagementApi;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories.Interfaces;

namespace RestApiASPNET.Services.Management;

public class ManagementAuth0
{
    private readonly IManagementApiClient _managementApiClient;
    private readonly IDbRepositories<User> _dbRepositories;

    public ManagementAuth0(IManagementApiClient managementApiClient, IDbRepositories<User> dbRepositories)
    {
        _managementApiClient = managementApiClient;
        _dbRepositories = dbRepositories;
    }

    public async Task<List<User>> GetUsersByRole(string roleId)
    {
        try
        {
            var assignedUsers = await _managementApiClient.Roles.GetUsersAsync(roleId);
            var listOfIds = assignedUsers.Select(user => user.UserId).ToList();
            var listOfUsers = await _dbRepositories.Where(a => listOfIds.Contains(a.UserAuth0Id)).GetAll();
            return listOfUsers;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}