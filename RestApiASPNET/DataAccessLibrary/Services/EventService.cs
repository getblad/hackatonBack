using DataAccessLibrary.CustomExceptions;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Services;

public class EventService
{
    private readonly HpContext _context;

    public EventService(HpContext context)
    {
        _context = context;
    }
    public async Task AssignMission(EventMission eventMission)
    {
        try
        {
            await _context.EventMissions.AddAsync(eventMission);
            await _context.SaveChangesAsync();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new AlreadyExistingException();
        }
        
    }

    public async Task<Event> GetEvent(int eventId)
    {
        try
        {
            var @event = await _context.Events.Include(a => a.EventMissions)
                .ThenInclude(b => b.Mission).Where(events => events.EventId == eventId).FirstOrDefaultAsync();
            return @event ?? throw new NotFoundException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
