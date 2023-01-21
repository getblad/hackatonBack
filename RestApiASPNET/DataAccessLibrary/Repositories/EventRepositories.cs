using System.Linq.Expressions;
using DataAccessLibrary.CustomExceptions;
using DataAccessLibrary.Enums;
using DataAccessLibrary.Models;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repositories;

public class EventRepositories : DbRepositories<Event>
{
    private readonly HpContext _context;

    public EventRepositories(HpContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IGrouping<int, Event>>> GetEventEveryProperty(int eventId)
    {
        var @event = await GetWithEveryPropertyOnce().Where(a => a.EventId == eventId).GetAll();
        return @event.GroupBy(a => a.EventId);
    }

    public async Task<Event> GetEventMissions(int eventId)
    {
        var dbEvents = await Get("EventMissions.Mission")
            .Where(a => a.EventId == eventId).GetOne();
        return dbEvents;
    }

    public async Task<List<EventTeam?>> GetTeamsByTwitter(int eventId, int twitterBonus, string[] users = null)
    {
        var eventTeams = await _context.EventUsers
            .Where(@event => @event.EventId == eventId && users!.Contains(@event.User.UserTwitter!) &&
                             @event.RowStatusId == (int)StatusEnums.Active)
            .Select(a => a.EventTeam).Where(team => team!.RowStatusId == (int)StatusEnums.Active && team.TeamTwitterPoint != true)
            .ToListAsync();
        foreach (var eventTeam in eventTeams)
        {
            eventTeam.UpdateTime = DateTime.Now;
            eventTeam!.EventTeamPoint += twitterBonus;
            eventTeam.TeamTwitterPoint = true;
        }

        await _context.SaveChangesAsync();
        return eventTeams;
    }

}