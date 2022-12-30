using System.Threading.Tasks;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLibrary.Services;

public class MissionService:IMissionService
{
    private readonly HpContext _context;

    public MissionService(HpContext context)
    {
        _context = context;
    }

    public Task<List<Mission>> GetMissions()
    {
        return Task.FromResult(_context.Missions.ToList());
    }

    public void AddMission(Mission newMission)
    {
        try
        {
            _context.Missions.Add(newMission);
            _context.SaveChanges();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void UpdateMission(long id, Mission mission)
    {
        try
        {
            var local = _context.Set<Mission>().Local.FirstOrDefault(entry => entry.MissionId.Equals(mission.MissionId));
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(mission).State = EntityState.Modified;
            _context.SaveChanges();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Mission SingleMission(long id)
    {
        try
        { 
            return _context.Missions.Find(id);

        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteMission(long id)
    {
        try
        {
            Mission? table = _context.Missions.Find(id);
            _context.Missions.Remove(table);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}