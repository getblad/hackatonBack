using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Http;

namespace DataAccessLibrary.Services;

public class UserHelper
{
    private readonly IHttpContextAccessor _context;
    private readonly IDbRepositories<User> _dbRepositories;

    public UserHelper(IHttpContextAccessor context, IDbRepositories<User> dbRepositories)
    {
        _context = context;
        _dbRepositories = dbRepositories;
    }

    public async Task<int> GetId()
    {
        var claim = _context.HttpContext?.User.Claims.FirstOrDefault(a => 
        a.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        var id = await _dbRepositories.Where(a => a.UserAuth0Id == claim).GetOne();
        return id.UserId;
    }

    public string? GetAuthId()
    {
        var claim = _context.HttpContext?.User.Claims.FirstOrDefault(a =>
        a.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        return claim;
    }
}