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

    public EventRepositories(HpContext context) : base(context)
    {
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

}