using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Services;

public class TeamService:ITeamService
{
    private HpContext _context;
    public TeamService(HpContext context)
    {
        _context = context;
    }
    public List<Team> GetTeams()
    {
       var dbTeams = _context.Teams.Where(e => e.RowStatusId == (int)StatusEnums.Active)
           .ToList();
       
       return dbTeams;
    }

    public void AddTeam(Team newTeam)
    {
        try
        {
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
        var local =  _context.Teams.FirstOrDefault(entry => entry.TeamId == team.TeamId);
            
        // check if local is not null
        if (local == null)
        {
            // detach
            // var a = _context.Entry(local);
            // _context.Entry(local).State = EntityState.Detached;
            throw new Exception("No such team");
        }

        team.CreateUserId = local.CreateUserId;
        team.CreateUser = local.CreateUser;
        team.UpdateUser = local.UpdateUser;
        team.CreateTime = local.CreateTime;
        team.UpdateTime = DateTime.Now;
        team.RowStatus = local.RowStatus;
        team.RowStatusId = local.RowStatusId;
        
        
        var entry = _context.Entry(local);
        entry.CurrentValues.SetValues(team);
        entry.State = EntityState.Modified;
        _context.SaveChanges();


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
    
}