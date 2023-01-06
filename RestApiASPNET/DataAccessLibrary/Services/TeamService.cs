using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;

namespace DataAccessLibrary.Services;

public class TeamService:ITeamService
{
    private HpContext _context;
    public TeamService(HpContext context)
    {
        _context = context;
    }
    public List<TeamAdmin> GetTeams()
    {
       var dbTeams = _context.Teams.Where(e => e.RowStatusId == (int)StatusEnums.Active).ToList();
       List<TeamAdmin> teamAdmins = new List<TeamAdmin>();
       foreach (var team in dbTeams)
       {
           teamAdmins.Add(_dbTeamAdmin(team));
       }

       return teamAdmins;
    }

    public void AddTeam(Team newTeam)
    {
        throw new NotImplementedException();
    }

    public void UpdateTeam(Team team)
    {
        throw new NotImplementedException();
    }

    public void DeleteTeam(int id)
    {
        throw new NotImplementedException();
    }

    private TeamAdmin _dbTeamAdmin(Team team)
    {
        return new TeamAdmin
        {
            TeamId = team.TeamId,
            TeamName = team.TeamName,
            TeamAvatar = team.TeamAvatar,
            TeamCapitanId = team.TeamCapitanId,
            CreateUserId = team.CreateUserId,
            UpdateUserId = team.UpdateUserId,
            CreateTime = team.CreateTime,
            UpdateTime = team.UpdateTime,

        };
    }
}