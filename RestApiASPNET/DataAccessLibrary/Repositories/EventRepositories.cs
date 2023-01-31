using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repositories;

public class EventRepositories : DbRepositories<Event>
{
    private readonly HpContext _context;

    public EventRepositories(HpContext context /*ILogger logger*/) : base(context /*logger*/)
    {
        _context = context;
    }

    // public async Task<IEnumerable<IGrouping<int, Event>>> GetEventEveryProperty(int eventId)
    // {
    //     var @event = await GetWithEveryPropertyOnce().Where(a => a.EventId == eventId).GetAll();
    //     return @event.GroupBy(a => a.EventId);
    // }

    public async Task<Event> GetEventMissions(int eventId)
    {
        var dbEvents = await Get("EventMissions.Mission")
            .Where(a => a.EventId == eventId).GetOne();
        return dbEvents;
    }

    public async Task<Event> GetEventUsers(int eventId)
    {
        var dbEvents = await Get("EventUsers.User")
            .Where(a => a.EventId == eventId).GetOne();
        return dbEvents;
    }


}