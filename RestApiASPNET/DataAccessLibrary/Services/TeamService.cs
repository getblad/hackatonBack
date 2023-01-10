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
    public List<TeamDtoAdmin> GetTeams()
    {
       var dbTeams = _context.Teams.Where(e => e.RowStatusId == (int)StatusEnums.Active).ToList();
       List<TeamDtoAdmin> teamAdmins = new List<TeamDtoAdmin>();
       foreach (var team in dbTeams)
       {
           teamAdmins.Add(_dbTeamAdmin(team));
       }

       return teamAdmins;
    }

    public void AddTeam(Team newTeam)
    {
        try
        {
            
            // Team addingTeam = new Team
            // {
            //     
            //     TeamName = newTeamDto.TeamName,
            //     
            //     TeamAvatar = newTeamDto.TeamAvatar,
            //     TeamCapitanId = newTeamDto.TeamCapitanId,
            //     TeamId = newTeamDto.TeamId,
            //     CreateUserId = newTeamDto.CreateUserId,
            //     CreateUser = null,
            //     UpdateUserId = newTeamDto.UpdateUserId,
            //     UpdateUser = null,
            //     CreateTime = DateTime.Now,
            //     UpdateTime = DateTime.Now,
            //     RowStatusId = (int)StatusEnums.Active,
            //     RowStatus = null,
            //     
            // };
            //  
             _context.Teams.Add(newTeam);
            
            _context.SaveChanges();
           

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;

        }
    }

    public void UpdateTeam(Team team)
    {
        throw new NotImplementedException();
    }

    public void DeleteTeam(int id)
    {
        try
        {
            var team = _context.Teams.Find(id);
            team.RowStatusId = (int)StatusEnums.Delete;
            _context.SaveChanges();
            
        }
        catch(Exception e)
        {
            Console.WriteLine(e);

        }
    
    }

    private TeamDtoAdmin _dbTeamAdmin(Team team)
    {
        return new TeamDtoAdmin
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